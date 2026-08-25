/* MIT License

Copyright (c) 2016 JetBrains http://www.jetbrains.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. */

#nullable disable

#pragma warning disable 1591
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable RedundantTypeDeclarationBody
// ReSharper disable ArrangeNamespaceBody
// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace JetBrains.Annotations
{
    /// <summary>
    /// Indicates that an <c>IEnumerable</c> passed as a parameter is not enumerated.
    /// Use this annotation to suppress the 'Possible multiple enumeration of IEnumerable' inspection.
    /// </summary>
    /// <example><code>
    /// static void ThrowIfNull&lt;T&gt;([NoEnumeration] T v, string n) where T : class
    /// {
    ///   // custom check for null but no enumeration
    /// }
    ///
    /// void Foo(IEnumerable&lt;string&gt; values)
    /// {
    ///   ThrowIfNull(values, nameof(values));
    ///   var x = values.ToList(); // No warnings about multiple enumeration
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class NoEnumerationAttribute : Attribute { }

    /// <summary>
    /// Indicates that the function argument should be a string literal and match
    /// one of the parameters of the caller function. This annotation is used for parameters
    /// like <c>string paramName</c> parameter of the <see cref="System.ArgumentNullException"/> constructor.
    /// </summary>
    /// <example><code>
    /// void Foo(string param)
    /// {
    ///   if (param == null)
    ///     throw new ArgumentNullException("par"); // Warning: Cannot resolve symbol
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class InvokerParameterNameAttribute : Attribute { }

    /// <summary>
    /// Tells the code analysis engine if the parameter is completely handled when the invoked method is on stack.
    /// If the parameter is of the delegate type - indicates that the delegate can only be invoked during the method
    /// execution. The delegate can be invoked zero or multiple times, but not stored to some field and invoked later,
    /// when the containing method is no longer on the execution stack.
    /// If the parameter is of the enumerable type - indicates that it is enumerated while the method is executed.
    /// If <see cref="RequireAwait"/> is true - the attribute will only take effect if the method invocation
    /// is located under the <c>await</c> expression.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class InstantHandleAttribute : Attribute
    {
        /// <summary>
        /// Requires the method invocation to be used under the <c>await</c> expression for this attribute to take effect.
        /// Can be used for delegate/enumerable parameters of <c>async</c> methods.
        /// </summary>
        public bool RequireAwait { get; set; }
    }

    /// <summary>
    /// This annotation allows enforcing allocation-less usage patterns of delegates for performance-critical APIs.
    /// When this annotation is applied to the parameter of a delegate type,
    /// the IDE checks the input argument of this parameter:
    /// * When a lambda expression or anonymous method is passed as an argument, the IDE verifies that the passed closure
    ///   has no captures of the containing local variables and the compiler is able to cache the delegate instance
    ///   to avoid heap allocations. Otherwise, a warning is produced.
    /// * The IDE warns when the method name or local function name is passed as an argument because this always results
    ///   in heap allocation of the delegate instance.
    /// </summary>
    /// <remarks>
    /// In C# 9.0+ code, the IDE will also suggest annotating the anonymous functions with the <c>static</c> modifier
    /// to make use of the similar analysis provided by the language/compiler.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class RequireStaticDelegateAttribute : Attribute
    {
        public bool IsError { get; set; }
    }
}
