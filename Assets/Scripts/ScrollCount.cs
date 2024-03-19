using System.Diagnostics;
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
    void Start()
    {
        dontUseSetTermWithScrollCount = false; 
        GetScrollCount ();
       _storiesHandler = GameObject.FindObjectOfType<StoriesHandler>();
       if(dontUseSetTermWithScrollCount == false){SetTermWithScrollCount ();}
    }
 
    private void GetScrollCount ()
    {
      savedScrollCount = PlayerPrefs.GetInt("SavedScrollCount",0);
    }
    
    private void GetTermName()
    {
      savedTermName = PlayerPrefs.GetString("SavedTermName");
    }

    public void IncreaseSlideCount()
    {
      GetScrollCount ();
      int incrreasedSlideCount = savedScrollCount + 1;
      PlayerPrefs.SetInt("SavedScrollCount",incrreasedSlideCount);
      slideCountText.text = incrreasedSlideCount.ToString();
      if(dontUseSetTermWithScrollCount == false){SetTermWithScrollCount ();}
    }

// working with StoriesHandlerWindow
   public void SetTermWithScrollCount()
{
    GetScrollCount ();
    string[] termNames = { "Cokus", "Lale", "Fetret", "KurulusAra" , "Kurulus" };
    int[] scrollCounts = { 20,15, 10, 5, 0 };
    GetTermName();

    for (int i = 0; i < scrollCounts.Length; i++)
    { 
        if (savedScrollCount >= scrollCounts[i])
        {
            savedTermName = termNames[i];
            PlayerPrefs.SetString("SavedTermName", savedTermName);
            _storiesHandler.ChangeTerm(savedTermName);
            break;
        }
    }
}
}