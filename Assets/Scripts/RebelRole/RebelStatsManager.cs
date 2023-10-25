using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KermansUtility.Patterns.Singleton;

public class RebelStatsManager : MonoSingleton<RebelStatsManager>
{
    public int PrivacyCount { get; private set; }
    public int AggressivenessCount { get; private set;}
    public int LawCount { get; private set; }
    public int RoyaltyCount { get; private set; }

    private void Awake()
    {
        //Varsayýlan deðerler.
        PrivacyCount = 25;
        AggressivenessCount = 25;
        LawCount = 25;
        RoyaltyCount = 25;
    }
    public void AddPrivacy(int value)
    {
        PrivacyCount += value;
    }
    public void AddAggressiveness(int value)
    {
        AggressivenessCount += value;
    }
    public void AddLawCount(int value)
    {
        LawCount += value;
    }
    public void AddRoyaltyCount(int value)
    {
        RoyaltyCount += value;
    }
}
