using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CircleScript : MonoBehaviour
{
    public Image healtbar;
    public Image happybar;
    public static int healtcount = 5;
    public static int happycount = 5;


    void Start()
    {
        healtbar.fillAmount = 0.5f;
        happybar.fillAmount = 0.5f;
    }
    //Animasyon eklenecek
    public void HealtAdd()
    {
        healtcount++;
        healtbar.fillAmount += 0.1f;
    }
    public void HealtRemove()
    {
        healtcount--;
        healtbar.fillAmount += 0.1f;
    }
    public void HappyAdd()
    {
        happycount++;
        happybar.fillAmount += 0.1f;
    }
    public void HappyRemove()
    {
        happycount--;
        happybar.fillAmount += 0.1f;
    }
    
}


