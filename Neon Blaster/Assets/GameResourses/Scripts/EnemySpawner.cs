using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemy;
    private GameObject EnemySpawnerObject;
    public float SpawnDelay;
    private float nextSpawn;
    private float nextSpawnMass;
    private GameControllerScript GameControllScript;
    private MainMenu MenuScript;
    private BackgroundScript scoreScript;
    private int RandLength;
    int SKCounter = 0;
    private void Start()
    {
        EnemySpawnerObject = gameObject;
        GameControllScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        MenuScript = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        scoreScript = GameObject.Find("Background").GetComponent<BackgroundScript>();
    }

    void Update()
    {
        if (GameControllScript.isPlaying&&GameControllScript.isStart)
        {
            float randX = Random.Range(-transform.localScale.x / 2, transform.localScale.y / 2);
            Vector2 randSpawn = new Vector2(randX, transform.position.y-0.1f);
            if (Time.time > nextSpawn || GameObject.FindGameObjectsWithTag("Enemy").Length < 1)
            {
                float RandGoldEnemy = Random.Range(0, 100);
                if (RandGoldEnemy <5) Instantiate(Enemy[6], randSpawn, Quaternion.Euler(0, 0, 0));
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(Enemy[0], randSpawn, Quaternion.Euler(0, 0, 0));
                }
                nextSpawn = Time.time + SpawnDelay;
                if (Time.time>nextSpawnMass)
                {
                    int RandEnemy = Random.Range(1, MenuScript.LvlAddGun);
                    nextSpawnMass = Time.time + ((SpawnDelay-MenuScript.LvlAddGun)*3);
                    if (scoreScript.ScoreCount >= 10000 * SKCounter) { Instantiate(Enemy[6], randSpawn, Quaternion.Euler(0, 0, 0)); SKCounter++; }
                    else
                    {
                        if (RandEnemy > 2)
                        {
                            Instantiate(Enemy[RandEnemy], randSpawn, Quaternion.Euler(0, 0, 0));
                        }
                        else
                        {
                            Instantiate(Enemy[RandEnemy], randSpawn, Quaternion.Euler(0, 0, 0));
                            Instantiate(Enemy[RandEnemy], randSpawn, Quaternion.Euler(0, 0, 0));
                        }
                    }
                }
            }
        }
    }
}
