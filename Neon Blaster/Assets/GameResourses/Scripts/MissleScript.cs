using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : MonoBehaviour
{
    private Transform Missle;
    public float MissleSpeed;
    private EnemyScript enemyScript;
    private MainMenu mainMenuScript;
    public int Damage=1;
    public Sprite Yellow;
    public Sprite Green;
    public Sprite Red;
    public Sprite Blue;

    void Start()
    {
        mainMenuScript = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        Missle = gameObject.transform;
        Damage = mainMenuScript.LvlDamage;
        if (mainMenuScript.LvlDamage < 5) gameObject.GetComponent<SpriteRenderer>().sprite = Green;
        else if (mainMenuScript.LvlDamage < 10) gameObject.GetComponent<SpriteRenderer>().sprite = Yellow;
        else if (mainMenuScript.LvlDamage < 15) gameObject.GetComponent<SpriteRenderer>().sprite = Red;
        else if (mainMenuScript.LvlDamage <= 20) gameObject.GetComponent<SpriteRenderer>().sprite = Blue;
        
    }

    void FixedUpdate()
    {
        transform.position += transform.up*Time.deltaTime * MissleSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Missle") return;
        if (other.gameObject.tag == "Enemy")
        {
            enemyScript= other.GetComponent<EnemyScript>();
            enemyScript.Health = enemyScript.Health - Damage;
            Destroy(gameObject);
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