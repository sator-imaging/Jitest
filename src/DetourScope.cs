// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using MonoMod.RuntimeDetour;
using System;
using System.Reflection;

namespace Jitest;

/// <summary>
/// Method interception lifetime manager.
/// </summary>
public sealed class DetourScope : IDisposable
{
    static readonly object HookSentinel = new();

    readonly Hook hook;
    bool disposed;

    DetourScope(Hook hook)
    {
        this.hook = hook;
    }

    internal static DetourScope Create(MethodInfo target, Delegate replacement, object? instance)
    {
        if (target.ContainsGenericParameters)
        {
            JitestException.Throw($"Open generic methods cannot be detoured directly: {target}");
        }

        if (target.IsAbstract)
        {
            JitestException.Throw($"Abstract methods cannot be detoured directly: {target}");
        }

        if (target.IsConstructor)
        {
            JitestException.Throw($"Constructors are not supported: {target}");
        }

        if (replacement.Method.ContainsGenericParameters)
        {
            JitestException.Throw($"Open generic replacement delegates are not supported: {replacement.Method}");
        }

        try
        {
            lock (HookSentinel)
            {
                return instance == null
                    ? new DetourScope(new Hook(target, replacement))
                    : new DetourScope(new Hook(target, replacement));  // TODO: cannot intercept instance method by --> .Method, instance));
            }
        }
        catch (Exception ex)
        {
            throw new JitestException(
                $"Method signature doesn't match (Did you forget a hidden first argument for the instance method?): {ex.Message}",
                inner: ex);
        }
    }

    void IDisposable.Dispose()
    {
        if (disposed)
        {
            return;
        }
        disposed = true;

        try
        {
            // Synchronize hook disposal with HookSentinel to prevent issues during concurrent test execution.
            lock (HookSentinel)
            {
                hook.Dispose();
            }
        }
        finally
        {
            // Release semaphore for method interception here (if required)
        }

        GC.SuppressFinalize(this);
    }
}
