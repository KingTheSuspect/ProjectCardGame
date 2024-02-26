using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScrollCount : MonoBehaviour
{
    public TextMeshProUGUI slideCountText;
    void Start()
    {
        
    }
    
    public void IncreaseSlideCount()
    {
      int savedScrollCount = PlayerPrefs.GetInt("SavedScrollCount",0);
      int incrreasedSlideCount = savedScrollCount + 1;
      PlayerPrefs.SetInt("SavedScrollCount",incrreasedSlideCount);
      slideCountText.text = incrreasedSlideCount.ToString();
    }
}
