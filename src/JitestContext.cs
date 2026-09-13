// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Threading;

namespace Jitest;

/// <summary>
/// Test context manager.
/// </summary>
public static class JitestContext
{
    static readonly SemaphoreSlim Gate = new(initialCount: 1, maxCount: 1);

    /// <inheritdoc cref="ScopeDisposable"/>
    /// <returns><see cref="IDisposable"/></returns>
    public static ScopeDisposable BeginTestMethod()
    {
        Gate.Wait();
        return new();
    }

    /// <summary>
    /// Ensures that other test methods are not started while `using` statement scope is active.
    /// </summary>
    public readonly struct ScopeDisposable : IDisposable
    {
        /// <inheritdoc/>
        public void Dispose() => Gate.Release();
    }
}
