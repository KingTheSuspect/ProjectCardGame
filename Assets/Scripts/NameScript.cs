using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameScript : MonoBehaviour
{
    public TextMeshProUGUI textname;


    //Random bir isim seçilecek, ardýndan otomatik olarak textname'e yazýlacak.
    //Bir txt dosyasýna isimler yazýlýp oradan çekilebilir. Aldýðýmýz isimler static bir biçimde olacak istediðimiz yerde alabileceðiz
    
    void Start()
    {
        textname.text = "";
    }
}
