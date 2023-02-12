using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CircleScript : MonoBehaviour
{
    public Image healthbar;
    public Image happybar;
    public static int healthcount = 50;
    public static int happycount = 50;
    public static int money = 50;
    public static int sociability = 50;
    public static int age = 22;


    void Start()
    {
        healthcount = 50;
        happycount = 50;
        money = 50;
        sociability = 50;
        age = 22;
        //healthbar.fillAmount = 0.5f;
        //happybar.fillAmount = 0.5f;
    }
    //Animasyon eklenecek
    public void HealthAdd(int healthAmount)
    {
        healthcount += healthAmount;
        //healthbar.fillAmount += healthAmount / 10;
    }
    public void HealthRemove()
    {
        healthcount--;
        //healthbar.fillAmount += 0.1f;
    }
    public void HappyAdd(int happinessAmount)
    {
        happycount += happinessAmount;
        //happybar.fillAmount += happinessAmount / 10;
    }
    public void HappyRemove()
    {
        happycount--;
        //happybar.fillAmount += 0.1f;
    }
    public void MoneyAdd(int moneyAmount)
    {
        money += moneyAmount;
    }

    public void SociabilityAdd(int sociabilityAmount)
    {
        sociability += sociabilityAmount;
    }
    public void AgeAdd()
    {
        age++;
    }
    
}


