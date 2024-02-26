using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScrollCount : MonoBehaviour
{
    public TextMeshProUGUI slideCountText;
    int savedScrollCount;
    int incrreasedSlideCount;
    void Start()
    {
        savedScrollCount = PlayerPrefs.GetInt("SavedScrollCount", 0);
        slideCountText.text = savedScrollCount.ToString();
    }
    
    public void IncreaseSlideCount()
    {
      savedScrollCount = PlayerPrefs.GetInt("SavedScrollCount",0);
      incrreasedSlideCount = savedScrollCount + 1;
      PlayerPrefs.SetInt("SavedScrollCount",incrreasedSlideCount);
      slideCountText.text = incrreasedSlideCount.ToString();
    }
}
