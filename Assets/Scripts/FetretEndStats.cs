using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetretEndStats : MonoBehaviour
{
public void HandleEnd()
    {
        if (RebelStatsManager.Instance.PrivacyCount <= 15)
        {
           // card = _storiesHandler.LoadStoriesList()[96];
           // return true;
           Debug.Log("privacyCount fetrette azaldı");
        }
        if (RebelStatsManager.Instance.PrivacyCount >= 50)
        {
          Debug.Log("privacyCount fetrette arttı");
        }


        if (RebelStatsManager.Instance.AggressivenessCount <= 0)
        {
             Debug.Log("agresiveCount fetrette azaldı");
        }
        if (RebelStatsManager.Instance.AggressivenessCount >= 50)
        {
            Debug.Log("agresiveCount fetrette arttı");
        }


        if (RebelStatsManager.Instance.LawCount <= 0)
        {
          Debug.Log("lawCount fetrette azaldı");
        }
        if (RebelStatsManager.Instance.LawCount >= 50)
        {
            Debug.Log("lawcount fetrette arttı");
        }


        if (RebelStatsManager.Instance.RoyaltyCount <= 0)
        {
          Debug.Log("royaltycount fetrette azaldı");
        }
        if (RebelStatsManager.Instance.RoyaltyCount >= 50)
        {
          Debug.Log("royaltycount fetrette arttı");
        }

    }
}
