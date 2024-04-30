using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScrollCount : MonoBehaviour
{
    public TextMeshProUGUI slideCountText;
    private int savedScrollCount;
    private StoriesHandler _storiesHandler;
    public static bool dontUseSetTermWithScrollCount;
    private string savedTermName;
    private static ScrollCount instance;

   private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
private GameObject scrollCountText;
    void Start()
    {   
        dontUseSetTermWithScrollCount = false;
        GetScrollCount();
        _storiesHandler = GameObject.FindObjectOfType<StoriesHandler>();
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
    public void SetTermWithScrollCount()
    { 
        GetScrollCount();
        // Diziler ve değişkenler
        string[] termNames = { "Cokus", "CokusBas", "Lale", "LaleBas", "Fetret", "FetretBas", "Kurulus", "KurulusBas" };
        int[] scrollCounts = { 70, 60, 50, 40, 30, 20, 10, 0 };

        GetTermName();

        for (int i = 0; i < scrollCounts.Length; i++)
        {  
            if (savedScrollCount >= scrollCounts[i])
            {   
                // _storiesHandler.LoadStoriesList();
                savedTermName = termNames[i];
                PlayerPrefs.SetString("SavedTermName", savedTermName);
                _storiesHandler.ChangeTerm(savedTermName);
                //if(savedTermName != "KurulusBas"){SceneManager.LoadScene(savedTermName+"Scene");}
                //_storiesHandler.LoadStoriesList();
                SceneManager.LoadScene(savedTermName+"Scene");
                break;
            }
        }
    }
} 