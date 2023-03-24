using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FuzzyLogic : MonoBehaviour
{
    public FuzzySet[] Fuzzify(FuzzySet[] fuzzySets, float value)
    {
        FuzzySet[] calculatedSets = fuzzySets.Select(set =>
        {
            MembershipFunctionDelegate func = MembershipFunction.GetByName(set.MembershipFunction);
            float membership = func(value, set.x0, set.x1, set.x2, set.x3);
            set.Membership = membership;
            return set;
        }).ToArray();

        return calculatedSets;
    }

    public FuzzyRule[] FuzzyIO(FuzzySet[] calculatedSets, FuzzyRuleCollection rules)
    {
        FuzzyRule[] calculatedRules = rules.FuzzyRules.Select(rule =>
        {
            float ruleMembership = CalculateRuleMembership(rule, calculatedSets);
            rule.Membership = ruleMembership;

            return rule;
        }).ToArray();

        return calculatedRules;
    }

    private float CalculateRuleMembership(FuzzyRule rule, FuzzySet[] calculatedSets)
    {
        float ruleMembership = 0;
        for (int i = 0; i < rule.FuzzyRuleOperators.Length; i++)
        {
            FuzzyRuleOperator op = rule.FuzzyRuleOperators[i];
            FuzzySet fuzzySet = GetSetByCategory(calculatedSets, op.Category);

            float fuzzySetMembership = rule.FuzzyRuleOperators[i].Not ?
                FuzzyLogicFunction.NOT(fuzzySet.Membership) :
                fuzzySet.Membership;

            if (i == 0)
            {
                ruleMembership = fuzzySetMembership;
            }
            else
            {
                ruleMembership = FuzzyLogicFunction.GetByOperator(op.Operator)(ruleMembership, fuzzySetMembership);
            }
        }

        return ruleMembership;
    }

    public FuzzyOutputCategory GetHighestDegreeOutput(FuzzyRule[] calculatedRules)
    {
        return calculatedRules.OrderByDescending(rule => rule.Membership).ToArray()[0].Output;
    }

    public float GetWeightedCrispValue(FuzzySet[] calculatedSet)
    {
        float weightedValue = calculatedSet.Sum(set => set.CrispValue * set.Membership);

        return weightedValue;
    }

    private FuzzySet GetSetByCategory(FuzzySet[] calculatedSets, FuzzySetCategory category)
    {
        return calculatedSets.Where(set => set.Category == category).ToArray()[0];
    }
}
