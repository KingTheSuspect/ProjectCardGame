using KermansUtility.Patterns.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebelEndingsHandler : MonoSingleton<RebelEndingsHandler>
{
    private StoriesHandler _storiesHandler;
    private CardSelectionHandler _cardSelectionHandler;

    private KurulusEndStats _kurulusEndStats;
    private FetretEndStats _fetretEndStats;
    private LaleEndStats _laleEndStats;
    private CokusEndStats _cokusEndStats;

    private string savedTermName;
    private void Awake()
    {
        _storiesHandler = FindAnyObjectByType<StoriesHandler>();
        _cardSelectionHandler = FindAnyObjectByType<CardSelectionHandler>();

        _kurulusEndStats = FindAnyObjectByType<KurulusEndStats>();
        _fetretEndStats =  FindAnyObjectByType<FetretEndStats>();
        _laleEndStats = FindAnyObjectByType<LaleEndStats>();
        _cokusEndStats = FindAnyObjectByType<CokusEndStats>();
    }

    private void GetTermName()
    {
      savedTermName = PlayerPrefs.GetString("SavedTermName");
    }

    public bool CheckAndGetEndingCard(out StoryCard card)
    {
        
        if (RebelStatsManager.Instance.PrivacyCount <= 0)
        {
            GetTermName();
            SeeEndWithTerm(savedTermName);
            card = _storiesHandler.LoadStoriesList()[96];
            return true;
        }
        if (RebelStatsManager.Instance.PrivacyCount >= 50)
        {  
            GetTermName();
            SeeEndWithTerm(savedTermName);
            card = _storiesHandler.LoadStoriesList()[94];
            return true;
        }


        if (RebelStatsManager.Instance.AggressivenessCount <= 0)
        {
            GetTermName();
            SeeEndWithTerm(savedTermName);
            card = _storiesHandler.LoadStoriesList()[100];
            return true;
        }
        if (RebelStatsManager.Instance.AggressivenessCount >= 50)
        {   
            GetTermName();
            SeeEndWithTerm(savedTermName);
            card = _storiesHandler.LoadStoriesList()[98];
            return true;
        }


        if (RebelStatsManager.Instance.LawCount <= 0)
        {   
            GetTermName();
            SeeEndWithTerm(savedTermName);
            card = _storiesHandler.LoadStoriesList()[104];
            return true;
        }
        if (RebelStatsManager.Instance.LawCount >= 50)
        {   
            GetTermName();
            SeeEndWithTerm(savedTermName);
            card = _storiesHandler.LoadStoriesList()[102];
            return true;
        }


        if (RebelStatsManager.Instance.RoyaltyCount <= 0)
        {   
            GetTermName();
            SeeEndWithTerm(savedTermName);
            card = _storiesHandler.LoadStoriesList()[108];
            return true;
        }
        if (RebelStatsManager.Instance.RoyaltyCount >= 50)
        {   
            GetTermName();
            SeeEndWithTerm(savedTermName);
            card = _storiesHandler.LoadStoriesList()[106];
            return true;
        }

        card = null;
        Debug.Log("kart nulll oldu");
        return false;
    }
 
    public void SeeEndWithTerm(string savedTermName)
{
  
    switch (savedTermName)
    {
        case "Kurulus":
        Debug.Log("kurulus için calıstı");
         _kurulusEndStats.HandleEnd();
            break;
        case "Fetret":
        Debug.Log("Fetret için calıstı");
        _fetretEndStats.HandleEnd();
            break;
        case "Lale":
        Debug.Log("Lale için calıstı");
        _laleEndStats.HandleEnd();
            break;
        case "Cokus":
        Debug.Log("Cokus için calıstı");
        _cokusEndStats.HandleEnd();
            break;

        default:
            Debug.LogError("Geçersiz dönem adı.");
            break;
    }

}
}
