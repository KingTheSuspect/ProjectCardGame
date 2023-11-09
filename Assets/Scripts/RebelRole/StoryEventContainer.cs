using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="Story Event Container",menuName ="Scriptable Objects/Story Event Container")]
public class StoryEventContainer : ScriptableObject
{
    public string StoryID;
    public bool BothOptionsEvent;
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
    public void CancelTheCharacter(string rebelCharecterName)
    {
        //Ýlgili karaktere ait kartlar oyun boyu gösterilmeyecek hale getirilir.
    }
    public static void GoSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
