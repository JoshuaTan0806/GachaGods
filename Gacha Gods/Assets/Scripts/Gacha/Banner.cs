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
    [SerializeField, ShowIf("bannerType", BannerType.Regular)] int timesForGuaranteed;

    [Header("Possible Character")]
    [ReadOnly, SerializeField] List<Character> characters = new List<Character>();
    [ReadOnly, SerializeField] List<Character> rateUpCharacters = new List<Character>();

    [Header("Pull Data")]
    [SerializeField] int levelToPullAt;
    [ReadOnly, SerializeField] int TimesRolled;
    [ReadOnly, SerializeField] Character characterPulled;
    [ReadOnly, SerializeField] Rarity rarityPulled;

    private void Awake()
    {
        GameManager.OnRoundEnd += RefreshBanner;
        TimesRolled = 0;
    }

    private void OnDestroy()
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

    public void Roll10(int level)
    {
        for (int i = 0; i < 9; i++)
        {
            Roll(level);
        }

        RollHighestPossibleTier(level);
    }

    public void Roll(int level)
    {
        TimesRolled++;

        if (bannerType == BannerType.Regular && TimesRolled >= timesForGuaranteed)
        {
            TimesRolled = 0;
            RollCharacterOfRarity(CharacterManager.AllRarities.LastElement());
            return;
        }

        OddsDictionary odds = CharacterManager.AllOdds[level];

        float roll = Random.Range(0, 100);
        float counter = 0;

        foreach (var item in odds)
        {
            counter += item.Value;

            if (roll < counter)
            {
                RollCharacterOfRarity(item.Key);
                return;
            }
        }

        throw new System.Exception("Roll total is above 100.");
    }

    public void RollHighestPossibleTier(int level)
    {
        TimesRolled++;

        OddsDictionary odds = CharacterManager.AllOdds[level];

        Rarity rarityToPick = CharacterManager.AllRarities[0];

        foreach (var item in odds)
        {
            if (item.Value > 0 && item.Key.RarityNumber > rarityToPick.RarityNumber)
                rarityToPick = item.Key;
        }

        RollCharacterOfRarity(rarityToPick);
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
        characterPulled = charactersOfSameRarity.ChooseRandomElementInList();
        rarityPulled = characterPulled.Rarity;
        return characterPulled;
    }

    [Button]
    public void RollLevel()
    {
        Roll(levelToPullAt);
    }

    [Button]
    public void Roll10Level()
    {
        Roll10(levelToPullAt);
    }
}
