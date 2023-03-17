using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Ball;
    public static GameController RedScore { get; private set; }
    public static GameController BlueScore { get; private set; }
    public bool _ballAlive;
    public Text RedScoreUI;
    public Text BlueScoreUI;
    public ParticleSystem RedWin;
    public ParticleSystem BlueWin;
    public GameObject CreationAnimObj;
    public GameObject WinPanel;
    public GameObject BlueWinsWindow;
    public GameObject RedWinsWindow;
    public GameObject PausePanel;

    public AudioClip GoalApplouse;
    public AudioClip WinClip;
    public AudioSource GoalAudioScource;
    public ChangeMusic changeMusicScript;

    private int _redScoreCount=0;
    private int _blueScoreCount=0;
    private bool _isGameOver;
    
    private void Awake()
    {
        Time.timeScale = 1;
        WinPanel.SetActive(false);
        RedScore = this;
        BlueScore = this;
    }
    private void Update()
    {
        if (!_ballAlive&&!_isGameOver) {
            _ballAlive = true;
            StartCoroutine(BallCreation());
        }

        if (_redScoreCount >= 9&&!_isGameOver)
        {
            GameOver();
            RedWinsWindow.SetActive(true);
        }
        else if (_blueScoreCount >= 9&&!_isGameOver) 
        {
            GameOver();
            BlueWinsWindow.SetActive(true);            
        }

        if (Input.GetKeyDown(KeyCode.Escape)&&!_isGameOver) {
            Time.timeScale = 0;
            WinPanel.SetActive(true);
            PausePanel.SetActive(true);
        }
    }

    IEnumerator BallCreation() {
        yield return new WaitForSeconds(0.5f);
        if (!_isGameOver)
        {
            CreationAnimObj.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            CreationAnimObj.SetActive(false);
            Instantiate(Ball, Vector2.zero, Quaternion.identity);
        }
    }

    public void AddScoreToRed() {
        GoalAudioScource.volume = Random.Range(0.1f, 0.9f);
        GoalAudioScource.PlayOneShot(GoalApplouse);
        _redScoreCount++;
        RedScoreUI.text = _redScoreCount.ToString();
        _ballAlive = false;
        RedWin.Play();
    }
    public void AddScoreToBlue()
    {
        GoalAudioScource.volume = Random.Range(0.1f, 0.9f);
        GoalAudioScource.PlayOneShot(GoalApplouse);
        _blueScoreCount++;
        BlueScoreUI.text = _blueScoreCount.ToString();
        _ballAlive = false;
        BlueWin.Play();
    }

    private void GameOver() {
        _isGameOver = true;
        StopCoroutine(BallCreation());
        WinPanel.SetActive(true);
        GameObject[] Objs = GameObject.FindGameObjectsWithTag("Players");
        changeMusicScript.WinMusic();
        GoalAudioScource.PlayOneShot(WinClip);
        GoalAudioScource.volume = 1f;
        foreach (GameObject item in Objs)
        {
            Destroy(item);
        }
        
    }

    public void ContinueGame() {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        WinPanel.SetActive(false);
    }

    public void RetryLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);

    }
    public void ToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
