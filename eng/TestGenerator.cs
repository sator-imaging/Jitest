// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#:property PublishAot=false

using System;
using System.IO;
using System.Linq;
using System.Text;

const string FileExtension = ".g.cs";

string rootDir = Directory.GetCurrentDirectory();
string testDir = Path.Combine(rootDir, "test");
string staticTestDir = Path.Combine(testDir, "Generated_StaticTests");
string instanceTestDir = Path.Combine(testDir, "Generated_InstanceTests");

Directory.CreateDirectory(staticTestDir);
Directory.CreateDirectory(instanceTestDir);

foreach (string file in Directory.GetFiles(staticTestDir)) File.Delete(file);
foreach (string file in Directory.GetFiles(instanceTestDir)) File.Delete(file);

string licenseHeader = @"// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest";

string header = $@"{licenseHeader}

#nullable enable

using Jitest;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Jitest.Test;
";

// Generate TestTargets.g.cs
GenerateTestTargets(testDir, licenseHeader);

for (int n = 0; n <= 16; n++)
{
    if (n == 0)
    {
        GenerateStaticActionTest(staticTestDir, header, 0, "StaticInterceptorActionT0Test", "StaticVoid_0", "StaticTestTargets", "Action", "", "");
        GenerateInstanceActionTest(instanceTestDir, header, 0, "InstanceInterceptorActionT0Test", "Void_0", "InstanceTestTargets", "Action", "", "");
    }
    else if (n <= 15)
    {
        // Action n parameters (int t1, ..., int tn)
        string[] pTypes = Enumerable.Repeat("int", n).ToArray();
        string extTargsAct = "StaticTestTargets, " + string.Join(", ", pTypes);
        string extTargsInstanceAct = "InstanceTestTargets, " + string.Join(", ", pTypes);
        string actDelegateType = $"Action<{string.Join(", ", pTypes)}>";

        string[] callArgs = Enumerable.Range(1, n).Select(i => i.ToString()).ToArray();
        string callArgsStr = string.Join(", ", callArgs);

        string[] lambdaParams = Enumerable.Range(1, n).Select(i => $"t{i}").ToArray();
        string lambdaParamsStr = string.Join(", ", lambdaParams);

        GenerateStaticActionTest(staticTestDir, header, n, $"StaticInterceptorActionT{n}Test", $"StaticVoid_{n}", extTargsAct, actDelegateType, callArgsStr, lambdaParamsStr);

        // Func n: n-1 params, return type int (Func<int, ..., int> has n types total)
        int funcArgCount = n - 1;
        string[] fnPTypes = Enumerable.Repeat("int", n).ToArray(); // n total types (n-1 inputs + 1 return)
        string extTargsFn = "StaticTestTargets, " + string.Join(", ", fnPTypes);
        string extTargsInstanceFn = "InstanceTestTargets, " + string.Join(", ", fnPTypes);
        string fnDelegateType = $"Func<{string.Join(", ", fnPTypes)}>";

        string[] fnCallArgs = Enumerable.Range(1, funcArgCount).Select(i => i.ToString()).ToArray();
        string fnCallArgsStr = string.Join(", ", fnCallArgs);

        string[] fnLambdaParams = Enumerable.Range(1, funcArgCount).Select(i => $"t{i}").ToArray();
        string fnLambdaParamsStr = string.Join(", ", fnLambdaParams);

        GenerateStaticFuncTest(staticTestDir, header, n, $"StaticInterceptorFuncT{n}Test", $"StaticGetInt_{n}", extTargsFn, fnDelegateType, fnCallArgsStr, fnLambdaParamsStr);

        GenerateInstanceActionTest(instanceTestDir, header, n, $"InstanceInterceptorActionT{n}Test", $"Void_{n}", extTargsInstanceAct, actDelegateType, callArgsStr, lambdaParamsStr);
        GenerateInstanceFuncTest(instanceTestDir, header, n, $"InstanceInterceptorFuncT{n}Test", $"GetInt_{n}", extTargsInstanceFn, fnDelegateType, fnCallArgsStr, fnLambdaParamsStr);
    }
    else // n == 16 (Func16 only)
    {
        int funcArgCount = 15;
        string[] fnPTypes = Enumerable.Repeat("int", 16).ToArray();
        string extTargsFn = "StaticTestTargets, " + string.Join(", ", fnPTypes);
        string extTargsInstanceFn = "InstanceTestTargets, " + string.Join(", ", fnPTypes);
        string fnDelegateType = $"Func<{string.Join(", ", fnPTypes)}>";

        string[] fnCallArgs = Enumerable.Range(1, funcArgCount).Select(i => i.ToString()).ToArray();
        string fnCallArgsStr = string.Join(", ", fnCallArgs);

        string[] fnLambdaParams = Enumerable.Range(1, funcArgCount).Select(i => $"t{i}").ToArray();
        string fnLambdaParamsStr = string.Join(", ", fnLambdaParams);

        GenerateStaticFuncTest(staticTestDir, header, n, $"StaticInterceptorFuncT{n}Test", $"StaticGetInt_{n}", extTargsFn, fnDelegateType, fnCallArgsStr, fnLambdaParamsStr);
        GenerateInstanceFuncTest(instanceTestDir, header, n, $"InstanceInterceptorFuncT{n}Test", $"GetInt_{n}", extTargsInstanceFn, fnDelegateType, fnCallArgsStr, fnLambdaParamsStr);
    }
}

Console.WriteLine("Test generation completed successfully!");

static void GenerateTestTargets(string testDir, string licenseHeader)
{
    StringBuilder sb = new StringBuilder();
    sb.AppendLine(licenseHeader);
    sb.AppendLine();
    sb.AppendLine("#nullable enable");
    sb.AppendLine();
    sb.AppendLine("namespace Jitest.Test;");
    sb.AppendLine();

    // StaticTestTargets must not be a 'static class' because static classes cannot be used as type arguments <TTarget>
    sb.AppendLine("public class StaticTestTargets");
    sb.AppendLine("{");
    for (int n = 0; n <= 15; n++)
    {
        sb.AppendLine($"    public static int StaticVoid_{n}Count;");
        if (n == 0)
        {
            sb.AppendLine("    public static void StaticVoid_0() => StaticVoid_0Count++;");
        }
        else
        {
            string pDecl = string.Join(", ", Enumerable.Range(1, n).Select(i => $"int t{i}"));
            string pSum = string.Join(" + ", Enumerable.Range(1, n).Select(i => $"t{i}"));
            sb.AppendLine($"    public static void StaticVoid_{n}({pDecl}) => StaticVoid_{n}Count += {pSum};");
        }
    }
    for (int n = 1; n <= 16; n++)
    {
        int argCount = n - 1;
        if (argCount == 0)
        {
            sb.AppendLine($"    public static int StaticGetInt_{n}() => 100 + {n};");
        }
        else
        {
            string pDecl = string.Join(", ", Enumerable.Range(1, argCount).Select(i => $"int t{i}"));
            string pSum = string.Join(" + ", Enumerable.Range(1, argCount).Select(i => $"t{i}"));
            sb.AppendLine($"    public static int StaticGetInt_{n}({pDecl}) => {pSum} + {n} * 10;");
        }
    }
    sb.AppendLine("}");
    sb.AppendLine();

    // InstanceTestTargets
    sb.AppendLine("public class InstanceTestTargets");
    sb.AppendLine("{");
    for (int n = 0; n <= 15; n++)
    {
        sb.AppendLine($"    public int Void_{n}Count;");
        if (n == 0)
        {
            sb.AppendLine("    public void Void_0() => Void_0Count++;");
        }
        else
        {
            string pDecl = string.Join(", ", Enumerable.Range(1, n).Select(i => $"int t{i}"));
            string pSum = string.Join(" + ", Enumerable.Range(1, n).Select(i => $"t{i}"));
            sb.AppendLine($"    public void Void_{n}({pDecl}) => Void_{n}Count += {pSum};");
        }
    }
    for (int n = 1; n <= 16; n++)
    {
        int argCount = n - 1;
        if (argCount == 0)
        {
            sb.AppendLine($"    public int GetInt_{n}() => 200 + {n};");
        }
        else
        {
            string pDecl = string.Join(", ", Enumerable.Range(1, argCount).Select(i => $"int t{i}"));
            string pSum = string.Join(" + ", Enumerable.Range(1, argCount).Select(i => $"t{i}"));
            sb.AppendLine($"    public int GetInt_{n}({pDecl}) => {pSum} + {n} * 10;");
        }
    }
    sb.AppendLine("}");

    File.WriteAllText(Path.Combine(testDir, $"TestTargets{FileExtension}"), sb.ToString());
}

static void GenerateStaticActionTest(string dir, string header, int n, string className, string methodName, string extTargs, string delegateType, string callArgs, string lambdaParams)
{
    StringBuilder sb = new StringBuilder();
    sb.AppendLine(header);
    sb.AppendLine("[NotInParallel]");
    sb.AppendLine($"public class {className}");
    sb.AppendLine("{");
    sb.AppendLine("    [Test]");
    sb.AppendLine($"    public async Task Test{methodName}()");
    sb.AppendLine("    {");
    sb.AppendLine($"        StaticTestTargets.{methodName}Count = 0;");
    sb.AppendLine("        bool intercepted = false;");
    sb.AppendLine($"        var interceptor = typeof(StaticTestTargets).StaticJitest<{extTargs}>(\"{methodName}\", out {delegateType} originalMethod);");
    sb.AppendLine();
    sb.AppendLine($"        using (interceptor.Intercept(({lambdaParams}) => {{ intercepted = true; }}))");
    sb.AppendLine("        {");
    sb.AppendLine($"            StaticTestTargets.{methodName}({callArgs});");
    sb.AppendLine("            await Assert.That(intercepted).IsTrue();");
    sb.AppendLine($"            await Assert.That(StaticTestTargets.{methodName}Count).IsEqualTo(0);");
    sb.AppendLine();
    sb.AppendLine($"            originalMethod.Invoke({callArgs});");
    sb.AppendLine($"            await Assert.That(StaticTestTargets.{methodName}Count).IsGreaterThan(0);");
    sb.AppendLine("        }");
    sb.AppendLine();
    sb.AppendLine($"        StaticTestTargets.{methodName}Count = 0;");
    sb.AppendLine($"        StaticTestTargets.{methodName}({callArgs});");
    sb.AppendLine($"        await Assert.That(StaticTestTargets.{methodName}Count).IsGreaterThan(0);");
    sb.AppendLine("    }");
    sb.AppendLine("}");

    File.WriteAllText(Path.Combine(dir, $"{className}{FileExtension}"), sb.ToString());
}

static void GenerateStaticFuncTest(string dir, string header, int n, string className, string methodName, string extTargs, string delegateType, string callArgs, string lambdaParams)
{
    StringBuilder sb = new StringBuilder();
    sb.AppendLine(header);
    sb.AppendLine("[NotInParallel]");
    sb.AppendLine($"public class {className}");
    sb.AppendLine("{");
    sb.AppendLine("    [Test]");
    sb.AppendLine($"    public async Task Test{methodName}()");
    sb.AppendLine("    {");
    sb.AppendLine($"        var originalVal = StaticTestTargets.{methodName}({callArgs});");
    sb.AppendLine($"        var interceptor = typeof(StaticTestTargets).StaticJitest<{extTargs}>(\"{methodName}\", out {delegateType} originalMethod);");
    sb.AppendLine();
    sb.AppendLine($"        using (interceptor.Intercept(({lambdaParams}) => 9999))");
    sb.AppendLine("        {");
    sb.AppendLine($"            var res = StaticTestTargets.{methodName}({callArgs});");
    sb.AppendLine("            await Assert.That(res).IsEqualTo(9999);");
    sb.AppendLine();
    sb.AppendLine($"            var origRes = originalMethod.Invoke({callArgs});");
    sb.AppendLine("            await Assert.That(origRes).IsEqualTo(originalVal);");
    sb.AppendLine("        }");
    sb.AppendLine();
    sb.AppendLine($"        var afterRes = StaticTestTargets.{methodName}({callArgs});");
    sb.AppendLine("        await Assert.That(afterRes).IsEqualTo(originalVal);");
    sb.AppendLine("    }");
    sb.AppendLine("}");

    File.WriteAllText(Path.Combine(dir, $"{className}{FileExtension}"), sb.ToString());
}

static void GenerateInstanceActionTest(string dir, string header, int n, string className, string methodName, string extTargs, string delegateType, string callArgs, string lambdaParams)
{
    StringBuilder sb = new StringBuilder();
    sb.AppendLine(header);
    sb.AppendLine("[NotInParallel]");
    sb.AppendLine($"public class {className}");
    sb.AppendLine("{");
    sb.AppendLine("    [Test]");
    sb.AppendLine($"    public async Task Test{methodName}()");
    sb.AppendLine("    {");
    sb.AppendLine("        var target = new InstanceTestTargets();");
    sb.AppendLine("        var other = new InstanceTestTargets();");
    sb.AppendLine($"        var interceptor = target.InstanceJitest<{extTargs}>(\"{methodName}\", out {delegateType} originalMethod);");
    sb.AppendLine();
    sb.AppendLine("        // 1. Intercept specific instance");
    sb.AppendLine("        bool targetIntercepted = false;");
    sb.AppendLine($"        using (interceptor.Intercept(target, ({lambdaParams}) => {{ targetIntercepted = true; }}))");
    sb.AppendLine("        {");
    sb.AppendLine($"            target.{methodName}({callArgs});");
    sb.AppendLine("            await Assert.That(targetIntercepted).IsTrue();");
    sb.AppendLine($"            await Assert.That(target.{methodName}Count).IsEqualTo(0);");
    sb.AppendLine();
    sb.AppendLine($"            other.{methodName}({callArgs});");
    sb.AppendLine($"            await Assert.That(other.{methodName}Count).IsGreaterThan(0);");
    sb.AppendLine("        }");
    sb.AppendLine();
    sb.AppendLine("        // 2. Intercept unsafe (all instances)");
    sb.AppendLine("        bool unsafeIntercepted = false;");
    sb.AppendLine($"        using (interceptor.InterceptUnsafe(({lambdaParams}) => {{ unsafeIntercepted = true; }}))");
    sb.AppendLine("        {");
    sb.AppendLine($"            target.{methodName}({callArgs});");
    sb.AppendLine("            await Assert.That(unsafeIntercepted).IsTrue();");
    sb.AppendLine();
    sb.AppendLine("            unsafeIntercepted = false;");
    sb.AppendLine($"            other.{methodName}({callArgs});");
    sb.AppendLine("            await Assert.That(unsafeIntercepted).IsTrue();");
    sb.AppendLine("        }");
    sb.AppendLine();
    sb.AppendLine("        // 3. Original method delegate");
    sb.AppendLine($"        int targetCountBefore = target.{methodName}Count;");
    sb.AppendLine($"        originalMethod.Invoke({callArgs});");
    sb.AppendLine($"        await Assert.That(target.{methodName}Count).IsGreaterThan(targetCountBefore);");
    sb.AppendLine();
    sb.AppendLine("        // 4. Reverted after disposal");
    sb.AppendLine($"        int otherCountBefore = other.{methodName}Count;");
    sb.AppendLine($"        other.{methodName}({callArgs});");
    sb.AppendLine($"        await Assert.That(other.{methodName}Count).IsGreaterThan(otherCountBefore);");
    sb.AppendLine("    }");
    sb.AppendLine("}");

    File.WriteAllText(Path.Combine(dir, $"{className}{FileExtension}"), sb.ToString());
}

static void GenerateInstanceFuncTest(string dir, string header, int n, string className, string methodName, string extTargs, string delegateType, string callArgs, string lambdaParams)
{
    StringBuilder sb = new StringBuilder();
    sb.AppendLine(header);
    sb.AppendLine("[NotInParallel]");
    sb.AppendLine($"public class {className}");
    sb.AppendLine("{");
    sb.AppendLine("    [Test]");
    sb.AppendLine($"    public async Task Test{methodName}()");
    sb.AppendLine("    {");
    sb.AppendLine("        var target = new InstanceTestTargets();");
    sb.AppendLine("        var other = new InstanceTestTargets();");
    sb.AppendLine($"        var targetOriginal = target.{methodName}({callArgs});");
    sb.AppendLine($"        var otherOriginal = other.{methodName}({callArgs});");
    sb.AppendLine($"        var interceptor = target.InstanceJitest<{extTargs}>(\"{methodName}\", out {delegateType} originalMethod);");
    sb.AppendLine();
    sb.AppendLine("        // 1. Intercept specific instance");
    sb.AppendLine($"        using (interceptor.Intercept(target, ({lambdaParams}) => 8888))");
    sb.AppendLine("        {");
    sb.AppendLine($"            var targetRes = target.{methodName}({callArgs});");
    sb.AppendLine("            await Assert.That(targetRes).IsEqualTo(8888);");
    sb.AppendLine();
    sb.AppendLine($"            var otherRes = other.{methodName}({callArgs});");
    sb.AppendLine("            await Assert.That(otherRes).IsEqualTo(otherOriginal);");
    sb.AppendLine("        }");
    sb.AppendLine();
    sb.AppendLine("        // 2. Intercept unsafe (all instances)");
    sb.AppendLine($"        using (interceptor.InterceptUnsafe(({lambdaParams}) => 7777))");
    sb.AppendLine("        {");
    sb.AppendLine($"            var targetRes = target.{methodName}({callArgs});");
    sb.AppendLine("            await Assert.That(targetRes).IsEqualTo(7777);");
    sb.AppendLine();
    sb.AppendLine($"            var otherRes = other.{methodName}({callArgs});");
    sb.AppendLine("            await Assert.That(otherRes).IsEqualTo(7777);");
    sb.AppendLine("        }");
    sb.AppendLine();
    sb.AppendLine("        // 3. Original method delegate");
    sb.AppendLine($"        var origRes = originalMethod.Invoke({callArgs});");
    sb.AppendLine("        await Assert.That(origRes).IsEqualTo(targetOriginal);");
    sb.AppendLine();
    sb.AppendLine("        // 4. Reverted after disposal");
    sb.AppendLine($"        var afterRes = target.{methodName}({callArgs});");
    sb.AppendLine("        await Assert.That(afterRes).IsEqualTo(targetOriginal);");
    sb.AppendLine("    }");
    sb.AppendLine("}");

    File.WriteAllText(Path.Combine(dir, $"{className}{FileExtension}"), sb.ToString());
}
