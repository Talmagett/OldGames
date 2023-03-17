using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private Transform Missle;
    public float MissleSpeed;
    private EnemyScript enemyScript;
    private MainMenu mainMenuScript;
    private int Damage = 1;

    void Start()
    {
        mainMenuScript = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        Missle = gameObject.transform;
        Damage = mainMenuScript.LvlDamage*5+20;
    }

    void FixedUpdate()
    {
        transform.position += transform.up * Time.deltaTime * MissleSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Missle") return;
        if (other.gameObject.tag == "Enemy")
        {
            enemyScript = other.GetComponent<EnemyScript>();
            enemyScript.Health = enemyScript.Health - Damage;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "GameArea")
        {
            Destroy(gameObject);
        }
    }
}
