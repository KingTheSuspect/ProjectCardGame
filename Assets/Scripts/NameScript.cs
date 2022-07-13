using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameScript : MonoBehaviour
{
    public TextMeshProUGUI textname;


    //Random bir isim se�ilecek, ard�ndan otomatik olarak textname'e yaz�lacak.
    //Bir txt dosyas�na isimler yaz�l�p oradan �ekilebilir. Ald���m�z isimler static bir bi�imde olacak istedi�imiz yerde alabilece�iz
    
    void Start()
    {
        textname.text = "";
    }
}
