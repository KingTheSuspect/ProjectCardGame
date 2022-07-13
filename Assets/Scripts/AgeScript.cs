using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AgeScript : MonoBehaviour
{
    public TextMeshProUGUI ageText;
    public static int age = 99;

    void Update()
    {
        ageText.text = age.ToString();
    }
    public void AddAge()
    {
        age++;
    }

}
