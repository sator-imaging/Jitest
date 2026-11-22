// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

namespace Jitest.Test;

public class StaticTestTargets
{
    public static int StaticVoid_0_Count;
    public static void StaticVoid_0() => StaticVoid_0_Count++;
    public static int StaticVoid_1_Count;
    public static void StaticVoid_1(int t1) => StaticVoid_1_Count += t1;
    public static int StaticVoid_2_Count;
    public static void StaticVoid_2(int t1, int t2) => StaticVoid_2_Count += t1 + t2;
    public static int StaticVoid_3_Count;
    public static void StaticVoid_3(int t1, int t2, int t3) => StaticVoid_3_Count += t1 + t2 + t3;
    public static int StaticVoid_4_Count;
    public static void StaticVoid_4(int t1, int t2, int t3, int t4) => StaticVoid_4_Count += t1 + t2 + t3 + t4;
    public static int StaticVoid_5_Count;
    public static void StaticVoid_5(int t1, int t2, int t3, int t4, int t5) => StaticVoid_5_Count += t1 + t2 + t3 + t4 + t5;
    public static int StaticVoid_6_Count;
    public static void StaticVoid_6(int t1, int t2, int t3, int t4, int t5, int t6) => StaticVoid_6_Count += t1 + t2 + t3 + t4 + t5 + t6;
    public static int StaticVoid_7_Count;
    public static void StaticVoid_7(int t1, int t2, int t3, int t4, int t5, int t6, int t7) => StaticVoid_7_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7;
    public static int StaticVoid_8_Count;
    public static void StaticVoid_8(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8) => StaticVoid_8_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8;
    public static int StaticVoid_9_Count;
    public static void StaticVoid_9(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9) => StaticVoid_9_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9;
    public static int StaticVoid_10_Count;
    public static void StaticVoid_10(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10) => StaticVoid_10_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10;
    public static int StaticVoid_11_Count;
    public static void StaticVoid_11(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11) => StaticVoid_11_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11;
    public static int StaticVoid_12_Count;
    public static void StaticVoid_12(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12) => StaticVoid_12_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12;
    public static int StaticVoid_13_Count;
    public static void StaticVoid_13(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13) => StaticVoid_13_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13;
    public static int StaticVoid_14_Count;
    public static void StaticVoid_14(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14) => StaticVoid_14_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14;
    public static int StaticVoid_15_Count;
    public static void StaticVoid_15(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15) => StaticVoid_15_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + t15;
    public static int StaticGetInt_1() => 100 + 1;
    public static int StaticGetInt_2(int t1) => t1 + 2 * 10;
    public static int StaticGetInt_3(int t1, int t2) => t1 + t2 + 3 * 10;
    public static int StaticGetInt_4(int t1, int t2, int t3) => t1 + t2 + t3 + 4 * 10;
    public static int StaticGetInt_5(int t1, int t2, int t3, int t4) => t1 + t2 + t3 + t4 + 5 * 10;
    public static int StaticGetInt_6(int t1, int t2, int t3, int t4, int t5) => t1 + t2 + t3 + t4 + t5 + 6 * 10;
    public static int StaticGetInt_7(int t1, int t2, int t3, int t4, int t5, int t6) => t1 + t2 + t3 + t4 + t5 + t6 + 7 * 10;
    public static int StaticGetInt_8(int t1, int t2, int t3, int t4, int t5, int t6, int t7) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + 8 * 10;
    public static int StaticGetInt_9(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + 9 * 10;
    public static int StaticGetInt_10(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + 10 * 10;
    public static int StaticGetInt_11(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + 11 * 10;
    public static int StaticGetInt_12(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + 12 * 10;
    public static int StaticGetInt_13(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + 13 * 10;
    public static int StaticGetInt_14(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + 14 * 10;
    public static int StaticGetInt_15(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + 15 * 10;
    public static int StaticGetInt_16(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + t15 + 16 * 10;
}

public class InstanceTestTargets
{
    public int Void_0_Count;
    public void Void_0() => Void_0_Count++;
    public int Void_1_Count;
    public void Void_1(int t1) => Void_1_Count += t1;
    public int Void_2_Count;
    public void Void_2(int t1, int t2) => Void_2_Count += t1 + t2;
    public int Void_3_Count;
    public void Void_3(int t1, int t2, int t3) => Void_3_Count += t1 + t2 + t3;
    public int Void_4_Count;
    public void Void_4(int t1, int t2, int t3, int t4) => Void_4_Count += t1 + t2 + t3 + t4;
    public int Void_5_Count;
    public void Void_5(int t1, int t2, int t3, int t4, int t5) => Void_5_Count += t1 + t2 + t3 + t4 + t5;
    public int Void_6_Count;
    public void Void_6(int t1, int t2, int t3, int t4, int t5, int t6) => Void_6_Count += t1 + t2 + t3 + t4 + t5 + t6;
    public int Void_7_Count;
    public void Void_7(int t1, int t2, int t3, int t4, int t5, int t6, int t7) => Void_7_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7;
    public int Void_8_Count;
    public void Void_8(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8) => Void_8_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8;
    public int Void_9_Count;
    public void Void_9(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9) => Void_9_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9;
    public int Void_10_Count;
    public void Void_10(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10) => Void_10_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10;
    public int Void_11_Count;
    public void Void_11(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11) => Void_11_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11;
    public int Void_12_Count;
    public void Void_12(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12) => Void_12_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12;
    public int Void_13_Count;
    public void Void_13(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13) => Void_13_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13;
    public int Void_14_Count;
    public void Void_14(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14) => Void_14_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14;
    public int Void_15_Count;
    public void Void_15(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15) => Void_15_Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + t15;
    public int GetInt_1() => 200 + 1;
    public int GetInt_2(int t1) => t1 + 2 * 10;
    public int GetInt_3(int t1, int t2) => t1 + t2 + 3 * 10;
    public int GetInt_4(int t1, int t2, int t3) => t1 + t2 + t3 + 4 * 10;
    public int GetInt_5(int t1, int t2, int t3, int t4) => t1 + t2 + t3 + t4 + 5 * 10;
    public int GetInt_6(int t1, int t2, int t3, int t4, int t5) => t1 + t2 + t3 + t4 + t5 + 6 * 10;
    public int GetInt_7(int t1, int t2, int t3, int t4, int t5, int t6) => t1 + t2 + t3 + t4 + t5 + t6 + 7 * 10;
    public int GetInt_8(int t1, int t2, int t3, int t4, int t5, int t6, int t7) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + 8 * 10;
    public int GetInt_9(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + 9 * 10;
    public int GetInt_10(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + 10 * 10;
    public int GetInt_11(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + 11 * 10;
    public int GetInt_12(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + 12 * 10;
    public int GetInt_13(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + 13 * 10;
    public int GetInt_14(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + 14 * 10;
    public int GetInt_15(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + 15 * 10;
    public int GetInt_16(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + t15 + 16 * 10;
}
