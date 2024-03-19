using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrive : MonoBehaviour
{
   enum Durumlar
    {
        Durum1,
        Durum2,
        Durum3
    }

    void Start()
    {
        Durumlar[] durumlar = { Durumlar.Durum1, Durumlar.Durum2, Durumlar.Durum3 };

        int durumIndex = 1; // Durumu seçmek için bir indeks seçiyoruz.

        // Durumları kontrol etmek için dizi indeksini kullanarak
        switch (durumlar[durumIndex])
        {
            case Durumlar.Durum1:
                Debug.Log("Durum 1 seçildi.");
                break;
            case Durumlar.Durum2:
                Debug.Log("Durum 2 seçildi.");
                break;
            case Durumlar.Durum3:
                Debug.Log("Durum 3 seçildi.");
                break;
            default:
                Debug.Log("Tanımsız durum.");
                break;
        }
    }
}
