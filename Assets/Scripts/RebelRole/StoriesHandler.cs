using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class StoriesHandler : MonoBehaviour
{
    public static string StoriesListPath = "StreamingAssets/RebelStoriesList/RebelStoriesList.json";

    [System.Obsolete]
    public List<StoryCard> LoadStoriesList()
    {
#if UNITY_ANDROID
        StoriesListPath = Path.Combine(Application.streamingAssetsPath, "RebelStoriesList.json");

        WWW reader = new WWW(StoriesListPath);
        while (!reader.isDone)
        {
        }
        string json = reader.text;
        return JsonConvert.DeserializeObject<List<StoryCard>>(json);
#endif
        //if (File.Exists(StoriesListPath))
        //{
        //    string json = File.ReadAllText(StoriesListPath);
        //    return JsonConvert.DeserializeObject<List<StoryCard>>(json);
        //}
        //SceneManager.LoadScene("MainMenu");
        //SaveStoriesListToFile(new List<StoryCard>());
        //return LoadStoriesList();
    }

    public void SaveStoriesListToFile(List<StoryCard> stories)
    {
        string json = JsonConvert.SerializeObject(stories, Formatting.Indented);
        File.WriteAllText(StoriesListPath, json);
    }

    public void AddNewStoryCard(StoryCard card)
    {
        List<StoryCard> stories = LoadStoriesList();
        stories.Add(card);
        SaveStoriesListToFile(stories);
    }
    public void DeleteStoryWithIndex(int index)
    {
        List<StoryCard> stories = LoadStoriesList();
        stories.RemoveAt(index);
        SaveStoriesListToFile(stories);
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