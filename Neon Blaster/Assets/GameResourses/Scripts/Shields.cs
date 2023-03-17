using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{
    public GameObject[] Shield;
    private MainMenu mainMenuScript;
    void Start()
    {
        mainMenuScript = GameObject.Find("MainMenu").GetComponent<MainMenu>();
    }

    void Update()
    {
        if (mainMenuScript.LvlShield <= 0)
        {
            for (int i = 0; i < mainMenuScript.MaxLvlShield; i++)
                if (Shield[i] != null) Shield[i].SetActive(false);
        }
        else {
            for (int i = 0; i < mainMenuScript.LvlShield; i++)
                if(Shield[i]!=null)Shield[i].SetActive(true);
        }
    }
}


