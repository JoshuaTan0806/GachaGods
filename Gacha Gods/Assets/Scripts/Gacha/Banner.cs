using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public enum BannerType
{
    Regular,
    RateUp,
}

public class Banner : MonoBehaviour
{
    [ReadOnly] public BannerType bannerType;

    [Header("Possible Characters")]
    [ReadOnly, SerializeField] List<Character> characters = new List<Character>();
    [ReadOnly, SerializeField, ShowIf("bannerType", BannerType.RateUp)] List<Character> rateUpCharacters = new List<Character>();

    [Header("Pull Data")]
    [SerializeField] int levelToPullAt;
    [ReadOnly, SerializeField] int TimesRolled;
    [ReadOnly, SerializeField] Character characterPulled;
    [ReadOnly, SerializeField] Rarity rarityPulled;

    private void Awake()
    {
        GameManager.OnGameStart += RefreshBanner;
        GameManager.OnRoundEnd += RefreshBanner;
        TimesRolled = 0;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= RefreshBanner;
        GameManager.OnRoundEnd -= RefreshBanner;
    }

    [Button]
    void RefreshBanner()
    {
        characters = new List<Character>();
        rateUpCharacters = new List<Character>();

        if (bannerType != BannerType.Regular)
            TimesRolled = 0;

        switch (bannerType)
        {
            case BannerType.Regular:
                characters = CharacterManager.Characters;
                break;

            case BannerType.RateUp:
                characters = CharacterManager.Characters;

                for (int i = 0; i < CharacterManager.Rarities.Count; i++)
                {
                    List<Character> charactersOfSameRarity = CharacterManager.FilterCharacters(CharacterManager.Characters, CharacterManager.Rarities[i]);
                    rateUpCharacters.Add(charactersOfSameRarity.ChooseRandomElementInList());
                }
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

        OddsDictionary odds = FindOdds(level);

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

        OddsDictionary odds = FindOdds(level);

        Rarity rarityToPick = CharacterManager.Rarities[0];

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

    OddsDictionary FindOdds(int level)
    {
        if (!IsRateUp())
            return CharacterManager.Odds[level];
        else
            return CharacterManager.Odds[level + 1];
    }

    bool IsRateUp()
    {
        return (GameManager.RoundNumber - 1) % 4 == 0;
    }

    [Button]
    public void RollAtTestLevel()
    {
        Roll(levelToPullAt);
    }

    [Button]
    public void Roll10AtTestLevel()
    {
        Roll10(levelToPullAt);
    }    
}
