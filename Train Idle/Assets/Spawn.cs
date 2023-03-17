using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    public static Spawn Instanse { get; set; }
    public float SpawnAngle;
    public GameObject[] Objects;
    public float timerCD;

    public int SpawnAmounts;
    public float Speed;
    public float SpawnDecreaser;
    [SerializeField]
    private int Lives;
    public float WaitAfterSpawn;
    public int GetLive { get { return Lives; } }
    public GameObject[] LivesImage;
    public GameObject WinWindow;
    public GameObject LoseWindow;
    public GameObject PauseWindow;
    private void Awake()
    {
        Instanse = this;
    }
    private IEnumerator Start()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(2);
        for (; SpawnAmounts >0; SpawnAmounts--)
        {
            if (Lives > 0)
            {
                Instantiate(Objects[Random.Range(0, Objects.Length)], transform.position, Quaternion.Euler(0, 0, SpawnAngle)).SendMessage("SetSpeed", Speed); ;
                yield return new WaitForSeconds(timerCD);
                timerCD = Mathf.Clamp(timerCD - timerCD / SpawnDecreaser, 0.5f, 2);
            }
            else break;
        }
        if (Lives > 0) { 
            yield return new WaitForSeconds(WaitAfterSpawn);
            Win();
        }
    }
    public void LoseLive() {
        Lives--;        
        if (Lives <= 0) GameOver();
        if(Lives>=0) LivesImage[Lives].SetActive(false);
    }

    void Win() {
        WinWindow.SetActive(true);
        if(Lives>PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "HighScore"))
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"HighScore",Lives);
    }
    void GameOver() {
        LoseWindow.SetActive(true);
    }
    public void Pause() {
        PauseWindow.SetActive(!PauseWindow.activeSelf);
        if (PauseWindow.activeSelf) Time.timeScale = 0;else Time.timeScale = 1;
    }
    public void ToMenu() {
        SceneManager.LoadScene(0);
    }
    public void NextLvl() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
