using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class StoriesHandler : MonoBehaviour
{
    public static string StoriesListPath = "Assets/RebelStoriesList/RebelStoriesList.json";

    public List<StoryCard> LoadStoriesList()
    {
        if (File.Exists(StoriesListPath))
        {
            string json = File.ReadAllText(StoriesListPath);
            return JsonConvert.DeserializeObject<List<StoryCard>>(json);
        }
        else
        {
            SaveStoriesListToFile(new List<StoryCard>());
            return LoadStoriesList();
        }
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
}
