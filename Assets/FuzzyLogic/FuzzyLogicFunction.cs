using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate float FuzzyLogicFunctionDelegate(float a, float b);

public enum FuzzyBooleanOperator
{
    AND,
    OR
}

public static class FuzzyLogicFunction
{
    public static FuzzyLogicFunctionDelegate[] FuzzyLogicFunctions =
    {
        AND,
        OR 
    };

    public static FuzzyLogicFunctionDelegate GetByOperator(FuzzyBooleanOperator op)
    {
        return FuzzyLogicFunctions[(int)op];
    }

    public static float AND(float a, float b) { return Mathf.Min(a, b); }
    public static float OR(float a, float b) { return Mathf.Max(a, b); }

    public static float NOT(float a) { return 1 - a; }
}
