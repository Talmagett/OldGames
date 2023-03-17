using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private float CooldownCounter;
    private float CooldownMax = 3f;
    public float speed;
    public GameObject LightningEffect;
    GameObject[] Enemies;
    private EnemyScript enemyScript;
    private MainMenu mainMenuScript;
    int i = 0;
    void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        CooldownCounter = CooldownMax;
        mainMenuScript = GameObject.Find("MainMenu").GetComponent<MainMenu>();
    }

    void Update()
    {
        if (i < Enemies.Length) {
            if (CooldownCounter >= 0)
            {
                if (Enemies[i] == null) { i++; }
                else
                {
                    CooldownCounter -= Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, Enemies[i].transform.position, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, Enemies[i].transform.position) < 0.1f)
                    {
                        Instantiate(LightningEffect, Enemies[i].transform.position, Quaternion.identity);
                        enemyScript = Enemies[i].GetComponent<EnemyScript>();
                        enemyScript.Health= enemyScript.Health - mainMenuScript.LvlDamage*mainMenuScript.LvlBonuses;
                        i++;
                    }
                }
            } }
        else Destroy(gameObject);

}
    
}
