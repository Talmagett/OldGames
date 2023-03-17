using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class EngRus : MonoBehaviour
{
    public string EngWord;
    public string RusWord;
    public bool HasToggles;
    public GameObject EngToggles;
    public GameObject RusToggles;

    private int LangIndex=0;
    private Text textToChange;
    void Start()
    {
        textToChange = GetComponent<Text>();
        LangIndex = PlayerPrefs.GetInt("Language");
        if (LangIndex == 0)
        {
            textToChange.text = EngWord;
            if (HasToggles)
            {
                EngToggles.SetActive(true);
                RusToggles.SetActive(false);
            }
        }
        else if (LangIndex == 1)
        {
            textToChange.text = RusWord;
            if (HasToggles)
            {
                EngToggles.SetActive(false);
                RusToggles.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (TranslationComponentMain.Translator.LangValue == 0)
        {
            textToChange.text = EngWord;
            if (HasToggles) { 
                EngToggles.SetActive(true);
                RusToggles.SetActive(false);
            }
        }
        else if (TranslationComponentMain.Translator.LangValue == 1)
        {
            textToChange.text = RusWord;
            if (HasToggles)
            {
                EngToggles.SetActive(false);
                RusToggles.SetActive(true);
            }
        }
    }
}
