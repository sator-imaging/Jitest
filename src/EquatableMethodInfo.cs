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
    readonly Type type;
    readonly string methodName;
    readonly Type[] parameters;

    EquatableMethodInfo(Type? type, string methodName, Type[] parameters, MethodInfo methodInfo)
    {
        if (type == null)
        {
            JitestException.ThrowArgumentNull(nameof(type));
        }

        this.type = type;
        this.methodName = methodName;
        this.parameters = parameters;
        this.MethodInfo = methodInfo;
    }

    public static implicit operator EquatableMethodInfo(MethodInfo info)
    {
        var rawParams = info.GetParameters();
        var parameters = new Type[rawParams.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            parameters[i] = rawParams[i].ParameterType;
        }

        return new(info.DeclaringType, info.Name, parameters, info);
    }

    internal MethodInfo MethodInfo { get; }

    // Don't include parameters!! Array hash is not based on its contents!!
    public override int GetHashCode() => HashCode.Combine(this.type, this.methodName);
    public override bool Equals(object? obj) => obj is EquatableMethodInfo other && this.Equals(other);
    public bool Equals(EquatableMethodInfo other)
    {
        return other.type == type
            && other.methodName == methodName
            && other.parameters.SequenceEqual(parameters);
    }
    public static bool operator ==(EquatableMethodInfo left, EquatableMethodInfo right) => left.Equals(right);
    public static bool operator !=(EquatableMethodInfo left, EquatableMethodInfo right) => !(left == right);
}
