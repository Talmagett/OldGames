using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeText : MonoBehaviour
{
    private Text TimeTextShow;
    public float TimeCounter=0;
    private int HighScore;

    void Start()
    {
        TimeTextShow=GetComponent<Text>();
        HighScore=PlayerPrefs.GetInt("HighScoreSave"); 
    }

    void Update()
    {
        TimeCounter += Time.deltaTime;
        if (Time.timeScale == 0)
        {
            TimeTextShow.text = HighScore.ToString();
        }
        else
        {
            TimeTextShow.text = ((int)TimeCounter).ToString();
        }
        if (HighScore < (int)TimeCounter) {HighScore = (int)TimeCounter;
            PlayerPrefs.SetInt("HighScoreSave",HighScore);
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
