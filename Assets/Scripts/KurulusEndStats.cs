using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurulusEndStats : MonoBehaviour
{
    // Start is called before the first frame update
   
   
    public void HandleEnd()
    {
        if (RebelStatsManager.Instance.PrivacyCount <= 15)
        {
           // card = _storiesHandler.LoadStoriesList()[96];
           // return true;
           Debug.Log("privacyCount kurulusta azaldı");
        }
        if (RebelStatsManager.Instance.PrivacyCount >= 50)
        {
          Debug.Log("privacyCount kurulusta arttı");
        }


        if (RebelStatsManager.Instance.AggressivenessCount <= 0)
        {
             Debug.Log("agresiveCount kurulusta azaldı");
        }
        if (RebelStatsManager.Instance.AggressivenessCount >= 50)
        {
            Debug.Log("agresiveCount kurulusta arttı");
        }


        if (RebelStatsManager.Instance.LawCount <= 0)
        {
          Debug.Log("lawCount kurulusta azaldı");
        }
        if (RebelStatsManager.Instance.LawCount >= 50)
        {
            Debug.Log("lawcount kurulusta arttı");
        }


        if (RebelStatsManager.Instance.RoyaltyCount <= 0)
        {
          Debug.Log("royaltycount kurulusta azaldı");
        }
        if (RebelStatsManager.Instance.RoyaltyCount >= 50)
        {
          Debug.Log("royaltycount kurulusta arttı");
        }

    }

}
