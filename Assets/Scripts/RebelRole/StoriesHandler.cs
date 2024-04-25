using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class StoriesHandler : MonoBehaviour
{
    [HideInInspector]public string CurrentSelectedStoriesListPath = "";

    //public static string RebelStoriesListPaths = "Assets/StreamingAssets/RebelStoriesList.json";
    public static string KurulusAraStoriesPath = "Assets/StreamingAssets/KurulusAraStories.json";
    public static string KurulusListPath = "Assets/StreamingAssets/KurulusStories.json";

    public static string FetretBasStoriesPath = "Assets/StreamingAssets/FetretBasStrories.json";
    public static string FetretStoriesPath = "Assets/StreamingAssets/FetretStories.json";

    public static string LaleBasStoriesPath = "Assets/StreamingAssets/LaleBasStories.json";
    public static string LaleStoriesPath = "Assets/StreamingAssets/LaleStories.json";
    
    public static string CokusBasStoriesPath = "Assets/StreamingAssets/CokusBasStories.json";
    public static string CokusStoriesPath = "Assets/StreamingAssets/CokusStories.json";

    

    [System.Obsolete]
    public List<StoryCard> LoadStoriesList()
    {
        List<StoryCard> result = new List<StoryCard>();

#if UNITY_EDITOR
        if (File.Exists(CurrentSelectedStoriesListPath))
        {
            string jsonPC = File.ReadAllText(CurrentSelectedStoriesListPath);
            result = JsonConvert.DeserializeObject<List<StoryCard>>(jsonPC);
            Debug.Log(CurrentSelectedStoriesListPath);
        }
#endif
 
//#if UNITY_ANDROID
//        CurrentSelectedStoriesListPath = Path.Combine(Application.streamingAssetsPath, "KurulusStories.json");

//        WWW reader = new WWW(CurrentSelectedStoriesListPath);
//        while (!reader.isDone)
//        {
//        }
//        string json = reader.text;
//        result = JsonConvert.DeserializeObject<List<StoryCard>>(json);
//#endif

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
        Debug.Log("Yeni hikaye eklendi. Dosya yolu: " + CurrentSelectedStoriesListPath); // Debug log eklendi
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


     public void ChangeTerm(string savedTermName)
{
  
    switch (savedTermName)
    {
            case "KurulusAra":
            CurrentSelectedStoriesListPath = KurulusAraStoriesPath;
            LoadStoriesList();
            //ScrollCount.dontUseSetTermWithScrollCount = true;
            break;
        case "Kurulus":
            CurrentSelectedStoriesListPath = KurulusListPath;
            LoadStoriesList();
            break;
        case "FetretBas":
            CurrentSelectedStoriesListPath = FetretBasStoriesPath;
            LoadStoriesList();
            //ScrollCount.dontUseSetTermWithScrollCount = true;
            break; 
        case "Fetret":
            CurrentSelectedStoriesListPath = FetretStoriesPath;
            LoadStoriesList();
            break;
        case "LaleBas":
            CurrentSelectedStoriesListPath = LaleBasStoriesPath;
            LoadStoriesList();
            //ScrollCount.dontUseSetTermWithScrollCount = true;
            break;
        case "Lale":
            CurrentSelectedStoriesListPath = LaleStoriesPath;
            LoadStoriesList();
            break;
        case "CokusBas":
            CurrentSelectedStoriesListPath = CokusBasStoriesPath;
            LoadStoriesList();
            //ScrollCount.dontUseSetTermWithScrollCount = true;
            break;
        case "Cokus":
            CurrentSelectedStoriesListPath = CokusStoriesPath;
            LoadStoriesList();
            break;

        default:
            Debug.LogError("Geçersiz dönem adı.");
            break;
    }

}
}