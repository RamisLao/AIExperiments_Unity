using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate float MembershipFunctionDelegate(float x, float x0, float x1, float x2, float x3);

public enum MembershipFunctionName
{
    Grade,
    ReverseGrade,
    Triangular,
    Trapezoid
}

public static class MembershipFunction
{
    public static MembershipFunctionDelegate[] MembershipFunctions =
    {
        Grade,
        ReverseGrade,
        Triangular,
        Trapezoid
    };

    public static MembershipFunctionDelegate GetByName(MembershipFunctionName name)
    {
        return MembershipFunctions[(int)name];
    }

    public static float Grade(float x, float x0, float x1, float x2, float x3)
    {
        if (x <= x0) return 0;
        else if (x >= x1) return 1;
        else
        {
            return (x / (x1 - x0)) - (x0 / (x1 - x0));
        }
    }

    public static float ReverseGrade(float x, float x0, float x1, float x2, float x3)
    {
        if (x <= x0) return 1;
        else if (x >= x1) return 0;
        else
        {
            return (-x / (x1 - x0)) + (x1 / (x1 - x0));
        }
    }

    public static float Triangular(float x, float x0, float x1, float x2, float x3)
    {
        if ((x <= x0) || (x >= x2)) return 0;
        else if (x == x1) return 1;
        else if ((x > x0) && (x < x1))
        {
            return (x / (x1 - x0)) - (x0 / (x1 - x0));
        }
        else
        {
            return (-x / (x2 - x1)) + (x2 / (x2 - x1));
        }
    }

    public static float Trapezoid(float x, float x0, float x1, float x2, float x3)
    {
        if ((x <= x0) || (x >= x3)) return 0;
        else if ((x >= x1) && x <= x2) return 1;
        else if ((x > x0) && (x < x1))
        {
            return (x / (x1 - x0)) - (x0 / (x1 - x0));
        }
        else
        {
            return (-x / (x3 - x2)) + (x3 / (x3 - x2));
        }
    }
}
