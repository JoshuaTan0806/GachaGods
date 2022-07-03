using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public enum BannerType
{
    Regular,
    RateUp,
    Role,
    Archetype,
    Element,
}

public class Banner : MonoBehaviour
{
    [SerializeField] BannerType bannerType;
    [ReadOnly, SerializeField] List<Character> characters = new List<Character>();
    [ReadOnly, SerializeField] List<Character> rateUpCharacters = new List<Character>();
    [ReadOnly, SerializeField] int TimesRolled;

    [Button]
    public void RefreshBanner()
    {
        characters = new List<Character>();

        if (bannerType != BannerType.Regular)
            TimesRolled = 0;

        switch (bannerType)
        {
            case BannerType.Regular:
                characters = CharacterManager.AllCharacters;
                break;

            case BannerType.RateUp:
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

    [Button]
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
