using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    public float Timer;
    public GameObject[] Enemies;
    public GameObject[] Boss;
    public Text SpellsPerSec;
    private int Counter;
    private float TimerCounter=1;
    private bool NoBody;
    void Start()
    {
           
    }
    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 2+ float.Parse(SpellsPerSec.text)*0.5f) {
            if (TimerCounter < 0)
            {
                SpawnEnemyStart();
            }
            else
            {
                TimerCounter -= Time.fixedDeltaTime; 
            }
        }
        
    }

    void SpawnEnemyStart() {
        Counter++;
        if (Counter > 5)
        {
            int randEnemy = Random.Range(0, Boss.Length);
            Instantiate(Boss[randEnemy], transform.position, Quaternion.identity);
            Counter = 0;
            TimerCounter = Timer * 3;
        }
        else
        {
            int randEnemy = Random.Range(0, Enemies.Length);
            Instantiate(Enemies[randEnemy], transform.position, Quaternion.identity);
            TimerCounter = Timer;
        }
    }
}
