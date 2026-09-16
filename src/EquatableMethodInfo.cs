// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Jitest;

// TODO: MethodInfo.Equals checks reference equality.
//       Not sure Type.GetMethod always returns the same or not.
[StructLayout(LayoutKind.Auto)]
internal readonly struct EquatableMethodInfo : IEquatable<EquatableMethodInfo>
{
    readonly int hashCode;

    EquatableMethodInfo(MethodInfo methodInfo)
    {
        if (methodInfo == null)
        {
            JitestException.ThrowArgumentNull(nameof(methodInfo));
        }

        this.MethodInfo = methodInfo;
        // Don't include parameters!! Array hash is not based on its contents!!
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

        if (otherInfo.DeclaringType != info.DeclaringType || otherInfo.Name != info.Name)
        {
            return false;
        }

        var params1 = info.GetParameters();
        var params2 = otherInfo.GetParameters();

        if (params1.Length != params2.Length)
        {
            return false;
        }

        return ArrayEquals(params1, params2);

        static bool ArrayEquals(ParameterInfo[] params1, ParameterInfo[] params2)
        {
            for (int i = 0; i < params1.Length; i++)
            {
                if (params1[i].ParameterType != params2[i].ParameterType)
                {
                    return false;
                }
            }

            return true;
        }
    }
    public static bool operator ==(EquatableMethodInfo left, EquatableMethodInfo right) => left.Equals(right);
    public static bool operator !=(EquatableMethodInfo left, EquatableMethodInfo right) => !(left == right);
}
