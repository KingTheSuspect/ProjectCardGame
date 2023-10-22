using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RebelOption
{
    public string OptionName;
    public int Privacy;
    public int Aggressiveness;
    public int Law;
    public int Royalty;
    public RebelMainQuestModifierType MainQuestModifierType;
}
public enum RebelMainQuestModifierType
{
    PropagandaAndPublicSpirit, 
    Rebellion,
    CovertOrganizationandEspionage,
    EconomicPressureAndResourceControl,
    Nothing
}
