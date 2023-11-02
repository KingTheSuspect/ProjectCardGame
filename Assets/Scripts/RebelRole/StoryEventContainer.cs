using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Story Event Container",menuName ="Scriptable Objects/Story Event Container")]
public class StoryEventContainer : ScriptableObject
{
    [HideInInspector] public int StoryID;
    public UnityEvent Event;
    public bool ContinueRandomStoryHandlingAfterEvent = true;

    public void GoSpesificStory(int storyIndex)
    {
        CardSelectionHandler cardSelectionHandler = FindObjectOfType<CardSelectionHandler>();
        StoriesHandler storiesHandler = FindObjectOfType<StoriesHandler>();
        StoryCard card = storiesHandler.LoadStoriesList()[storyIndex];

        cardSelectionHandler.HandleStory(card);
    }
    public void AddRandomizationStat(StatRandomizationInfoScriptableObject info)
    {
        RebelStatsManager.Instance.AddRandomizationWithPosibility(info.StatRandomizationInfo);
    }
}
