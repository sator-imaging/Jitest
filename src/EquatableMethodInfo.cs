// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Jitest;

// TODO: MethodInfo.Equals checks reference equality.
//       Not sure Type.GetMethod always returns the same or not.
[StructLayout(LayoutKind.Auto)]
internal readonly struct EquatableMethodInfo : IEquatable<EquatableMethodInfo>
{
    EquatableMethodInfo(MethodInfo methodInfo)
    {
        if (methodInfo == null)
        {
            JitestException.ThrowArgumentNull(nameof(methodInfo));
        }

        this.MethodInfo = methodInfo;
    }

    public static implicit operator EquatableMethodInfo(MethodInfo info) => new(info);

    internal MethodInfo MethodInfo { get; }

    // Don't include parameters!! Array hash is not based on its contents!!
    public override int GetHashCode() => HashCode.Combine(this.MethodInfo.DeclaringType, this.MethodInfo.Name);
    public override bool Equals(object? obj) => obj is EquatableMethodInfo other && this.Equals(other);
    public bool Equals(EquatableMethodInfo other)
    {
        return other.MethodInfo.DeclaringType == MethodInfo.DeclaringType
            && other.MethodInfo.Name == MethodInfo.Name
            && other.MethodInfo.GetParameters().Select(p => p.ParameterType).SequenceEqual(MethodInfo.GetParameters().Select(p => p.ParameterType));
    }
    public static bool operator ==(EquatableMethodInfo left, EquatableMethodInfo right) => left.Equals(right);
    public static bool operator !=(EquatableMethodInfo left, EquatableMethodInfo right) => !(left == right);
}
