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
    // Don't include parameters!! Array hash is not based on its contents!!
    readonly int hashCode;

    EquatableMethodInfo(MethodInfo methodInfo)
    {
        if (methodInfo == null)
        {
            JitestException.ThrowArgumentNull(nameof(methodInfo));
        }

        this.MethodInfo = methodInfo;
        this.hashCode = HashCode.Combine(methodInfo.DeclaringType, methodInfo.Name);
    }

    public static implicit operator EquatableMethodInfo(MethodInfo info) => new(info);

    internal MethodInfo MethodInfo { get; }

    public override int GetHashCode() => this.hashCode;
    public override bool Equals(object? obj) => obj is EquatableMethodInfo other && this.Equals(other);
    public bool Equals(EquatableMethodInfo other)
    {
        var info = this.MethodInfo;
        var otherInfo = other.MethodInfo;

        return otherInfo.DeclaringType == info.DeclaringType
            && otherInfo.Name == info.Name
            && otherInfo.GetParameters().Select(p => p.ParameterType).SequenceEqual(info.GetParameters().Select(p => p.ParameterType));
    }
    public static bool operator ==(EquatableMethodInfo left, EquatableMethodInfo right) => left.Equals(right);
    public static bool operator !=(EquatableMethodInfo left, EquatableMethodInfo right) => !(left == right);
}
