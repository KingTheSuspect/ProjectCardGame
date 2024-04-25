using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollCount : MonoBehaviour
{
    public TextMeshProUGUI slideCountText;
    private int savedScrollCount;
    private StoriesHandler _storiesHandler;
    public static bool dontUseSetTermWithScrollCount;
    private string savedTermName;

    // Kuyruk yapısı tanımlama
    private Queue<GameObject> storyEventQueue;

    // Kuyruk yapılacak nesneleri tanımla
    public GameObject[] storyEventObjects;

    void Start()
    {
        dontUseSetTermWithScrollCount = false;
        GetScrollCount();
        _storiesHandler = GameObject.FindObjectOfType<StoriesHandler>();

        // Kuyruğu başlatın
        storyEventQueue = new Queue<GameObject>();

        // storyEventObjects dizisini kuyruğa ekle
        foreach (GameObject obj in storyEventObjects)
        {
            storyEventQueue.Enqueue(obj);
        }

        SetTermWithScrollCount();
    }

    private void GetScrollCount()
    {
        savedScrollCount = PlayerPrefs.GetInt("SavedScrollCount", 0);
    }

    private void GetTermName()
    {
        savedTermName = PlayerPrefs.GetString("SavedTermName");
    }

    public void IncreaseSlideCount()
    {
        GetScrollCount();
        int increasedSlideCount = savedScrollCount + 1;
        PlayerPrefs.SetInt("SavedScrollCount", increasedSlideCount);
        slideCountText.text = increasedSlideCount.ToString();
        SetTermWithScrollCount();
    }

    // Çalıştığınız hikayelerle ilgili pencereler
    public void SetTermWithScrollCount()
    { 
        GetScrollCount();

        // Diziler ve değişkenler
        string[] termNames = { "Cokus", "CokusBas", "Lale", "LaleBas", "Fetret", "FetretBas", "Kurulus", "KurulusAra" };
        int[] scrollCounts = { 70, 60, 50, 40, 30, 20, 10, 0 };

        GetTermName();

        for (int i = 0; i < scrollCounts.Length; i++)
        {  
            if (savedScrollCount -1 >= scrollCounts[i])
            {

            }
            if (savedScrollCount >= scrollCounts[i])
            {   ProcessStoryEventQueue();
                 _storiesHandler.LoadStoriesList();
                savedTermName = termNames[i];
                PlayerPrefs.SetString("SavedTermName", savedTermName);
                _storiesHandler.ChangeTerm(savedTermName);
                _storiesHandler.LoadStoriesList();
                // Kuyrukta sırayla işlem yapma
                //ProcessStoryEventQueue();
                // İlgili nesneyi etkinleştir
                storyEventObjects[i].SetActive(true);
                _storiesHandler.LoadStoriesList();
                break;
            }
        }
    }

    // Kuyruktaki nesneleri sırayla işle ve devre dışı bırak
    void ProcessStoryEventQueue()
    {
        while (storyEventQueue.Count > 0)
        {   
           _storiesHandler.LoadStoriesList();
            GameObject obj = storyEventQueue.Dequeue();
            obj.SetActive(false);
           _storiesHandler.LoadStoriesList();
        }
        
        // Kuyruğa yeni nesneler ekle
        foreach (GameObject obj in storyEventObjects)
        {   
           _storiesHandler.LoadStoriesList();
            storyEventQueue.Enqueue(obj);
            _storiesHandler.LoadStoriesList();
        }
    }
}