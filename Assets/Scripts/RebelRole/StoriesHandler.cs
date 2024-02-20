using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class StoriesHandler : MonoBehaviour
{
    public static string CurrentSelectedStoriesListPath = "Assets/StreamingAssets/KingdomStoriesList.json";
    public static string RebelStoriesListPaths = "Assets/StreamingAssets/RebelStoriesList.json";
    public static string KingdomStoriesListPath = "Assets/StreamingAssets/KingdomStoriesList.json";

    [System.Obsolete]
    public List<StoryCard> LoadStoriesList()
    {
        List<StoryCard> result = new List<StoryCard>();

#if UNITY_EDITOR
        if (File.Exists(CurrentSelectedStoriesListPath))
        {
            string jsonPC = File.ReadAllText(CurrentSelectedStoriesListPath);
            result = JsonConvert.DeserializeObject<List<StoryCard>>(jsonPC);
        }
#endif

#if UNITY_ANDROID
        CurrentSelectedStoriesListPath = Path.Combine(Application.streamingAssetsPath, "KingdomStoriesList.json");

        WWW reader = new WWW(CurrentSelectedStoriesListPath);
        while (!reader.isDone)
        {
        }
        string json = reader.text;
        result = JsonConvert.DeserializeObject<List<StoryCard>>(json);
#endif

        return result;
    }


    public void SaveStoriesListToFile(List<StoryCard> stories)
    {
        string json = JsonConvert.SerializeObject(stories, Formatting.Indented);
        File.WriteAllText(CurrentSelectedStoriesListPath, json);
        Debug.Log("güncelleme yapildi");
    }

    public void AddNewStoryCard(StoryCard card)
    {
        List<StoryCard> stories = LoadStoriesList();
        stories.Add(card);
        SaveStoriesListToFile(stories);
        Debug.Log("yeni hikaye eklendi");
    }
    public void DeleteStoryWithIndex(int index)
    {
        List<StoryCard> stories = LoadStoriesList();
        stories.RemoveAt(index);
        SaveStoriesListToFile(stories);
         Debug.Log("index ile beraber hikaye silindi");
    }
    public int GetIndexWithContent(StoryCard card)
    {
        for (int i = 0; i < LoadStoriesList().Count; i++)
        {
            if (LoadStoriesList()[i].StoryContent == card.StoryContent)
            {
                return i;
            }
        }
        return -1;
    }
}