using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyAgentControl : FuzzyAgent
{
    [SerializeField] private FuzzySetCollection _fuzzySetsMovement;
    [SerializeField] private float _forwardSpeed = 20;
    private Objective _objective;

    protected override void Awake()
    {
        base.Awake();

        _objective = FindObjectOfType<Objective>();
    }

    private void Update()
    {
        Vector3 vectorToObjective = (_objective.transform.position - transform.position).normalized;
        float relativeAngleToObjective = Vector3.SignedAngle(transform.forward, vectorToObjective, Vector3.up);
        FuzzySet[] calculatedSets = _fuzzyLogic.Fuzzify(_fuzzySetsMovement.Collection, relativeAngleToObjective);
        float weightedCrispValue = _fuzzyLogic.GetWeightedCrispValue(calculatedSets);

        transform.position += transform.forward * _forwardSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0.0f, weightedCrispValue * Time.deltaTime, 0.0f));
    }
}
