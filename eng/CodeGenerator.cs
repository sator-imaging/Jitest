// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#:property LangVersion=latest
#:property TargetFramework=net10.0
#:property PublishAot=false

using System;
using System.IO;
using System.Text;
using System.Web;

const string FileExtension = ".g.cs";

string rootDir = Directory.GetCurrentDirectory();
string staticDir = Path.Combine(rootDir, "src", "Generated_StaticIntercepter");
string instanceDir = Path.Combine(rootDir, "src", "Generated_InstanceIntercepter");

Directory.CreateDirectory(staticDir);
Directory.CreateDirectory(instanceDir);

foreach (string file in Directory.GetFiles(staticDir)) File.Delete(file);
foreach (string file in Directory.GetFiles(instanceDir)) File.Delete(file);

string licenseHeader = @"// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest";

string header = $@"{licenseHeader}

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;
";

for (int n = 0; n <= 16; n++)
{
    bool hasAction = n <= 15;
    bool hasFunc = n >= 1;

    if (n == 0)
    {
        // Action 0 params
        GenerateStaticAction(staticDir, header, 0, "StaticInterceptorActionT0", "TTarget", "Action", "TTarget", "TTarget");
        GenerateInstanceAction(instanceDir, header, 0, "InstanceInterceptorActionT0", "TTarget", "Action", "TTarget", "TTarget", "Action<TTarget>", "", "");
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

        string classTparamsAct = $"TTarget, {string.Join(", ", tList)}";
        string actUser = $"Action<{string.Join(", ", tList)}>";
        string actInternal = $"Action<TTarget, {string.Join(", ", tList)}>";
        string actParamDecl = ", " + string.Join(", ", pDecl);
        string actInvokeArgs = string.Join(", ", iArgs);

        string classTparamsFn = $"TTarget, {string.Join(", ", tList)}";
        string fnUser = $"Func<{string.Join(", ", tList)}>";
        string fnInternal = $"Func<TTarget, {string.Join(", ", tList)}>";

        string[] fnPDecl = new string[n - 1];
        string[] fnIArgs = new string[n - 1];
        for (int i = 0; i < n - 1; i++)
        {
            fnPDecl[i] = $"T{i + 1} t{i + 1}";
            fnIArgs[i] = $"t{i + 1}";
        }
        string fnParamDecl = fnPDecl.Length > 0 ? ", " + string.Join(", ", fnPDecl) : "";
        string fnInvokeArgs = string.Join(", ", fnIArgs);

        GenerateStaticAction(staticDir, header, n, $"StaticInterceptorActionT{n}", classTparamsAct, actUser, classTparamsAct, classTparamsAct);
        GenerateStaticFunc(staticDir, header, n, $"StaticInterceptorFuncT{n}", classTparamsFn, fnUser, classTparamsFn, classTparamsFn);

        GenerateInstanceAction(instanceDir, header, n, $"InstanceInterceptorActionT{n}", classTparamsAct, actUser, classTparamsAct, classTparamsAct, actInternal, actParamDecl, actInvokeArgs);
        GenerateInstanceFunc(instanceDir, header, n, $"InstanceInterceptorFuncT{n}", classTparamsFn, fnUser, classTparamsFn, classTparamsFn, fnInternal, fnParamDecl, fnInvokeArgs);
    }
    else // n == 16
    {
        string[] tList = new string[n];
        for (int i = 0; i < n; i++) tList[i] = $"T{i + 1}";

        string classTparamsFn = $"TTarget, {string.Join(", ", tList)}";
        string fnUser = $"Func<{string.Join(", ", tList)}>";
        string fnInternal = $"Func<TTarget, {string.Join(", ", tList)}>";

        string[] fnPDecl = new string[n - 1];
        string[] fnIArgs = new string[n - 1];
        for (int i = 0; i < n - 1; i++)
        {
            fnPDecl[i] = $"T{i + 1} t{i + 1}";
            fnIArgs[i] = $"t{i + 1}";
        }
        string fnParamDecl = fnPDecl.Length > 0 ? ", " + string.Join(", ", fnPDecl) : "";
        string fnInvokeArgs = string.Join(", ", fnIArgs);

        GenerateStaticFunc(staticDir, header, n, $"StaticInterceptorFuncT{n}", classTparamsFn, fnUser, classTparamsFn, classTparamsFn);
        GenerateInstanceFunc(instanceDir, header, n, $"InstanceInterceptorFuncT{n}", classTparamsFn, fnUser, classTparamsFn, classTparamsFn, fnInternal, fnParamDecl, fnInvokeArgs);
    }
}

Console.WriteLine("Code generation completed successfully!");

static void GenerateStaticAction(string dir, string header, int n, string className, string classTparams, string actUser, string extTargs, string extTparams)
{
    string actUserXml = HttpUtility.HtmlEncode(actUser);
    StringBuilder sb = new StringBuilder();
    sb.AppendLine(header);
    sb.AppendLine($"/// <summary>Static method interceptor for <see cref=\"{actUserXml}\"/>.</summary>");
    sb.AppendLine($"public sealed class {className}<{classTparams}>");
    sb.AppendLine("{");
    sb.AppendLine("    readonly Interceptor interceptor;");
    sb.AppendLine();
    sb.AppendLine($"    internal {className}(Interceptor interceptor)");
    sb.AppendLine("    {");
    sb.AppendLine("        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));");
    sb.AppendLine("    }");
    sb.AppendLine();
    sb.AppendLine($"    /// <summary>Intercepts static method for <see cref=\"{actUserXml}\"/>.</summary>");
    sb.AppendLine($"    public DetourScope Intercept({actUser} replacement)");
    sb.AppendLine("    {");
    sb.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
    sb.AppendLine("        return interceptor.Intercept(replacement);");
    sb.AppendLine("    }");
    sb.AppendLine("}");
    sb.AppendLine();
    sb.AppendLine("/// <summary>Extension methods for static interceptors.</summary>");
    sb.AppendLine("public static partial class StaticInterceptorExtensions");
    sb.AppendLine("{");
    sb.AppendLine($"    /// <summary>Extension method for static method interceptor.</summary>");
    sb.AppendLine($"    public static {className}<{classTparams}> StaticJitest<{extTparams}>(this Type type, string methodName, out {actUser}? originalMethod)");
    sb.AppendLine("    {");
    sb.AppendLine("        if (type == null) throw new ArgumentNullException(nameof(type));");
    sb.AppendLine($"        var interceptor = type.Jitest<{actUser}>(methodName, out originalMethod);");
    sb.AppendLine($"        return new {className}<{classTparams}>(interceptor);");
    sb.AppendLine("    }");
    sb.AppendLine("}");

    File.WriteAllText(Path.Combine(dir, $"{className}{FileExtension}"), sb.ToString());
}

static void GenerateStaticFunc(string dir, string header, int n, string className, string classTparams, string fnUser, string extTargs, string extTparams)
{
    string fnUserXml = HttpUtility.HtmlEncode(fnUser);
    StringBuilder sb = new StringBuilder();
    sb.AppendLine(header);
    sb.AppendLine($"/// <summary>Static method interceptor for <see cref=\"{fnUserXml}\"/>.</summary>");
    sb.AppendLine($"public sealed class {className}<{classTparams}>");
    sb.AppendLine("{");
    sb.AppendLine("    readonly Interceptor interceptor;");
    sb.AppendLine();
    sb.AppendLine($"    internal {className}(Interceptor interceptor)");
    sb.AppendLine("    {");
    sb.AppendLine("        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));");
    sb.AppendLine("    }");
    sb.AppendLine();
    sb.AppendLine($"    /// <summary>Intercepts static method for <see cref=\"{fnUserXml}\"/>.</summary>");
    sb.AppendLine($"    public DetourScope Intercept({fnUser} replacement)");
    sb.AppendLine("    {");
    sb.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
    sb.AppendLine("        return interceptor.Intercept(replacement);");
    sb.AppendLine("    }");
    sb.AppendLine("}");
    sb.AppendLine();
    sb.AppendLine("/// <summary>Extension methods for static interceptors.</summary>");
    sb.AppendLine("public static partial class StaticInterceptorExtensions");
    sb.AppendLine("{");
    sb.AppendLine($"    /// <summary>Extension method for static method interceptor.</summary>");
    sb.AppendLine($"    public static {className}<{classTparams}> StaticJitest<{extTparams}>(this Type type, string methodName, out {fnUser}? originalMethod)");
    sb.AppendLine("    {");
    sb.AppendLine("        if (type == null) throw new ArgumentNullException(nameof(type));");
    sb.AppendLine($"        var interceptor = type.Jitest<{fnUser}>(methodName, out originalMethod);");
    sb.AppendLine($"        return new {className}<{classTparams}>(interceptor);");
    sb.AppendLine("    }");
    sb.AppendLine("}");

    File.WriteAllText(Path.Combine(dir, $"{className}{FileExtension}"), sb.ToString());
}

static void GenerateInstanceAction(string dir, string header, int n, string className, string classTparams, string actUser, string extTargs, string extTparams, string actInternal, string actParamDecl, string actInvokeArgs)
{
    string actUserXml = HttpUtility.HtmlEncode(actUser);
    StringBuilder sb = new StringBuilder();
    sb.AppendLine(header);
    sb.AppendLine($"/// <summary>Instance method interceptor for <see cref=\"{actUserXml}\"/>.</summary>");
    sb.AppendLine($"public sealed class {className}<{classTparams}>");
    sb.AppendLine("{");
    sb.AppendLine("    readonly Interceptor interceptor;");
    sb.AppendLine($"    readonly {actUser}? originalMethod;");
    sb.AppendLine();
    sb.AppendLine($"    internal {className}(Interceptor interceptor, {actUser} originalMethod)");
    sb.AppendLine("    {");
    sb.AppendLine("        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));");
    sb.AppendLine("        this.originalMethod = originalMethod;");
    sb.AppendLine("    }");
    sb.AppendLine();
    sb.AppendLine($"    /// <summary>Intercepts instance method for specific instance with <see cref=\"{actUserXml}\"/>.</summary>");
    sb.AppendLine($"    public DetourScope Intercept(TTarget instance, {actUser} replacement)");
    sb.AppendLine("    {");
    sb.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
    sb.AppendLine($"        var actual = (TTarget self{actParamDecl}) =>");
    sb.AppendLine("        {");
    sb.AppendLine("            if (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance))");
    sb.AppendLine($"                replacement.Invoke({actInvokeArgs});");
    sb.AppendLine("            else");
    sb.AppendLine($"                originalMethod.Invoke({actInvokeArgs});");
    sb.AppendLine("        };");
    sb.AppendLine("        return interceptor.Intercept(actual);");
    sb.AppendLine("    }");
    sb.AppendLine();
    sb.AppendLine($"    /// <summary>Intercepts instance method across all instances with <see cref=\"{actUserXml}\"/>.</summary>");
    sb.AppendLine($"    public DetourScope InterceptUnsafe({actUser} replacement)");
    sb.AppendLine("    {");
    sb.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
    sb.AppendLine($"        var actual = (TTarget self{actParamDecl}) => replacement.Invoke({actInvokeArgs});");
    sb.AppendLine("        return interceptor.Intercept(actual);");
    sb.AppendLine("    }");
    sb.AppendLine("}");
    sb.AppendLine();
    sb.AppendLine("/// <summary>Extension methods for instance interceptors.</summary>");
    sb.AppendLine("public static partial class InstanceInterceptorExtensions");
    sb.AppendLine("{");
    sb.AppendLine($"    /// <summary>Extension method for instance method interceptor.</summary>");
    sb.AppendLine($"    public static {className}<{classTparams}> InstanceJitest<{extTparams}>(this TTarget instance, string methodName, out {actUser}? originalMethod)");
    sb.AppendLine("    {");
    sb.AppendLine("        if (instance == null) throw new ArgumentNullException(nameof(instance));");
    sb.AppendLine($"        var interceptor = instance.Jitest<{actUser}>(methodName, out originalMethod);");
    sb.AppendLine($"        return new {className}<{classTparams}>(interceptor, originalMethod);");
    sb.AppendLine("    }");
    sb.AppendLine("}");

    File.WriteAllText(Path.Combine(dir, $"{className}{FileExtension}"), sb.ToString());
}

static void GenerateInstanceFunc(string dir, string header, int n, string className, string classTparams, string fnUser, string extTargs, string extTparams, string fnInternal, string fnParamDecl, string fnInvokeArgs)
{
    string fnUserXml = HttpUtility.HtmlEncode(fnUser);
    StringBuilder sb = new StringBuilder();
    sb.AppendLine(header);
    sb.AppendLine($"/// <summary>Instance method interceptor for <see cref=\"{fnUserXml}\"/>.</summary>");
    sb.AppendLine($"public sealed class {className}<{classTparams}>");
    sb.AppendLine("{");
    sb.AppendLine("    readonly Interceptor interceptor;");
    sb.AppendLine($"    readonly {fnUser}? originalMethod;");
    sb.AppendLine();
    sb.AppendLine($"    internal {className}(Interceptor interceptor, {fnUser} originalMethod)");
    sb.AppendLine("    {");
    sb.AppendLine("        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));");
    sb.AppendLine("        this.originalMethod = originalMethod;");
    sb.AppendLine("    }");
    sb.AppendLine();
    sb.AppendLine($"    /// <summary>Intercepts instance method for specific instance with <see cref=\"{fnUserXml}\"/>.</summary>");
    sb.AppendLine($"    public DetourScope Intercept(TTarget instance, {fnUser} replacement)");
    sb.AppendLine("    {");
    sb.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
    sb.AppendLine($"        var actual = (TTarget self{fnParamDecl}) =>");
    sb.AppendLine("            (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance))");
    sb.AppendLine($"                ? replacement.Invoke({fnInvokeArgs})");
    sb.AppendLine($"                : originalMethod.Invoke({fnInvokeArgs});");
    sb.AppendLine("        return interceptor.Intercept(actual);");
    sb.AppendLine("    }");
    sb.AppendLine();
    sb.AppendLine($"    /// <summary>Intercepts instance method across all instances with <see cref=\"{fnUserXml}\"/>.</summary>");
    sb.AppendLine($"    public DetourScope InterceptUnsafe({fnUser} replacement)");
    sb.AppendLine("    {");
    sb.AppendLine("        if (replacement == null) throw new ArgumentNullException(nameof(replacement));");
    sb.AppendLine($"        var actual = (TTarget self{fnParamDecl}) => replacement.Invoke({fnInvokeArgs});");
    sb.AppendLine("        return interceptor.Intercept(actual);");
    sb.AppendLine("    }");
    sb.AppendLine("}");
    sb.AppendLine();
    sb.AppendLine("/// <summary>Extension methods for instance interceptors.</summary>");
    sb.AppendLine("public static partial class InstanceInterceptorExtensions");
    sb.AppendLine("{");
    sb.AppendLine($"    /// <summary>Extension method for instance method interceptor.</summary>");
    sb.AppendLine($"    public static {className}<{classTparams}> InstanceJitest<{extTparams}>(this TTarget instance, string methodName, out {fnUser}? originalMethod)");
    sb.AppendLine("    {");
    sb.AppendLine("        if (instance == null) throw new ArgumentNullException(nameof(instance));");
    sb.AppendLine($"        var interceptor = instance.Jitest<{fnUser}>(methodName, out originalMethod);");
    sb.AppendLine($"        return new {className}<{classTparams}>(interceptor, originalMethod);");
    sb.AppendLine("    }");
    sb.AppendLine("}");

    File.WriteAllText(Path.Combine(dir, $"{className}{FileExtension}"), sb.ToString());
}
