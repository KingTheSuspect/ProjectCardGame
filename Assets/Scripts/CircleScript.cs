using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;



public class CircleScript : MonoBehaviour
{
    public SlideCards eslidecards;
    public static int healthcount = 25;
    public static int happycount = 25;
    public static int money = 25;
    public static int sociability = 25;
    public static int dieforone;
    //public static int age = 22;


    void Start()
    {
        healthcount = 5;
        happycount = 25;
        money = 25;
        sociability = 25;
    }
    public void HealthAdd(int healthAmount)
    {
        healthcount += healthAmount;
    }
    public void HappyAdd(int happinessAmount)
    {
        happycount += happinessAmount;
    }
    public void MoneyAdd(int moneyAmount)
    {
        money += moneyAmount;
    }
    public void SociabilityAdd(int sociabilityAmount)
    {
        sociability += sociabilityAmount;
    }
    private void Update()
    {
        //Debug.Log(eslidecards.eventList[((int)(eslidecards.index * eslidecards.jObj.Count)).ToString()][eslidecards.controlLength].ToString());
        if (healthcount > 0)
        {
            //Debug.Log(eslidecards.eventList[eslidecards.jObj].ToString()[1].ToString()); 
        }
        if (happycount > 0)
        {

        }
        if (money > 0)
        {

        }
        if (sociability > 0)
        {

        }


    }



}


