using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocalisationSystem : MonoBehaviour
{
    public enum Language
    {
        English,
        Swedish,
    }

    public static Language language = Language.Swedish;

    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedSV;


    public static bool isInit;

    public static void SetLanguage(string languageToSet)
    {
        if (languageToSet.Equals("Swedish"))
        {
            language = Language.Swedish;
        }
        else
        {
            language = Language.English;
        }
    }

    public static void SetLanguageBySystem()
    {
        if (Application.systemLanguage == SystemLanguage.Swedish)
        {
            language = Language.Swedish;

        }
        else
        {
            language = Language.English;
        }
    }


    public static string GetLanguage()
    {
        if (language == Language.English)
        {
            return "English";
        }
        else
        {
            return "Swedish";
        }
    }

    public static void Init()
    {
        TXTLoader txtLoader = new TXTLoader();
        txtLoader.LoadTXT();

        localisedEN = txtLoader.GetDictionaryValues("en");
        localisedSV = txtLoader.GetDictionaryValues("sv");

        isInit = true;
    }
    

    public static string GetLocalisedValue(string key)
    {
        if (!isInit)
        {
            Init();
        }

        string value = key;

        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Swedish:
                localisedSV.TryGetValue(key, out value);
                break;
        }

        return value;
    }

}
