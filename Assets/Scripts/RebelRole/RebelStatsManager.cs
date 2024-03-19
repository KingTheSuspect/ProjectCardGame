using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KermansUtility.Patterns.Singleton;
using Ink;

// deger erttırma ve azaltma islemleri
public class RebelStatsManager : MonoSingleton<RebelStatsManager>
{
    public int PrivacyCount { get; private set; }
    public int AggressivenessCount { get; private set; }
    public int LawCount { get; private set; }
    public int RoyaltyCount { get; private set; }

    private void Awake()
    {
        //Varsay�lan de�erler.
        PrivacyCount = 10;
        AggressivenessCount = 10;
        LawCount = 10;
        RoyaltyCount = 10;

        FindAnyObjectByType<CardSelectionHandler>().RefreshStatsUi();
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
    //Verilen rastgelelik de�erlerine g�re ihtimallere ba�l� olarak stat de�i�ikli�i yapar.
    public void AddRandomizationWithPosibility(List<StatRandomizationInfo> possibilities)
    {

        float randomValue = Random.Range(0.0f, 1.0f);

        float cumulativeProbability = 0.0f;
        StatRandomizationInfo selectedStatInfo = null;

        foreach (var statInfo in possibilities)
        {
            cumulativeProbability += statInfo.Probability;

            if (randomValue <= cumulativeProbability)
            {
                selectedStatInfo = statInfo;
                break;
            }
        }

        int valueToAdd = selectedStatInfo.Amount;

        switch (selectedStatInfo.Type)
        {
            case StatType.Privacy:
                AddPrivacy(valueToAdd);
                break;
            case StatType.Aggressiveness:
                AddAggressiveness(valueToAdd);
                break;
            case StatType.Law:
                AddLawCount(valueToAdd);
                break;
            case StatType.Royalty:
                AddRoyaltyCount(valueToAdd);
                break;
        }

        FindObjectOfType<CardSelectionHandler>().RefreshStatsUi();

    }
}
public enum StatType
{
    Privacy,
    Aggressiveness,
    Law,
    Royalty,
}
