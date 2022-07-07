using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Managers/Language Manager")]
public class LanguageManager : Factories.FactoryBase
{
    public static TranslationDictionary Translations = new TranslationDictionary();
    [SerializeField, ReadOnly] TranslationDictionary translations = new TranslationDictionary();
    public static Language CurrentLanguage;
    [SerializeField] Language English;

    public static System.Action OnLanguageChanged;

    public override void Initialise()
    {
        Translations = translations;

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
    public void GetAllTranslations()
    {
        List<Translation> translations = EditorExtensionMethods.GetAllInstances<Translation>();

        for (int i = 0; i < translations.Count; i++)
        {
            if (!this.translations.ContainsKey(translations[i].Translations[English]))
            {
                this.translations.Add(translations[i].Translations[English], translations[i]);
            }
        }
    }
#endif
}



[System.Serializable] public class TranslationDictionary : SerializableDictionary<string, Translation> { }
