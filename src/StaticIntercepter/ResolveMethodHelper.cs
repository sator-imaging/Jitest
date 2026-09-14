// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using MonoMod.Utils;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Jitest;

internal static class ResolveMethodHelper
{
    static readonly ConcurrentDictionary<EquatableMethodInfo, Delegate?> OriginalMethodByMethodInfo = new();

    internal static (MethodInfo, TDelegate?) ResolveMethod<TDelegate>(Type type, string methodName)
        where TDelegate : class
    {
        Type[] parameters;
        if (typeof(TDelegate) == typeof(Delegate))
        {
            parameters = Array.Empty<Type>();
        }
        else if (typeof(TDelegate).Name.StartsWith("Func", StringComparison.Ordinal))
        {
            parameters = typeof(TDelegate).GenericTypeArguments[..^1];
        }
        else if (typeof(TDelegate).Name.StartsWith("Action", StringComparison.Ordinal))
        {
            parameters = typeof(TDelegate).GenericTypeArguments;
        }
        else
        {
            parameters = Array.Empty<Type>();
        }

        const BindingFlags AllBindingFlags = (BindingFlags)(~0);

        MethodInfo? method = null;
        if (parameters.Length > 0 || typeof(TDelegate) != typeof(Delegate))
        {
            method = type.GetMethod(methodName, AllBindingFlags, binder: null, parameters, modifiers: null);
        }

        if (method == null)
        {
            var methods = type.GetMethods(AllBindingFlags).Where(m => m.Name == methodName).ToArray();
            if (methods.Length == 1)
            {
                method = methods[0];
            }
            else if (methods.Length > 1 && parameters.Length > 0)
            {
                method = methods.FirstOrDefault(m => m.GetParameters().Length == parameters.Length);
            }
        }

        if (method == null)
        {
            var bt = type.BaseType;
            while (bt != null)
            {
                method = bt.GetMethod(methodName, AllBindingFlags, binder: null, parameters, modifiers: null);
                if (method != null) break;
                bt = bt.BaseType;
            }
        }

        if (method == null)
        {
            throw new JitestException(
                $"Method '{methodName}({string.Join(", ", parameters.Select(static x => x.Name))})' is not found in the type hierarchy of '{type}'");
        }

        if (typeof(TDelegate) == typeof(Delegate))
        {
            return (method, null);
        }

        try
        {
            var cloneMethod = OriginalMethodByMethodInfo.GetOrAdd(
                method,
                static (method) =>
                {
                    using var dmd = new DynamicMethodDefinition(method.MethodInfo);
                    var copy = dmd.Generate();

                    return copy.CreateDelegate(typeof(TDelegate));
                })
                as TDelegate;

            return (method, cloneMethod);
        }
        catch (ArgumentException)
        {
            return (method, null);
        }
    }
}
