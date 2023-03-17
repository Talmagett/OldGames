using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance { get; set; }
    public Text ScoreText;
    private int ScoreAmount;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void AddScore(int value) {
        ScoreAmount += value;
        ScoreText.text = ScoreAmount.ToString();
    }

    public int GetScore() {
        return ScoreAmount;
    }
}
