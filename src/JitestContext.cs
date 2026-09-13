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

    /// <summary>
    /// Acquires a global lock scope to prevent participating test methods from executing concurrently.
    /// </summary>
    /// <remarks>
    /// <para>
    /// While acquiring a <see cref="JitestContext"/> scope per test method serializes test execution, the recommended best practice is to perform global test initialization:
    /// </para>
    /// <para>
    /// Install all required interceptions once during global test framework setup, configure fixed behaviors/instances (e.g., mock implementations), and keep the interceptions active without modification while tests run.
    /// Because tests only select pre-configured instances and do not replace method behaviors at runtime, tests can safely execute concurrently in parallel without needing per-test locks.
    /// </para>
    /// <para>
    /// Serializing test methods via <see cref="BeginTestMethod"/> should be used when dynamic runtime interception during test execution is strictly required. Note that every test method that can reach an intercepted method (or share process-wide state affected by hooks) must acquire this scope to ensure proper isolation.
    /// </para>
    /// </remarks>
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
