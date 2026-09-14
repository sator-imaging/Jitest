using System;
using System.IO;
using System.Text;
using System.Web;

namespace CodeGenerator;

internal static class Program
{
    private static void Main(string[] args)
    {
        string rootDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../.."));
        string staticDir = Path.Combine(rootDir, "src", "StaticIntercepter");
        string instanceDir = Path.Combine(rootDir, "src", "InstanceIntercepter");

        Directory.CreateDirectory(staticDir);
        Directory.CreateDirectory(instanceDir);

        foreach (string file in Directory.GetFiles(staticDir)) File.Delete(file);
        foreach (string file in Directory.GetFiles(instanceDir)) File.Delete(file);

        string header = @"// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;
";

        string helperCode = @"// Licensed under the Apache-2.0 License
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
        else if (typeof(TDelegate).Name.StartsWith(""Func"", StringComparison.Ordinal))
        {
            parameters = typeof(TDelegate).GenericTypeArguments[..^1];
        }
        else if (typeof(TDelegate).Name.StartsWith(""Action"", StringComparison.Ordinal))
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
                $""Method '{methodName}({string.Join("", "", parameters.Select(static x => x.Name))})' is not found in the type hierarchy of '{type}'"");
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

    internal static (MethodInfo, TOrigDelegate?) ResolveInstanceMethodWithSelf<TTarget, TOrigDelegate>(Type type, string methodName)
        where TOrigDelegate : class
    {
        var (method, _) = ResolveMethod<Delegate>(type, methodName);
        try
        {
            var cloneMethod = OriginalMethodByMethodInfo.GetOrAdd(
                method,
                static (method) =>
                {
                    using var dmd = new DynamicMethodDefinition(method.MethodInfo);
                    var copy = dmd.Generate();

                    return copy.CreateDelegate(typeof(TOrigDelegate));
                })
                as TOrigDelegate;

            return (method, cloneMethod);
        }
        catch (ArgumentException)
        {
            return (method, null);
        }
    }
}
";
        File.WriteAllText(Path.Combine(staticDir, "ResolveMethodHelper.cs"), helperCode);

        for (int n = 0; n <= 16; n++)
        {
            bool hasAction = n <= 15;
            bool hasFunc = n >= 1;

            string classTparams = "";
            string actUser = "";
            string actInternal = "";
            string actParamDecl = "";
            string actInvokeArgs = "";

            string fnUser = "";
            string fnInternal = "";
            string fnParamDecl = "";
            string fnInvokeArgs = "";

            if (n == 0)
            {
                classTparams = "TTarget";
                actUser = "Action";
                actInternal = "Action<TTarget>";
                actParamDecl = "";
                actInvokeArgs = "";
            }
            else if (n <= 15)
            {
                string[] tList = new string[n];
                string[] pDecl = new string[n];
                string[] iArgs = new string[n];
                for (int i = 0; i < n; i++)
                {
                    tList[i] = $"T{i + 1}";
                    pDecl[i] = $"T{i + 1} t{i + 1}";
                    iArgs[i] = $"t{i + 1}";
                }

                classTparams = $"TTarget, {string.Join(", ", tList)}";
                actUser = $"Action<{string.Join(", ", tList)}>";
                actInternal = $"Action<TTarget, {string.Join(", ", tList)}>";
                actParamDecl = ", " + string.Join(", ", pDecl);
                actInvokeArgs = string.Join(", ", iArgs);

                fnUser = $"Func<{string.Join(", ", tList)}>";
                fnInternal = $"Func<TTarget, {string.Join(", ", tList)}>";

                string[] fnPDecl = new string[n - 1];
                string[] fnIArgs = new string[n - 1];
                for (int i = 0; i < n - 1; i++)
                {
                    fnPDecl[i] = $"T{i + 1} t{i + 1}";
                    fnIArgs[i] = $"t{i + 1}";
                }

                fnParamDecl = fnPDecl.Length > 0 ? ", " + string.Join(", ", fnPDecl) : "";
                fnInvokeArgs = string.Join(", ", fnIArgs);
            }
            else // n == 16
            {
                string[] tList = new string[n];
                for (int i = 0; i < n; i++) tList[i] = $"T{i + 1}";

                classTparams = $"TTarget, {string.Join(", ", tList)}";
                fnUser = $"Func<{string.Join(", ", tList)}>";
                fnInternal = $"Func<TTarget, {string.Join(", ", tList)}>";

                string[] fnPDecl = new string[n - 1];
                string[] fnIArgs = new string[n - 1];
                for (int i = 0; i < n - 1; i++)
                {
                    fnPDecl[i] = $"T{i + 1} t{i + 1}";
                    fnIArgs[i] = $"t{i + 1}";
                }

                fnParamDecl = fnPDecl.Length > 0 ? ", " + string.Join(", ", fnPDecl) : "";
                fnInvokeArgs = string.Join(", ", fnIArgs);
            }

            // Generate StaticInterceptor_T[n].cs
            StringBuilder sbS = new StringBuilder();
            sbS.AppendLine(header);
            sbS.AppendLine($"/// <summary>Static method interceptor.</summary>");
            sbS.AppendLine($"public sealed class StaticInterceptor<{classTparams}>");
            sbS.AppendLine("{");
            sbS.AppendLine("    readonly MethodInfo target;");
            sbS.AppendLine("    readonly Delegate? originalMethod;");
            sbS.AppendLine();
            sbS.AppendLine($"    internal StaticInterceptor(MethodInfo target, Delegate? originalMethod)");
            sbS.AppendLine("    {");
            sbS.AppendLine("        this.target = target ?? throw new ArgumentNullException(nameof(target));");
            sbS.AppendLine("        this.originalMethod = originalMethod;");
            sbS.AppendLine("    }");

            if (hasAction)
            {
                string actUserXml = HttpUtility.HtmlEncode(actUser);
                sbS.AppendLine();
                sbS.AppendLine($"    /// <summary>Intercepts static method for <see cref=\"{actUserXml}\"/>.</summary>");
                sbS.AppendLine($"    public DetourScope Intercept({actUser} replacement)");
                sbS.AppendLine("    {");
                sbS.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
                sbS.AppendLine("        return DetourScope.Create(target, replacement, instance: null);");
                sbS.AppendLine("    }");
            }

            if (hasFunc)
            {
                string fnUserXml = HttpUtility.HtmlEncode(fnUser);
                sbS.AppendLine();
                sbS.AppendLine($"    /// <summary>Intercepts static method for <see cref=\"{fnUserXml}\"/>.</summary>");
                sbS.AppendLine($"    public DetourScope Intercept({fnUser} replacement)");
                sbS.AppendLine("    {");
                sbS.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
                sbS.AppendLine("        return DetourScope.Create(target, replacement, instance: null);");
                sbS.AppendLine("    }");
            }

            sbS.AppendLine("}");

            sbS.AppendLine();
            sbS.AppendLine("/// <summary>Extension methods for static interceptors.</summary>");
            sbS.AppendLine($"public static class StaticInterceptorExtensions_T{n}");
            sbS.AppendLine("{");

            if (hasAction)
            {
                sbS.AppendLine($"    /// <summary>Extension method for static method interceptor.</summary>");
                sbS.AppendLine($"    public static StaticInterceptor<{classTparams}> StaticJitest<{classTparams}>(this Type type, string methodName, out {actUser}? originalMethod)");
                sbS.AppendLine("    {");
                sbS.AppendLine($"        var (method, dele) = ResolveMethodHelper.ResolveMethod<{actUser}>(type, methodName);");
                sbS.AppendLine("        originalMethod = dele;");
                sbS.AppendLine($"        return new StaticInterceptor<{classTparams}>(method, dele);");
                sbS.AppendLine("    }");
            }

            if (hasFunc)
            {
                sbS.AppendLine($"    /// <summary>Extension method for static method interceptor.</summary>");
                sbS.AppendLine($"    public static StaticInterceptor<{classTparams}> StaticJitest<{classTparams}>(this Type type, string methodName, out {fnUser}? originalMethod)");
                sbS.AppendLine("    {");
                sbS.AppendLine($"        var (method, dele) = ResolveMethodHelper.ResolveMethod<{fnUser}>(type, methodName);");
                sbS.AppendLine("        originalMethod = dele;");
                sbS.AppendLine($"        return new StaticInterceptor<{classTparams}>(method, dele);");
                sbS.AppendLine("    }");
            }

            sbS.AppendLine("}");

            File.WriteAllText(Path.Combine(staticDir, $"StaticInterceptor_T{n}.cs"), sbS.ToString());

            // Generate InstanceInterceptor_T[n].cs
            StringBuilder sbI = new StringBuilder();
            sbI.AppendLine(header);
            sbI.AppendLine($"/// <summary>Instance method interceptor.</summary>");
            sbI.AppendLine($"public sealed class InstanceInterceptor<{classTparams}>");
            sbI.AppendLine("{");
            sbI.AppendLine("    readonly MethodInfo target;");
            sbI.AppendLine("    readonly Delegate? originalMethod;");
            sbI.AppendLine();
            sbI.AppendLine($"    internal InstanceInterceptor(MethodInfo target, Delegate? originalMethod)");
            sbI.AppendLine("    {");
            sbI.AppendLine("        this.target = target ?? throw new ArgumentNullException(nameof(target));");
            sbI.AppendLine("        this.originalMethod = originalMethod;");
            sbI.AppendLine("    }");

            if (hasAction)
            {
                string actUserXml = HttpUtility.HtmlEncode(actUser);
                sbI.AppendLine();
                sbI.AppendLine($"    /// <summary>Intercepts instance method for specific instance with <see cref=\"{actUserXml}\"/>.</summary>");
                sbI.AppendLine($"    public DetourScope Intercept(TTarget instance, {actUser} replacement)");
                sbI.AppendLine("    {");
                sbI.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
                sbI.AppendLine($"        var orig = originalMethod != null ? ({actInternal})(object)originalMethod : null;");
                sbI.AppendLine($"        {actInternal} actual = (TTarget self{actParamDecl}) =>");
                sbI.AppendLine("        {");
                sbI.AppendLine($"            if (object.ReferenceEquals(self, instance)) replacement.Invoke({actInvokeArgs});");
                sbI.AppendLine($"            else orig?.Invoke(self{(string.IsNullOrEmpty(actInvokeArgs) ? "" : ", " + actInvokeArgs)});");
                sbI.AppendLine("        };");
                sbI.AppendLine("        return DetourScope.Create(target, actual, instance);");
                sbI.AppendLine("    }");

                sbI.AppendLine();
                sbI.AppendLine($"    /// <summary>Intercepts instance method across all instances with <see cref=\"{actUserXml}\"/>.</summary>");
                sbI.AppendLine($"    public DetourScope InterceptUnsafe({actUser} replacement)");
                sbI.AppendLine("    {");
                sbI.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
                sbI.AppendLine($"        {actInternal} actual = (TTarget self{actParamDecl}) => replacement.Invoke({actInvokeArgs});");
                sbI.AppendLine("        return DetourScope.Create(target, actual, instance: null);");
                sbI.AppendLine("    }");
            }

            if (hasFunc)
            {
                string fnUserXml = HttpUtility.HtmlEncode(fnUser);
                sbI.AppendLine();
                sbI.AppendLine($"    /// <summary>Intercepts instance method for specific instance with <see cref=\"{fnUserXml}\"/>.</summary>");
                sbI.AppendLine($"    public DetourScope Intercept(TTarget instance, {fnUser} replacement)");
                sbI.AppendLine("    {");
                sbI.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
                sbI.AppendLine($"        var orig = originalMethod != null ? ({fnInternal})(object)originalMethod : null;");
                sbI.AppendLine($"        {fnInternal} actual = (TTarget self{fnParamDecl}) =>");
                sbI.AppendLine("            object.ReferenceEquals(self, instance)");
                sbI.AppendLine($"                ? replacement.Invoke({fnInvokeArgs})");
                sbI.AppendLine($"                : (orig != null ? orig.Invoke(self{(string.IsNullOrEmpty(fnInvokeArgs) ? "" : ", " + fnInvokeArgs)}) : default!);");
                sbI.AppendLine("        return DetourScope.Create(target, actual, instance);");
                sbI.AppendLine("    }");

                sbI.AppendLine();
                sbI.AppendLine($"    /// <summary>Intercepts instance method across all instances with <see cref=\"{fnUserXml}\"/>.</summary>");
                sbI.AppendLine($"    public DetourScope InterceptUnsafe({fnUser} replacement)");
                sbI.AppendLine("    {");
                sbI.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
                sbI.AppendLine($"        {fnInternal} actual = (TTarget self{fnParamDecl}) => replacement.Invoke({fnInvokeArgs});");
                sbI.AppendLine("        return DetourScope.Create(target, actual, instance: null);");
                sbI.AppendLine("    }");
            }

            sbI.AppendLine("}");

            sbI.AppendLine();
            sbI.AppendLine("/// <summary>Extension methods for instance interceptors.</summary>");
            sbI.AppendLine($"public static class InstanceInterceptorExtensions_T{n}");
            sbI.AppendLine("{");

            if (hasAction)
            {
                sbI.AppendLine($"    /// <summary>Extension method for instance method interceptor.</summary>");
                sbI.AppendLine($"    public static InstanceInterceptor<{classTparams}> InstanceJitest<{classTparams}>(this TTarget instance, string methodName, out {actUser}? originalMethod)");
                sbI.AppendLine("    {");
                sbI.AppendLine("        if (instance == null) throw new ArgumentNullException(nameof(instance));");
                sbI.AppendLine("        if (typeof(TTarget).IsValueType)");
                sbI.AppendLine("        {");
                sbI.AppendLine($"            var (method, _) = ResolveMethodHelper.ResolveMethod<Delegate>(typeof(TTarget), methodName);");
                sbI.AppendLine("            originalMethod = null;");
                sbI.AppendLine($"            return new InstanceInterceptor<{classTparams}>(method, null);");
                sbI.AppendLine("        }");
                sbI.AppendLine("        else");
                sbI.AppendLine("        {");
                sbI.AppendLine($"            var (method, dele) = ResolveMethodHelper.ResolveInstanceMethodWithSelf<TTarget, {actInternal}>(typeof(TTarget), methodName);");
                sbI.AppendLine("            originalMethod = dele != null ? ({actUser})((object)dele) : null;");
                sbI.AppendLine($"            return new InstanceInterceptor<{classTparams}>(method, dele);");
                sbI.AppendLine("        }");
                sbI.AppendLine("    }");
            }

            if (hasFunc)
            {
                sbI.AppendLine($"    /// <summary>Extension method for instance method interceptor.</summary>");
                sbI.AppendLine($"    public static InstanceInterceptor<{classTparams}> InstanceJitest<{classTparams}>(this TTarget instance, string methodName, out {fnUser}? originalMethod)");
                sbI.AppendLine("    {");
                sbI.AppendLine("        if (instance == null) throw new ArgumentNullException(nameof(instance));");
                sbI.AppendLine("        if (typeof(TTarget).IsValueType)");
                sbI.AppendLine("        {");
                sbI.AppendLine($"            var (method, _) = ResolveMethodHelper.ResolveMethod<Delegate>(typeof(TTarget), methodName);");
                sbI.AppendLine("            originalMethod = null;");
                sbI.AppendLine($"            return new InstanceInterceptor<{classTparams}>(method, null);");
                sbI.AppendLine("        }");
                sbI.AppendLine("        else");
                sbI.AppendLine("        {");
                sbI.AppendLine($"            var (method, dele) = ResolveMethodHelper.ResolveInstanceMethodWithSelf<TTarget, {fnInternal}>(typeof(TTarget), methodName);");
                sbI.AppendLine("            originalMethod = dele != null ? ({fnUser})((object)dele) : null;");
                sbI.AppendLine($"            return new InstanceInterceptor<{classTparams}>(method, dele);");
                sbI.AppendLine("        }");
                sbI.AppendLine("    }");
            }

            sbI.AppendLine("}");

            File.WriteAllText(Path.Combine(instanceDir, $"InstanceInterceptor_T{n}.cs"), sbI.ToString());
        }

        Console.WriteLine("Code generation completed successfully!");
    }
}
