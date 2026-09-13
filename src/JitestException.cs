// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Diagnostics.CodeAnalysis;

namespace Jitest;

internal sealed class JitestException : Exception
{
    public JitestException(string message, Exception? inner = null) : base(message, inner) { }

    [DoesNotReturn] public static void Throw(string message) => throw new JitestException(message);
    [DoesNotReturn] public static void ThrowArgumentNull(string paramName) => throw new JitestException($"Argument is null: {paramName}");
}
