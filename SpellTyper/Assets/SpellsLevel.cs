using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsLevel : MonoBehaviour
{  
    public Slider[] Sliders;
    void Start()
    {
        Sliders[0].value=PlayerPrefs.GetInt("Spell1");
        Sliders[1].value = PlayerPrefs.GetInt("Spell2");
        Sliders[2].value = PlayerPrefs.GetInt("Spell3");
        Sliders[3].value = PlayerPrefs.GetInt("Spell4");
        Sliders[4].value = PlayerPrefs.GetInt("Spell5");
        Sliders[5].value = PlayerPrefs.GetInt("Spell6");
        Sliders[6].value = PlayerPrefs.GetInt("Spell7");
        Sliders[7].value = PlayerPrefs.GetInt("Spell8");
        Sliders[8].value = PlayerPrefs.GetInt("Spell9");
        Sliders[9].value = PlayerPrefs.GetInt("Spell10");
    }

    void Update()
    {
        PlayerPrefs.SetInt("Spell1", (int)Sliders[0].value);
        PlayerPrefs.SetInt("Spell2", (int)Sliders[1].value);
        PlayerPrefs.SetInt("Spell3", (int)Sliders[2].value);
        PlayerPrefs.SetInt("Spell4", (int)Sliders[3].value);
        PlayerPrefs.SetInt("Spell5", (int)Sliders[4].value);
        PlayerPrefs.SetInt("Spell6", (int)Sliders[5].value);
        PlayerPrefs.SetInt("Spell7", (int)Sliders[6].value);
        PlayerPrefs.SetInt("Spell8", (int)Sliders[7].value);
        PlayerPrefs.SetInt("Spell9", (int)Sliders[8].value);
        PlayerPrefs.SetInt("Spell10", (int)Sliders[9].value);        
    }
}
