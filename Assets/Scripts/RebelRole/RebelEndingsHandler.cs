using KermansUtility.Patterns.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebelEndingsHandler : MonoSingleton<RebelEndingsHandler>
{
    private StoriesHandler _storiesHandler;
    private CardSelectionHandler _cardSelectionHandler;
    private void Awake()
    {
        _storiesHandler = FindAnyObjectByType<StoriesHandler>();
        _cardSelectionHandler = FindAnyObjectByType<CardSelectionHandler>();
    }
    public bool CheckAndGetEndingCard(out StoryCard card)
    {

        if (RebelStatsManager.Instance.PrivacyCount <= 0)
        {
            card = _storiesHandler.LoadStoriesList()[96];
            return true;
        }
        if (RebelStatsManager.Instance.PrivacyCount >= 50)
        {
            card = _storiesHandler.LoadStoriesList()[94];
            return true;
        }


        if (RebelStatsManager.Instance.AggressivenessCount <= 0)
        {
            card = _storiesHandler.LoadStoriesList()[100];
            return true;
        }
        if (RebelStatsManager.Instance.AggressivenessCount >= 50)
        {
            card = _storiesHandler.LoadStoriesList()[98];
            return true;
        }


        if (RebelStatsManager.Instance.LawCount <= 0)
        {
            card = _storiesHandler.LoadStoriesList()[104];
            return true;
        }
        if (RebelStatsManager.Instance.LawCount >= 50)
        {
            card = _storiesHandler.LoadStoriesList()[102];
            return true;
        }


        if (RebelStatsManager.Instance.RoyaltyCount <= 0)
        {
            card = _storiesHandler.LoadStoriesList()[108];
            return true;
        }
        if (RebelStatsManager.Instance.RoyaltyCount >= 50)
        {
            card = _storiesHandler.LoadStoriesList()[106];
            return true;
        }

        card = null;
        return false;
    }
}
