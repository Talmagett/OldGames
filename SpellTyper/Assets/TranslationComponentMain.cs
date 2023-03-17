using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslationComponentMain : MonoBehaviour
{
    public static TranslationComponentMain Translator { get; set; }
    public Slider EngRusElement;
    public int LangValue;

    private void Awake()
    {
        Translator = this;
        LangValue= PlayerPrefs.GetInt("Language");
        EngRusElement.value = LangValue;
    }
    public void ChangedLang() {
        if (EngRusElement.value == 0) {
            LangValue = 0;
            PlayerPrefs.SetInt("Language",0);
        }else if (EngRusElement.value == 1)
        {
            LangValue = 1;
            PlayerPrefs.SetInt("Language", 1);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Language", LangValue);
    }

}
