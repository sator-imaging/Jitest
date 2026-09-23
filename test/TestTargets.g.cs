// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

namespace Jitest.Test;

public class StaticTestTargets
{
    public static int Action0Count;
    public static void Action0() => Action0Count++;
    public static int Action1Count;
    public static void Action1(int t1) => Action1Count += t1;
    public static int Action2Count;
    public static void Action2(int t1, int t2) => Action2Count += t1 + t2;
    public static int Action3Count;
    public static void Action3(int t1, int t2, int t3) => Action3Count += t1 + t2 + t3;
    public static int Action4Count;
    public static void Action4(int t1, int t2, int t3, int t4) => Action4Count += t1 + t2 + t3 + t4;
    public static int Action5Count;
    public static void Action5(int t1, int t2, int t3, int t4, int t5) => Action5Count += t1 + t2 + t3 + t4 + t5;
    public static int Action6Count;
    public static void Action6(int t1, int t2, int t3, int t4, int t5, int t6) => Action6Count += t1 + t2 + t3 + t4 + t5 + t6;
    public static int Action7Count;
    public static void Action7(int t1, int t2, int t3, int t4, int t5, int t6, int t7) => Action7Count += t1 + t2 + t3 + t4 + t5 + t6 + t7;
    public static int Action8Count;
    public static void Action8(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8) => Action8Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8;
    public static int Action9Count;
    public static void Action9(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9) => Action9Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9;
    public static int Action10Count;
    public static void Action10(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10) => Action10Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10;
    public static int Action11Count;
    public static void Action11(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11) => Action11Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11;
    public static int Action12Count;
    public static void Action12(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12) => Action12Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12;
    public static int Action13Count;
    public static void Action13(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13) => Action13Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13;
    public static int Action14Count;
    public static void Action14(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14) => Action14Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14;
    public static int Action15Count;
    public static void Action15(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15) => Action15Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + t15;
    public static int Func1() => 100 + 1;
    public static int Func2(int t1) => t1 + 2 * 10;
    public static int Func3(int t1, int t2) => t1 + t2 + 3 * 10;
    public static int Func4(int t1, int t2, int t3) => t1 + t2 + t3 + 4 * 10;
    public static int Func5(int t1, int t2, int t3, int t4) => t1 + t2 + t3 + t4 + 5 * 10;
    public static int Func6(int t1, int t2, int t3, int t4, int t5) => t1 + t2 + t3 + t4 + t5 + 6 * 10;
    public static int Func7(int t1, int t2, int t3, int t4, int t5, int t6) => t1 + t2 + t3 + t4 + t5 + t6 + 7 * 10;
    public static int Func8(int t1, int t2, int t3, int t4, int t5, int t6, int t7) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + 8 * 10;
    public static int Func9(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + 9 * 10;
    public static int Func10(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + 10 * 10;
    public static int Func11(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + 11 * 10;
    public static int Func12(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + 12 * 10;
    public static int Func13(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + 13 * 10;
    public static int Func14(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + 14 * 10;
    public static int Func15(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + 15 * 10;
    public static int Func16(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + t15 + 16 * 10;
}

public class InstanceTestTargets
{
    public int Action0Count;
    public void Action0() => Action0Count++;
    public int Action1Count;
    public void Action1(int t1) => Action1Count += t1;
    public int Action2Count;
    public void Action2(int t1, int t2) => Action2Count += t1 + t2;
    public int Action3Count;
    public void Action3(int t1, int t2, int t3) => Action3Count += t1 + t2 + t3;
    public int Action4Count;
    public void Action4(int t1, int t2, int t3, int t4) => Action4Count += t1 + t2 + t3 + t4;
    public int Action5Count;
    public void Action5(int t1, int t2, int t3, int t4, int t5) => Action5Count += t1 + t2 + t3 + t4 + t5;
    public int Action6Count;
    public void Action6(int t1, int t2, int t3, int t4, int t5, int t6) => Action6Count += t1 + t2 + t3 + t4 + t5 + t6;
    public int Action7Count;
    public void Action7(int t1, int t2, int t3, int t4, int t5, int t6, int t7) => Action7Count += t1 + t2 + t3 + t4 + t5 + t6 + t7;
    public int Action8Count;
    public void Action8(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8) => Action8Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8;
    public int Action9Count;
    public void Action9(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9) => Action9Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9;
    public int Action10Count;
    public void Action10(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10) => Action10Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10;
    public int Action11Count;
    public void Action11(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11) => Action11Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11;
    public int Action12Count;
    public void Action12(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12) => Action12Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12;
    public int Action13Count;
    public void Action13(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13) => Action13Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13;
    public int Action14Count;
    public void Action14(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14) => Action14Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14;
    public int Action15Count;
    public void Action15(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15) => Action15Count += t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + t15;
    public int Func1() => 200 + 1;
    public int Func2(int t1) => t1 + 2 * 10;
    public int Func3(int t1, int t2) => t1 + t2 + 3 * 10;
    public int Func4(int t1, int t2, int t3) => t1 + t2 + t3 + 4 * 10;
    public int Func5(int t1, int t2, int t3, int t4) => t1 + t2 + t3 + t4 + 5 * 10;
    public int Func6(int t1, int t2, int t3, int t4, int t5) => t1 + t2 + t3 + t4 + t5 + 6 * 10;
    public int Func7(int t1, int t2, int t3, int t4, int t5, int t6) => t1 + t2 + t3 + t4 + t5 + t6 + 7 * 10;
    public int Func8(int t1, int t2, int t3, int t4, int t5, int t6, int t7) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + 8 * 10;
    public int Func9(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + 9 * 10;
    public int Func10(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + 10 * 10;
    public int Func11(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + 11 * 10;
    public int Func12(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + 12 * 10;
    public int Func13(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + 13 * 10;
    public int Func14(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + 14 * 10;
    public int Func15(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + 15 * 10;
    public int Func16(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15) => t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + t14 + t15 + 16 * 10;
}
