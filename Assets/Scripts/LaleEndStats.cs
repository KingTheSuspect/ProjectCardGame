using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaleEndStats : MonoBehaviour
{
public void HandleEnd()
    {
        if (RebelStatsManager.Instance.PrivacyCount <= 15)
        {
           // card = _storiesHandler.LoadStoriesList()[96];
           // return true;
           Debug.Log("privacyCount lale d. azaldı");
        }
        if (RebelStatsManager.Instance.PrivacyCount >= 50)
        {
          Debug.Log("privacyCount lale d. arttı");
        }


        if (RebelStatsManager.Instance.AggressivenessCount <= 0)
        {
             Debug.Log("agresiveCount lale d. azaldı");
        }
        if (RebelStatsManager.Instance.AggressivenessCount >= 50)
        {
            Debug.Log("agresiveCount lale d. arttı");
        }


        if (RebelStatsManager.Instance.LawCount <= 0)
        {
          Debug.Log("lawCount lale d. azaldı");
        }
        if (RebelStatsManager.Instance.LawCount >= 50)
        {
            Debug.Log("lawcount lale d. arttı");
        }


        if (RebelStatsManager.Instance.RoyaltyCount <= 0)
        {
          Debug.Log("royaltycount lale d. azaldı");
        }
        if (RebelStatsManager.Instance.RoyaltyCount >= 50)
        {
          Debug.Log("royaltycount lale d. arttı");
        }

    }
}
