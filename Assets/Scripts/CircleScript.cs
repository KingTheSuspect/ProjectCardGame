using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CircleScript : MonoBehaviour
{
    public static int healthcount = 50;
    public static int happycount = 50;
    public static int money = 50;
    public static int sociability = 50;
    //public static int age = 22;


    void Start()
    {
        healthcount = 50;
        happycount = 50;
        money = 50;
        sociability = 50;
        //age = 22;
    }
    //Animasyon eklenecek
    public void HealthAdd(int healthAmount)
    {
        healthcount += healthAmount;
    }
    //public void HealthRemove()
    //{
    //    healthcount--;
    //}
    public void HappyAdd(int happinessAmount)
    {
        happycount += happinessAmount;
    }
    //public void HappyRemove()
    //{
    //    happycount--;
    //}
    public void MoneyAdd(int moneyAmount)
    {
        money += moneyAmount;
    }

    public void SociabilityAdd(int sociabilityAmount)
    {
        sociability += sociabilityAmount;
    }
    //public void AgeAdd()
    //{
    //    age++;
    //}
    
}


