using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using CheckType = CheckAndThrow.Check;
using ThrowType = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests;

public class PublicApiAttributeTests
{
    [Test]
    public async Task EveryThrowMethodHasDoesNotReturnAttribute()
    {
        var unannotatedMethods = typeof(ThrowType)
            .GetNestedTypes(BindingFlags.Public)
            .Append(typeof(ThrowType))
            .SelectMany(type =>
                type.GetMethods(
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly
                )
            )
            .Where(method => !method.IsDefined(typeof(DoesNotReturnAttribute), inherit: false))
            .Select(FormatMethod)
            .ToArray();

        await Assert.That(unannotatedMethods).IsEmpty();
    }

    [Test]
    public async Task EveryParamNameInCheckAndThrowArgMethodsHasInvokerParameterNameAttribute()
    {
        var methods = new[] { typeof(CheckType.Arg), typeof(ThrowType.Arg) }.SelectMany(type =>
            type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
        );
        var unannotatedParameters = methods
            .SelectMany(method =>
                method
                    .GetParameters()
                    .Where(parameter =>
                        parameter.Name == "paramName"
                        && !HasAttribute(
                            parameter,
                            "JetBrains.Annotations.InvokerParameterNameAttribute"
                        )
                    )
                    .Select(parameter => $"{FormatMethod(method)} parameter '{parameter.Name}'")
            )
            .ToArray();

        await Assert.That(string.Join("; ", unannotatedParameters)).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task EveryCheckArgMethodHasAnnotatedParamNameParameter()
    {
        var methods = typeof(CheckType.Arg).GetMethods(
            BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly
        );
        var invalidMethods = methods
            .Where(method =>
                !method
                    .GetParameters()
                    .Any(parameter =>
                        parameter.Name == "paramName"
                        && HasAttribute(
                            parameter,
                            "JetBrains.Annotations.InvokerParameterNameAttribute"
                        )
                        && parameter.IsDefined(
                            typeof(CallerArgumentExpressionAttribute),
                            inherit: false
                        )
                    )
            )
            .Select(FormatMethod)
            .ToArray();

        await Assert.That(string.Join("; ", invalidMethods)).IsEqualTo(string.Empty);
    }

    static bool HasAttribute(ParameterInfo parameter, string attributeTypeName) =>
        parameter.CustomAttributes.Any(attribute =>
            attribute.AttributeType.FullName == attributeTypeName
        );

    static string FormatMethod(MethodInfo method) =>
        $"{method.DeclaringType?.FullName}.{method.Name}({string.Join(", ", method.GetParameters().Select(parameter => parameter.Name))})";
}
