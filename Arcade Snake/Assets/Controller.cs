using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Controller
{
    public static int scoreCount;
    public static void AddScore(Text ScoreText) {
        scoreCount++;
        ScoreText.text = scoreCount.ToString();
    }
}
