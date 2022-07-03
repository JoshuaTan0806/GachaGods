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
    [ReadOnly, SerializeField] Character characterPulled;

    private void OnEnable()
    {
        GameManager.OnRoundEnd += RefreshBanner;
    }

    private void OnDisable()
    {
        GameManager.OnRoundEnd -= RefreshBanner;
    }

    [Button]
    void RefreshBanner()
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

    public void Roll(int level)
    {
        TimesRolled++;

        OddsDictionary odds = CharacterManager.AllOdds[level];

        float roll = Random.Range(0, 100);
        float counter = 0;

        foreach (var item in odds)
        {
            counter += item.Value;

            if (roll < counter)
            {
                characterPulled = RollCharacterOfRarity(item.Key);
                return;
            }
        }

        throw new System.Exception("Roll total is above 100.");
    }

    Character RollCharacterOfRarity(Rarity rarity)
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

    [Button]
    public void RollLevel3()
    {
        Roll(3);
    }
}
