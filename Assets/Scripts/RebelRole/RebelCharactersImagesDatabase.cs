using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KermansUtility.Patterns.Singleton;

public class RebelCharactersImagesDatabase : MonoSingleton<RebelCharactersImagesDatabase>
{
    [SerializeField] private List<RebelCharacterImage> characterImages = new List<RebelCharacterImage>();

    //Story card i�indeki tellername ile girilen CharacterName e�it olma�l�d�r!
    public Sprite GetCharacterSpriteWithName(string name)
    {
        for (int i = 0; i < characterImages.Count; i++)
        {
            if (characterImages[i].CharacterName == name)
            {
                return characterImages[i].CharacterImg;
            }
        }
        Debug.LogWarning("Belirtilen isime ait g�rsel bulunamad�.");
        return null;
    }
}
[System.Serializable]
public struct RebelCharacterImage
{
    public string CharacterName;
    public Sprite CharacterImg;
}
