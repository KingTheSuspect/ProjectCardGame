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
    public int scrollCountInStoryIndex;
    public int percentPossibilityValue = 100;
    public void GoSpesificStory(int storyIndex)
    {
        CardSelectionHandler cardSelectionHandler = FindObjectOfType<CardSelectionHandler>();
        StoriesHandler storiesHandler = FindObjectOfType<StoriesHandler>();
        StoryCard card = storiesHandler.LoadStoriesList()[storyIndex];
        cardSelectionHandler.HandleStory(card);
    }

    public void GoSpesificStoryWithPossibility(int storyIndex)
    {
        int randomChance = Random.Range(0, 101);
        if (randomChance <= percentPossibilityValue)
        {
           GoSpesificStory(storyIndex);
        }else
        {
          CardSelectionHandler cardSelectionHandler = FindObjectOfType<CardSelectionHandler>();
          cardSelectionHandler.HandleStory();
        }
    }
// ! scrollCountInStoryIndex'i yanlış yazmayın.Yazarsanız yanlış dönemlere atanabilirsiniz
    public void SetTermNow()
    {
        CardSelectionHandler cardSelectionHandler = FindObjectOfType<CardSelectionHandler>();
        PlayerPrefs.SetInt("SavedScrollCount",scrollCountInStoryIndex);
        ScrollCount scrollCount = FindObjectOfType<ScrollCount>();
        cardSelectionHandler.HandleStory();
    }

        public void GoSpesificStoryAndSetTermNow(int storyIndex)
    {
        CardSelectionHandler cardSelectionHandler = FindObjectOfType<CardSelectionHandler>();
        StoriesHandler storiesHandler = FindObjectOfType<StoriesHandler>();
        StoryCard card = storiesHandler.LoadStoriesList()[storyIndex];
        cardSelectionHandler.HandleStory(card);
        PlayerPrefs.SetInt("SavedScrollCount",scrollCountInStoryIndex);
        ScrollCount.dontUseSetTermWithScrollCount = false; 
    }
 
    public void AddRandomizationStat(StatRandomizationInfoScriptableObject info)
    {
        RebelStatsManager.Instance.AddRandomizationWithPosibility(info.StatRandomizationInfo);
    }
    public void CancelTheCharacter(string rebelCharecterName)
    {
        //�lgili karaktere ait kartlar oyun boyu g�sterilmeyecek hale getirilir.
    }
    public static void GoSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
