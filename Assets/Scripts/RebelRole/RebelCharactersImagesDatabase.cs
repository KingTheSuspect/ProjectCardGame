using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KermansUtility.Patterns.Singleton;

public class RebelCharactersImagesDatabase : MonoSingleton<RebelCharactersImagesDatabase>
{
    [SerializeField] private List<RebelCharacterImage> characterImages = new List<RebelCharacterImage>();

    //Story card içindeki tellername ile girilen CharacterName eþit olmaýlýdýr!
    public Sprite GetCharacterSpriteWithName(string name)
    {
        for (int i = 0; i < characterImages.Count; i++)
        {
            if (characterImages[i].CharacterName == name)
            {
                return characterImages[i].CharacterImg;
            }
        }
        Debug.LogWarning("Belirtilen isime ait görsel bulunamadý.");
        return null;
    }
}
[System.Serializable]
public struct RebelCharacterImage
{
    public string CharacterName;
    public Sprite CharacterImg;
}
