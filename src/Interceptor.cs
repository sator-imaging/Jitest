// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>
/// Method interceptor.
/// </summary>
public sealed class Interceptor
{
    readonly MethodInfo target;
    readonly object? instance;

    internal Interceptor(MethodInfo target, object? instance)
    {
        if (target == null)
        {
            JitestException.ThrowArgumentNull(nameof(target));
        }

        this.target = target;
        this.instance = instance;
    }

    /// <remarks>
    /// **IMPORTANT**: Instance method has a hidden first argument of declaring type that is implicitly inserted by C# compiler.
    /// </remarks>
    /// <returns><see cref="IDisposable"/></returns>
    public DetourScope Intercept(Delegate instance_method_has_a_hidden_first_argument_of_declaring_type)
    {
        if (instance_method_has_a_hidden_first_argument_of_declaring_type == null)
        {
            JitestException.ThrowArgumentNull(nameof(instance_method_has_a_hidden_first_argument_of_declaring_type));
        }

        return instance == null
            ? CreateScope(instance_method_has_a_hidden_first_argument_of_declaring_type)
            : CreateScope(instance_method_has_a_hidden_first_argument_of_declaring_type, instance);
    }

    DetourScope CreateScope(Delegate replacement)
    {
        if (!target.IsStatic)
        {
            JitestException.Throw("Target method must be a static method");
        }

        return DetourScope.Create(target, replacement, instance: null);
    }

    DetourScope CreateScope(Delegate replacement, object instance)
    {
        if (target.IsStatic)
        {
            JitestException.Throw("Target method must be an instance method");
        }

        var declaringType = target.DeclaringType;
        if (declaringType == null)
        {
            JitestException.Throw("Target method has no declaring type");
        }

        if (!declaringType.IsInstanceOfType(instance))
        {
            JitestException.Throw(
                $"Instance type '{instance.GetType()}' is not compatible with target declaring type '{declaringType}'");
        }

        var parameters = replacement.Method.GetParameters();
        if (parameters.Length == 0)
        {
            JitestException.Throw(
                "The replacement delegate for an instance method must have the target instance as its first argument" +
                " (C# compiler inserts a hidden argument to instance method implicitly)");
        }

        var replacementInstanceType = parameters[0].ParameterType;
        if (!declaringType.IsAssignableFrom(replacementInstanceType) ||
            parameters.Length != target.GetParameters().Length + 1)
        {
            JitestException.Throw(
                $"The first replacement parameter type '{replacementInstanceType}' " +
                $"is not compatible with target declaring type '{declaringType}'. " +
                "The first argument must accept the target instance " +
                " (C# compiler inserts a hidden argument to instance method implicitly)");
        }

        return DetourScope.Create(target, replacement, instance);
    }
}
