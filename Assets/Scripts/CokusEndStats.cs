using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CokusEndStats : MonoBehaviour
{
    public void HandleEnd()
    {
        if (RebelStatsManager.Instance.PrivacyCount <= 15)
        {
           // card = _storiesHandler.LoadStoriesList()[96];
           // return true;
           Debug.Log("privacyCount cokuste azaldı");
        }
        if (RebelStatsManager.Instance.PrivacyCount >= 50)
        {
          Debug.Log("privacyCount cokuste arttı");
        }


        if (RebelStatsManager.Instance.AggressivenessCount <= 0)
        {
             Debug.Log("agresiveCount cokuste azaldı");
        }
        if (RebelStatsManager.Instance.AggressivenessCount >= 50)
        {
            Debug.Log("agresiveCount cokuste arttı");
        }


        if (RebelStatsManager.Instance.LawCount <= 0)
        {
          Debug.Log("lawCount cokuste azaldı");
        }
        if (RebelStatsManager.Instance.LawCount >= 50)
        {
            Debug.Log("lawcount cokuste arttı");
        }


        if (RebelStatsManager.Instance.RoyaltyCount <= 0)
        {
          Debug.Log("royaltycount cokuste azaldı");
        }
        if (RebelStatsManager.Instance.RoyaltyCount >= 50)
        {
          Debug.Log("royaltycount cokuste arttı");
        }

    }
}