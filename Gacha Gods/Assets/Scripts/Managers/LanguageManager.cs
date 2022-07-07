using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

[CreateAssetMenu(menuName = "Managers/Language Manager")]
public class LanguageManager : Factories.FactoryBase
{
    public static TranslationDictionary Translations = new TranslationDictionary();
    [SerializeField, ReadOnly] TranslationDictionary translations = new TranslationDictionary();
    public static KeywordDictionary Keywords = new KeywordDictionary();
    [SerializeField, ReadOnly] KeywordDictionary keywords = new KeywordDictionary();
    public static Language CurrentLanguage;
    [SerializeField] Language English;

    public static System.Action OnLanguageChanged;

    public override void Initialise()
    {
        Translations.Clear();
        Keywords.Clear();

        foreach (var item in translations)
        {
            Translations.Add(item.Key, item.Value);
        }

        //Keywords = (KeywordDictionary)keywords.OrderByDescending(x => x.Key.Length); //cast not working

        List<string> keywordKeys = new List<string>();
        foreach (var item in keywords)
        {
            keywordKeys.Add(item.Key);
        }
        keywordKeys = keywordKeys.OrderByDescending(x => x.Length).ToList();
        foreach (var item in keywordKeys)
        {
            Keywords.Add(item, keywords[item]);
        }

        //save language here

#if UNITY_EDITOR
        CurrentLanguage = LanguageToTest;
#endif

        if (CurrentLanguage == null)
            CurrentLanguage = English;
    }

    public static void ChangeLanguage(Language language)
    {
        CurrentLanguage = language;
        OnLanguageChanged?.Invoke();
    }

    public static string FindTranslation(string str)
    {
        if (TranslateSentence(str) == str)
            return TranslateKeyword(str);
        else
            return TranslateSentence(str);
    }

    public static string TranslateKeyword(string str)
    {
        foreach (var item in Keywords)
        {
            if (str.Contains(item.Key))
                return str.Replace(item.Key, item.Value.Translations[CurrentLanguage]);
        }

        return str;
    }

    public static string TranslateSentence(string str)
    {
        if (CurrentLanguage == null)
            return str;

        if (!Translations.ContainsKey(str))
            return str;

        if (!Translations[str].Translations.ContainsKey(CurrentLanguage))
            return str;

        return Translations[str].Translations[CurrentLanguage];
    }

    [Header("Language Test")]
    [SerializeField] Language LanguageToTest;

    [Button]
    public void TestLanguage()
    {
        CurrentLanguage = LanguageToTest;
        ChangeLanguage(LanguageToTest);
    }

#if UNITY_EDITOR
    [Button]
    public void GetAllTranslationsAndKeywords()
    {
        translations.Clear();
        keywords.Clear();

        List<Translation> newTranslations = EditorExtensionMethods.GetAllInstances<Translation>();

        for (int i = 0; i < newTranslations.Count; i++)
        {
            if (!this.translations.ContainsKey(newTranslations[i].Translations[English]))
            {
                this.translations.Add(newTranslations[i].Translations[English], newTranslations[i]);
            }
        }

        List<Keyword> newKeywords = EditorExtensionMethods.GetAllInstances<Keyword>();

        for (int i = 0; i < newKeywords.Count; i++)
        {
            if (!this.keywords.ContainsKey(newKeywords[i].Translations[English]))
            {
                this.keywords.Add(newKeywords[i].Translations[English], newKeywords[i]);
            }
        }
    }
#endif
}

[System.Serializable] public class Translations : SerializableDictionary<Language, string> { }
[System.Serializable] public class TranslationDictionary : SerializableDictionary<string, Translation> { }
[System.Serializable] public class KeywordDictionary : SerializableDictionary<string, Keyword> { }
