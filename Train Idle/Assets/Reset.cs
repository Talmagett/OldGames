using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public LevelProp[] props;
    
    public void ResetScores() {
        for (int i = 1; i < 6; i++)
        {
            PlayerPrefs.SetInt("Level"+i+"HighScore",0);
            props[i - 1].SetStars();
        }
        
    }
}
