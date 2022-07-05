using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public enum BannerType
{
    Regular,
    RateUp,
}

public class Banner : MonoBehaviour
{
    [ReadOnly] public BannerType bannerType;
    [SerializeField] int RateUpChances = 25;

    [Header("Characters")]
    [ReadOnly, SerializeField, ShowIf("bannerType", BannerType.RateUp)] List<Character> rateUpCharacters = new List<Character>();

    [Header("Pull Data")]
    [SerializeField] int levelToPullAt;
    [ReadOnly, SerializeField] Character characterPulled;
    [ReadOnly, SerializeField] Rarity rarityPulled;

    [Header("References")]
    [SerializeField] Button OneRollReference;
    [SerializeField] Button TenRollReference;
    [SerializeField] Button CharacterOddsReference;

    private void Awake()
    {
        GameManager.OnGameStart += RefreshBanner;
        GameManager.OnRoundEnd += RefreshBanner;
        OneRollReference.onClick.AddListener(RollAtLevel);
        TenRollReference.onClick.AddListener(Roll10AtLevel);
        CharacterOddsReference.onClick.AddListener(SpawnCharacterOdds);

        //levelToPullAt = 0;
        //Player.OnLevelUp += levelToPullAt;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= RefreshBanner;
        GameManager.OnRoundEnd -= RefreshBanner;
    }

    [Button]
    void RefreshBanner()
    {
        rateUpCharacters = new List<Character>();

        switch (bannerType)
        {
            case BannerType.Regular:

            case BannerType.RateUp:
                for (int i = 0; i < GachaManager.Rarities.Count; i++)
                {
                    List<Character> charactersOfSameRarity = GachaManager.FilterCharacters(GachaManager.Characters, GachaManager.Rarities[i]);
                    rateUpCharacters.Add(charactersOfSameRarity.ChooseRandomElementInList());
                }
                break;
            default:
                break;
        }
    }

    public void RollAtLevel()
    {
        Roll(levelToPullAt);
    }

    public void Roll(int level)
    {
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

    public void Roll10AtLevel()
    {
        Roll10(levelToPullAt);
    }

    public void Roll10(int level)
    {
        for (int i = 0; i < 9; i++)
        {
            Roll(level);
        }

        RollHighestPossibleTier(level);
    }

    public void RollHighestPossibleTier(int level)
    {
        OddsDictionary odds = FindOdds(level);

        Rarity rarityToPick = GachaManager.Rarities[0];

        foreach (var item in odds)
        {
            if (item.Value > 0 && item.Key.RarityNumber > rarityToPick.RarityNumber)
                rarityToPick = item.Key;
        }

        RollCharacterOfRarity(rarityToPick);
    }

    void RollCharacterOfRarity(Rarity rarity)
    {
        //pull a character
        List<Character> charactersOfSameRarity = GachaManager.FilterCharacters(GachaManager.Characters, rarity);
        characterPulled = charactersOfSameRarity.ChooseRandomElementInList();

        //if theres rate up, we have a chance of overriding that with the rate up
        if (rateUpCharacters.Where(x => x.Rarity == rarity).ToList().Count > 0)
        {
            int num = Random.Range(0, 100);
            if (num < RateUpChances)
            {
                characterPulled = rateUpCharacters.Where(x => x.Rarity == rarity).ToList()[0];
            }
        }

        if (characterPulled == null)
            throw new System.Exception("Character pulled cannot be null.");
        else
            AddCharacter(characterPulled);
    }

    void AddCharacter(Character character)
    {
        rarityPulled = character.Rarity;
        CharacterManager.AddCharacter(character);
    }

    OddsDictionary FindOdds(int level)
    {
        if (!IsRateUp())
            return GachaManager.Odds[level];
        else
            return GachaManager.Odds[level + 1];
    }

    bool IsRateUp()
    {
        return (GameManager.RoundNumber - 1) % 4 == 0;
    }  

    void SpawnCharacterOdds()
    {

    }
}
