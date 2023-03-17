using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroComponent : MonoBehaviour
{
    public static HeroComponent instance { get; set; }
    public float HPMax;
    public Slider HPSlider;
    public GameObject PausePanel;
         
    private void Awake()
    {
        instance = this;
        
    }
    void Start()
    {
        HPSlider.maxValue = HPMax;
        HPSlider.value = PlayerPrefs.GetFloat("PlayerHp", HPMax);
    }

    public void GetDamage(float value)
    {
        HPSlider.value -= value;
        if (HPSlider.value <= 0) {
            MainController.instance.ClearXP();
            SceneManager.LoadScene(1);
        }
    }
    public void Heal(float value) {
        HPSlider.value += value;
        print(HPSlider.value);
    }
}
