using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class BackgroundScript : MonoBehaviour
{
    public Text ScoreTextInBG;
    public Text HighScoreText;
    public int ScoreCount;
    public Text MoneyText;
    public int MoneyCount;

    private Save sv = new Save();
    private string path;

    private void Start()
    { 
        path = Path.Combine(Application.dataPath,"Save.json");
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            HighScoreText.text = sv.HighScoreSave.ToString();
            MoneyCount = sv.MoneySave;
        }
        MoneyText.text = MoneyCount.ToString();
    }

    void Update()
    {
        ScoreTextInBG.text = ScoreCount.ToString();
        MoneyText.text = MoneyCount.ToString();
        sv.MoneySave = MoneyCount;
        if (ScoreCount > sv.HighScoreSave)
        {
            sv.HighScoreSave = ScoreCount;
            HighScoreText.text = ScoreCount.ToString();
        }
    }
    public void Reset()
    {
        ScoreCount = 0;
        MoneyCount = 0;
        sv.HighScoreSave = 0;
        sv.MoneySave = 0;
        sv.HighScoreSave = 0;
        HighScoreText.text = 0.ToString();
    }
    private void OnApplicationQuit()
    {
        File.WriteAllText(path,JsonUtility.ToJson(sv));
    }
}


[Serializable]
public class Save {
    public int MoneySave;
    public int HighScoreSave;
}