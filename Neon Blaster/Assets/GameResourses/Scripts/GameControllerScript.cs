using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public bool isPlaying = false;
    public bool isStart = false;
    public GameObject BG;
    public GameObject MainMenu;
    public GameObject HeroObject;
    private GameObject Hero;
    private AudioClip Sound;
    private AudioSource Sourse;

    private void Start()
    {
        Sourse = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (!isPlaying&&!isStart)
        {
            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in Enemies)
            {
                enemy.GetComponent<EnemyScript>().EnemyCostToScore = 0;
                Destroy(enemy);
            }
            GameObject[] Money = GameObject.FindGameObjectsWithTag ("Money");
            foreach (GameObject money in Money)
            {
                Destroy(money);
            }
            GameObject[] Missles = GameObject.FindGameObjectsWithTag("Missle");
            foreach (GameObject missles in Missles)
            {
                Destroy(missles);
            }
            MainMenu.SetActive(true);
            isStart = false;
            Time.timeScale = 1f;            
        }        
    }

    public void PlayButton() {
        BG.GetComponent<BackgroundScript>().ScoreCount = 0;
        MainMenu.SetActive(false);
        Hero=Instantiate(HeroObject);
        Hero.transform.position = new Vector2(0, -5.25f);
        isPlaying = true;
        Invoke("DoStart",0.5f);
    }

    void DoStart()
    {
        isStart = true;
        
    }
    public void GameRestart()
    {
        Invoke("Restart", 1f);
    }

    public void Restart() {
        GameObject[] Hero = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject objs in Hero)
        {
            Destroy(objs);
        }
        
        isPlaying = false;
        isStart = false;
        Time.timeScale = 1;
    }

    public void PlaySound(AudioClip Sound,float Volume) {
        Sourse.PlayOneShot(Sound);
        Sourse.volume = Volume;
    }
}
