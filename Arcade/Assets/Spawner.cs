using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject[] Figures;
    public Text ScoreText;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    int Score=0;
    bool isGameOver;
    public float FallTime;
    void Start()
    {
        CreateNewFigure();
        ScoreText.text = "0";
        Time.timeScale = 1;
    }

    public void CreateNewFigure()
    {
        if (!isGameOver) { 
            Instantiate(Figures[Random.Range(0, Figures.Length)], transform.position, Quaternion.identity);
            FallTime=Mathf.Clamp( FallTime - 0.01f,0.2f,1f);
        }
    }
    public void AddScore() {
        Score++;
        ScoreText.text = Score.ToString();
    }
    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        this.enabled = false;
        isGameOver = true;
    }

    public void Pause() {
        Time.timeScale = (Time.timeScale > 0.5f) ? 0 : 1;
        PausePanel.SetActive(!PausePanel.activeSelf);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(0);
    }
}
