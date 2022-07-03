using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum BannerType
{
    Regular,
    Role,
    Archetype,
    Element,
}

public class Banner : MonoBehaviour
{
    [SerializeField] BannerType bannerType;
    List<Character> characters = new List<Character>();
    List<Character> rateUpCharacters = new List<Character>();
    public void RefreshBanner()
    {
        characters = new List<Character>();

        switch (bannerType)
        {
            case BannerType.Regular:
                characters = CharacterManager.AllCharacters;

                for (int i = 0; i < CharacterManager.AllRarities.Count; i++)
                {
                    List<Character> charactersOfSameRarity = CharacterManager.FilterCharacters(CharacterManager.AllCharacters, CharacterManager.AllRarities[i]);
                    rateUpCharacters.Add(charactersOfSameRarity.ChooseRandomElementInList());
                }

                break;
            case BannerType.Role:
                characters = CharacterManager.FilterCharacters(CharacterManager.AllCharacters, CharacterManager.RandomRole());
                break;
            case BannerType.Archetype:
                characters = CharacterManager.FilterCharacters(CharacterManager.AllCharacters, CharacterManager.RandomArchetype());
                break;
            case BannerType.Element:
                characters = CharacterManager.FilterCharacters(CharacterManager.AllCharacters, CharacterManager.RandomElement());
                break;
            default:
                break;
        }
    }

    public void Roll()
    {
        for (int i = 0; i < CharacterManager.AllRarities.Count; i++)
        {

        }
    }

    public Character RollCharacterOfRarity(Rarity rarity)
    {
        if (rateUpCharacters.Where(x => x.Rarity == rarity).ToList().Count > 0)
        {
            int num = Random.Range(0, 3);
            if (num > 0)
                return rateUpCharacters.Where(x => x.Rarity == rarity).ToList()[0];
        }

        List<Character> charactersOfSameRarity = CharacterManager.FilterCharacters(characters, rarity);
        return charactersOfSameRarity.ChooseRandomElementInList();
    }
}
