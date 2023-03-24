using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FuzzySet
{
    public FuzzySetCategory Category;
    public MembershipFunctionName MembershipFunction;
    public float CrispValue;
    private float _membership;
    public float Membership
    {
        get { return _membership; }
        set { _membership = value; }
    }
    public float x0;
    public float x1;
    public float x2;
    public float x3;
}

[CreateAssetMenu(menuName = "FuzzyLogic/FuzzySet Collection")]
public class FuzzySetCollection : ScriptableObject
{
    public FuzzySet[] Collection;
}
