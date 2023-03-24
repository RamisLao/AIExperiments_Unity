using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FuzzyLogic))]
public class FuzzyAgent : MonoBehaviour
{
    protected FuzzyLogic _fuzzyLogic;

    protected virtual void Awake()
    {
        _fuzzyLogic = GetComponent<FuzzyLogic>();
    }
}
