using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Slider slider;
    public Toggle Timer;
    public Toggle Turns;
    private void Start()
    {
        switch (PlayerPrefs.GetInt("GameMode"))
        {
            case 1:
                Timer.isOn = true; Turns.isOn = false; break;
            case 2:
                Timer.isOn = false; Turns.isOn = true; break;
            case 3:
                Timer.isOn = true; Turns.isOn = true; break;
            default:
                Timer.isOn = false; Turns.isOn = false; break;
        }
        slider.value = PlayerPrefs.GetInt("Scale");
    }
    public void ToDouble() {
        PlayerPrefs.SetInt("Scale", (int)slider.value);
        //PlayerPrefs.SetString("GameMode",);
        Settings();
        SceneManager.LoadScene("DoublePair");
    }
    void Settings() {
        int sender=0+(Timer.isOn?1:0)+(Turns.isOn?2:0);
        PlayerPrefs.SetInt("GameMode", sender);
    }
    public void ToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void ThemeChange(int id) {
        PlayerPrefs.SetInt("Theme", id);
    }
}
