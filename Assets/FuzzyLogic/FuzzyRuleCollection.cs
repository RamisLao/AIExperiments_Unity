using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FuzzyRuleOperator
{
    public FuzzyBooleanOperator Operator;
    public bool Not;
    public FuzzySetCategory Category;
}

[Serializable]
public class FuzzyRule
{
    public FuzzyRuleOperator[] FuzzyRuleOperators;
    public FuzzyOutputCategory Output;
    private float _membership;
    public float Membership
    {
        get { return _membership; }
        set { _membership = value; }
    }
}

[CreateAssetMenu(menuName = "FuzzyLogic/FuzzyRuleCollection")]
public class FuzzyRuleCollection : ScriptableObject
{
    public FuzzyRule[] FuzzyRules;    
}
