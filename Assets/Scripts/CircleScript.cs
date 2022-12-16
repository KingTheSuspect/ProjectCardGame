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
        healtcount = 5;
        happycount = 5;
        healtbar.fillAmount = 0.5f;
        happybar.fillAmount = 0.5f;
    }
    //Animasyon eklenecek
    public void HealtAdd(int healthAmount)
    {
        healtcount+=healthAmount;
        healtbar.fillAmount += healthAmount / 10;
    }
    public void HealtRemove()
    {
        healtcount--;
        healtbar.fillAmount += 0.1f;
    }
    public void HappyAdd(int happinessAmount)
    {
        happycount+=happinessAmount;
        happybar.fillAmount += happinessAmount/10;
    }
    public void HappyRemove()
    {
        happycount--;
        happybar.fillAmount += 0.1f;
    }
    
}


