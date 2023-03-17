using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProp : MonoBehaviour
{
    public string SceneName;
    public GameObject[] Lives;

    int livesCount;

    private void Start()
    {
        SetStars();
    }
    public void SetStars() {
        livesCount = PlayerPrefs.GetInt(SceneName + "HighScore");
        for (int i = 0; i < livesCount; i++)
        {
            Lives[i].SetActive(true);
        }
    }
    public void PlayScene() {
        SceneManager.LoadScene(SceneName);
    }
}
