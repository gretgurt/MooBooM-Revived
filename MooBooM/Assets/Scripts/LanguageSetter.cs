using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSetter : MonoBehaviour
{

    TextLocaliserUI[] textLocaliserUI;

    // Start is called before the first frame update
    void Awake()
    {
        if(PlayerPrefs.GetString("Language") != null) { 
        string languageID = PlayerPrefs.GetString("Language");
        LocalisationSystem.SetLanguage(languageID);
        }
        else
        {
            LocalisationSystem.SetLanguageBySystem();
            string languageID = LocalisationSystem.GetLanguage();
            PlayerPrefs.SetString("Language", languageID);
        }

        textLocaliserUI = FindObjectsOfType<TextLocaliserUI>();
        /*if (!PlayerPrefs.GetString("Language", "defaultValue").Equals("defaultValue"))
        {
            LocalisationSystem.SetLanguage(PlayerPrefs.GetString("Language"));
        }
        else
        {
            LocalisationSystem.SetLanguageBySystem();
            PlayerPrefs.SetString("Language", LocalisationSystem.GetLanguage());
            Debug.Log("Changed language by system");
        }*/

    }


    public void ChangeToSwedish()
    {
        LocalisationSystem.language = LocalisationSystem.Language.Swedish;
        PlayerPrefs.SetString("Language", LocalisationSystem.GetLanguage());
        for (int i = 0; i < textLocaliserUI.Length; i++)
        {
            textLocaliserUI[i].UpdateLanguage();
            /*if (LocalisationSystem.language.Equals(LocalisationSystem.Language.Swedish))
            {
                LocalisationSystem.language = LocalisationSystem.Language.English;
            }
            else
            {
                LocalisationSystem.language = LocalisationSystem.Language.Swedish;
            }
            PlayerPrefs.SetString("Language", LocalisationSystem.GetLanguage());
            for (int i = 0; i < textLocaliserUI.Length; i++) {
                textLocaliserUI[i].UpdateLanguage(); 
            }
            Debug.Log("Toggle toggle! " + LocalisationSystem.GetLanguage());*/
        }
    }

    public void ChangeToEnglish()
    {
        LocalisationSystem.language = LocalisationSystem.Language.English;
        PlayerPrefs.SetString("Language", LocalisationSystem.GetLanguage());
        for (int i = 0; i < textLocaliserUI.Length; i++)
        {
            textLocaliserUI[i].UpdateLanguage();
        }
    }

}
