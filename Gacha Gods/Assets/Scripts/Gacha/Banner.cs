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
    [ReadOnly, SerializeField] List<Character> charactersPulled = new List<Character>();

    [Header("References")]
    [SerializeField] Button PremiumRollButtonReference;
    [SerializeField] Button OneRollButtonReference;
    [SerializeField] Button TenRollButtonReference;
    [SerializeField] Button CharacterOddButtonReference;
    [SerializeField] Transform RateUpReference;
    [SerializeField] List<Image> RateUpCharactersPositionReference;
    GameObject CharacterOddsReference;

    private void Awake()
    {
        GameManager.OnGameStart += RefreshBanner;
        GameManager.OnRoundEnd += RefreshBanner;
        OneRollButtonReference.AddListenerToButton(RollAtLevel);
        PremiumRollButtonReference.AddListenerToButton(RollAtLevel);
        TenRollButtonReference.AddListenerToButton(Roll10AtLevel);
        CharacterOddButtonReference.AddListenerToButton(SpawnCharacterOdds);

        //levelToPullAt = 0;
        //Player.OnLevelUp += levelToPullAt;
    }

    private void OnDisable()
    {
        if (CharacterOddsReference)
            Destroy(CharacterOddsReference);
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

        if(bannerType == BannerType.Regular)
        {
            RateUpReference.gameObject.SafeSetActive(false);
        }
        else if(bannerType == BannerType.RateUp)
        {
            for (int i = 0; i < GachaManager.Rarities.Count; i++)
            {
                List<Character> charactersOfSameRarity = GachaManager.FilterCharacters(GachaManager.Characters, GachaManager.Rarities[i]);
                Character character = charactersOfSameRarity.ChooseRandomElementInList();
                rateUpCharacters.Add(character);
                RateUpCharactersPositionReference[i].sprite = character.Icon;
            }
        }

        if (bannerType == BannerType.Regular)
            gameObject.name = "Regular Banner";
        else
            gameObject.name = BannerManager.BannerNames.ChooseRandomElementInList(true) + " Banner";
    }

    void RollAtLevel()
    {
        charactersPulled.Clear();
        Roll(levelToPullAt);
        SpawnGacha();
    }

    void Roll(int level)
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

    void Roll10AtLevel()
    {
        charactersPulled.Clear();
        Roll10(levelToPullAt);
        SpawnGacha();
    }

    void Roll10(int level)
    {
        for (int i = 0; i < 9; i++)
        {
            Roll(level);
        }

        RollHighestPossibleTier(level);
    }

    void RollHighestPossibleTier(int level)
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
        Character characterPulled = charactersOfSameRarity.ChooseRandomElementInList();

        //if theres rate up, we have a chance of overriding that with the rate up
        if (rateUpCharacters.Where(x => x.Rarity == rarity).ToList().Count > 0)
        {
            int num = Random.Range(0, 100);
            if (num < RateUpChances)
            {
                characterPulled = rateUpCharacters.Where(x => x.Rarity == rarity).ToList()[0];
            }
        }

        if (charactersPulled == null)
            throw new System.Exception("Character pulled cannot be null.");
        else
            AddCharacter(characterPulled);
    }

    void AddCharacter(Character character)
    {
        charactersPulled.Add(character);
        CharacterManager.AddCharacter(character);
    }

    OddsDictionary FindOdds(int level)
    {
        if (!BannerManager.IsRateUp())
            return GachaManager.Odds[level];
        else
            return GachaManager.Odds[level + 1];
    }

    void SpawnCharacterOdds()
    {
        if (!CharacterOddsReference)
        {
            CharacterOddsReference = Instantiate(PrefabManager.CharacterOdds, GetComponentInParent<BannerManager>().transform);
            TextList list = CharacterOddsReference.GetComponentInChildren<TextList>();
            OddsDictionary odds = FindOdds(levelToPullAt);

            //Spawn the rarity odds
            list.SpawnText("Rarity Odds", 40, Color.black, true);

            foreach (var item in odds)
            {
                list.SpawnText(item.Key.Name + ": " + item.Value + "%", 25, item.Key.Gradient, true);
            }

            list.AddSpace();

            //Spawn the character odds
            list.SpawnText("Character Odds", 40, Color.black, true);

            foreach (var item in odds)
            {
                list.SpawnText(item.Key.Name, 35, item.Key.Gradient, true);

                List<Character> charactersOfRarity = GachaManager.FilterCharacters(GachaManager.Characters, item.Key);

                for (int i = 0; i < charactersOfRarity.Count; i++)
                {
                    if(bannerType == BannerType.Regular)
                    {
                        float chance = (float)item.Value / (float)charactersOfRarity.Count;
                        chance = chance.ConvertTo2DP();
                        list.SpawnText(charactersOfRarity[i].name + ": " + chance + "%", 25);
                    }
                    else
                    {
                        if (rateUpCharacters.Contains(charactersOfRarity[i]))
                        {
                            float rateUpChance = ((float)RateUpChances * (float)item.Value / 100);
                            float normalChance = (100 - (float)RateUpChances) / 100 * (float)item.Value / (float)charactersOfRarity.Count;
                            float chance = rateUpChance + normalChance;
                            chance = chance.ConvertTo2DP();
                            list.SpawnText(charactersOfRarity[i].name + ": " + chance + "%", 25);
                        }
                        else
                        {
                            float chance = (100 - (float)RateUpChances) / 100 * (float)item.Value / (float)charactersOfRarity.Count;
                            chance = chance.ConvertTo2DP();
                            list.SpawnText(charactersOfRarity[i].name + ": " + chance + "%", 25);
                        }
                    }
                }

                list.AddSpace();
            }
        }
        else
            Destroy(CharacterOddsReference);
    }

    void SpawnGacha()
    {
        Instantiate(PrefabManager.Gacha).GetComponent<Gacha>().InitialiseCharacters(charactersPulled);
    }
}
