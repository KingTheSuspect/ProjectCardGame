using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatRandomizationInfo
{
    [SerializeField] private StatType type;
    [SerializeField] private int amount;
    [SerializeField] private float probability;
    public StatType Type => type;
    public int Amount => amount;
    public float Probability => probability;

}
