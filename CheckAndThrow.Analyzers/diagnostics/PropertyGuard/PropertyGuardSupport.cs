using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CheckAndThrow.Analyzers.Diagnostics.PropertyGuard;

internal sealed class PropertyGuardTarget
{
    public PropertyGuardTarget(
        PropertyDeclarationSyntax declaration,
        AccessorDeclarationSyntax setter,
        IPropertySymbol propertySymbol,
        IParameterSymbol valueParameter
    )
    {
        Declaration = declaration;
        Setter = setter;
        PropertySymbol = propertySymbol;
        ValueParameter = valueParameter;
    }

    public PropertyDeclarationSyntax Declaration { get; }

    public AccessorDeclarationSyntax Setter { get; }

    public IPropertySymbol PropertySymbol { get; }

    public IParameterSymbol ValueParameter { get; }
}

internal static class PropertyGuardSupport
{
    internal static PropertyGuardTarget? FindTarget(
        SyntaxNode root,
        Diagnostic diagnostic,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        var property = root.FindToken(diagnostic.Location.SourceSpan.Start)
            .Parent?.AncestorsAndSelf()
            .OfType<PropertyDeclarationSyntax>()
            .FirstOrDefault();
        if (
            property is null
            || property.Parent is not TypeDeclarationSyntax
            || property.AccessorList is null
            || property.Modifiers.Any(modifier =>
                modifier.IsKind(SyntaxKind.AbstractKeyword)
                || modifier.IsKind(SyntaxKind.ExternKeyword)
            )
        )
        {
            return null;
        }

        return CreateTarget(property, semanticModel, cancellationToken);
    }

    internal static PropertyGuardTarget? CreateTarget(
        PropertyDeclarationSyntax property,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        if (
            property.Parent is not TypeDeclarationSyntax
            || property.AccessorList is null
            || property.Modifiers.Any(modifier =>
                modifier.IsKind(SyntaxKind.AbstractKeyword)
                || modifier.IsKind(SyntaxKind.ExternKeyword)
            )
        )
        {
            return null;
        }

        var propertySymbol = semanticModel.GetDeclaredSymbol(property, cancellationToken);
        var setter = property.AccessorList.Accessors.FirstOrDefault(accessor =>
            accessor.IsKind(SyntaxKind.SetAccessorDeclaration)
            || accessor.IsKind(SyntaxKind.InitAccessorDeclaration)
        );
        var valueParameter = propertySymbol?.SetMethod?.Parameters.SingleOrDefault();
        return
            propertySymbol is null
            || propertySymbol.ContainingType.TypeKind == TypeKind.Interface
            || setter is null
            || valueParameter is null
            ? null
            : new PropertyGuardTarget(property, setter, propertySymbol, valueParameter);
    }

    internal static bool IsEligible(PropertyGuardTarget target)
    {
        if (target.ValueParameter.RefKind == RefKind.Out)
        {
            return false;
        }

        if (target.PropertySymbol.NullableAnnotation == NullableAnnotation.Annotated)
        {
            return false;
        }

        return target.PropertySymbol.Type switch
        {
            ITypeParameterSymbol typeParameter => !typeParameter.HasValueTypeConstraint,
            _ => target.PropertySymbol.Type.IsReferenceType,
        };
    }

    internal static bool SupportsField(Compilation compilation) =>
        compilation is CSharpCompilation csharpCompilation
        && (int)csharpCompilation.LanguageVersion >= 1400;

    internal static IEnumerable<InvocationExpressionSyntax> GetTopLevelGuardInvocations(
        AccessorDeclarationSyntax accessor
    )
    {
        if (accessor.Body is { } body)
        {
            foreach (var statement in body.Statements)
            {
                var invocation = GetTopLevelGuardInvocation(statement);
                if (invocation is not null)
                {
                    yield return invocation;
                }

                if (
                    statement is LocalDeclarationStatementSyntax local
                    && local.Declaration.Variables.Count == 1
                    && local.Declaration.Variables[0].Initializer?.Value
                        is MemberAccessExpressionSyntax
                        {
                            Expression: InvocationExpressionSyntax inlineGuard,
                        }
                )
                {
                    yield return inlineGuard;
                }
            }
        }

        if (accessor.ExpressionBody?.Expression is InvocationExpressionSyntax invocationExpression)
        {
            yield return invocationExpression;
        }
        else if (
            accessor.ExpressionBody?.Expression is AssignmentExpressionSyntax
            {
                Right: InvocationExpressionSyntax assignmentInvocation,
            }
        )
        {
            yield return assignmentInvocation;
        }
    }

    internal static SyntaxNode AddGuard(
        SyntaxNode root,
        PropertyGuardTarget target,
        string call,
        Compilation compilation
    )
    {
        var property = target.Declaration;
        var replacement = IsAutoProperty(property)
            ? SupportsField(compilation)
                ? UseFieldBackedProperty(
                    property,
                    target.Setter,
                    "field",
                    call,
                    keepAutoGetter: true
                )
                : UseExplicitBackingField(root, target, call)
            : AddGuardToSetter(property, target.Setter, call);

        return replacement is PropertyDeclarationSyntax propertyReplacement
            ? root.ReplaceNode(property, propertyReplacement)
            : replacement;
    }

    static bool IsAutoProperty(PropertyDeclarationSyntax property) =>
        property.AccessorList?.Accessors.All(accessor =>
            accessor.Body is null && accessor.ExpressionBody is null
        ) == true;

    static PropertyDeclarationSyntax AddGuardToSetter(
        PropertyDeclarationSyntax property,
        AccessorDeclarationSyntax setter,
        string call
    )
    {
        var statement = SyntaxFactory.ParseStatement(call + ";");
        var replacement =
            setter.Body is { } body
                ? setter.WithBody(body.WithStatements(body.Statements.Insert(0, statement)))
            : setter.ExpressionBody is { } expressionBody
                ? setter
                    .WithBody(
                        SyntaxFactory
                            .Block(
                                statement,
                                SyntaxFactory
                                    .ExpressionStatement(expressionBody.Expression)
                                    .WithTriviaFrom(expressionBody.Expression)
                            )
                            .WithTriviaFrom(expressionBody)
                    )
                    .WithExpressionBody(null)
                    .WithSemicolonToken(default)
            : setter;

        return property.WithAccessorList(
            property.AccessorList!.WithAccessors(
                property.AccessorList.Accessors.Replace(setter, replacement)
            )
        );
    }

    static PropertyDeclarationSyntax UseFieldBackedProperty(
        PropertyDeclarationSyntax property,
        AccessorDeclarationSyntax setter,
        string fieldName,
        string call,
        bool keepAutoGetter = false
    )
    {
        var accessors = property.AccessorList!.Accessors.Replace(
            setter,
            CreateAccessor(setter, $"{fieldName} = {call}")
        );
        var getter = accessors.FirstOrDefault(accessor =>
            accessor.IsKind(SyntaxKind.GetAccessorDeclaration)
        );
        if (
            !keepAutoGetter
            && getter is not null
            && getter.Body is null
            && getter.ExpressionBody is null
        )
        {
            accessors = accessors.Replace(getter, CreateAccessor(getter, fieldName));
        }

        return property.WithAccessorList(property.AccessorList.WithAccessors(accessors));
    }

    static SyntaxNode UseExplicitBackingField(
        SyntaxNode root,
        PropertyGuardTarget target,
        string call
    )
    {
        if (target.Declaration.Parent is not TypeDeclarationSyntax containingType)
        {
            return root;
        }

        var fieldName = GetFieldName(target, containingType);
        var field = CreateField(target.Declaration, fieldName);
        var property = UseFieldBackedProperty(target.Declaration, target.Setter, fieldName, call)
            .WithInitializer(null);
        var members = containingType.Members;
        var propertyIndex = members.IndexOf(target.Declaration);
        if (propertyIndex < 0)
        {
            return root;
        }

        var replacementMembers = members
            .RemoveAt(propertyIndex)
            .Insert(propertyIndex, field)
            .Insert(propertyIndex + 1, property);
        return root.ReplaceNode(containingType, containingType.WithMembers(replacementMembers));
    }

    static FieldDeclarationSyntax CreateField(PropertyDeclarationSyntax property, string fieldName)
    {
        var variable = SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(fieldName));
        if (property.Initializer is { } initializer)
        {
            variable = variable.WithInitializer(initializer);
        }

        var modifiers = new List<SyntaxToken>
        {
            SyntaxFactory.Token(SyntaxKind.PrivateKeyword).WithTrailingTrivia(SyntaxFactory.Space),
        };
        if (property.Modifiers.Any(SyntaxKind.StaticKeyword))
        {
            modifiers.Add(
                SyntaxFactory
                    .Token(SyntaxKind.StaticKeyword)
                    .WithTrailingTrivia(SyntaxFactory.Space)
            );
        }

        return SyntaxFactory
            .FieldDeclaration(
                SyntaxFactory
                    .VariableDeclaration(
                        property.Type.WithoutTrivia().WithTrailingTrivia(SyntaxFactory.Space)
                    )
                    .WithVariables(SyntaxFactory.SingletonSeparatedList(variable))
            )
            .WithModifiers(SyntaxFactory.TokenList(modifiers));
    }

    static string GetFieldName(PropertyGuardTarget target, TypeDeclarationSyntax containingType)
    {
        var propertyName = target.PropertySymbol.Name;
        var baseName = "_" + char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        var existingNames = new HashSet<string>(
            containingType
                .Members.OfType<BaseFieldDeclarationSyntax>()
                .SelectMany(field => field.Declaration.Variables)
                .Select(variable => variable.Identifier.ValueText),
            StringComparer.Ordinal
        );
        var fieldName = baseName;
        var suffix = 1;
        while (existingNames.Contains(fieldName))
        {
            fieldName = $"{baseName}{suffix++}";
        }

        return fieldName;
    }

    static AccessorDeclarationSyntax CreateAccessor(
        AccessorDeclarationSyntax original,
        string expression
    ) =>
        original
            .WithBody(null)
            .WithExpressionBody(
                SyntaxFactory.ArrowExpressionClause(
                    SyntaxFactory
                        .Token(SyntaxKind.EqualsGreaterThanToken)
                        .WithLeadingTrivia(SyntaxFactory.Space)
                        .WithTrailingTrivia(SyntaxFactory.Space),
                    SyntaxFactory.ParseExpression(expression)
                )
            )
            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));

    static InvocationExpressionSyntax? GetTopLevelGuardInvocation(StatementSyntax statement)
    {
        if (
            statement is ExpressionStatementSyntax
            {
                Expression: InvocationExpressionSyntax invocation,
            }
        )
        {
            return invocation;
        }

        if (
            statement is ExpressionStatementSyntax
            {
                Expression: AssignmentExpressionSyntax
                {
                    Right: InvocationExpressionSyntax assignmentInvocation,
                },
            }
        )
        {
            return assignmentInvocation;
        }

        return
            statement is LocalDeclarationStatementSyntax local
            && local.Declaration.Variables.Count == 1
            ? local.Declaration.Variables[0].Initializer?.Value as InvocationExpressionSyntax
            : null;
    }
}
