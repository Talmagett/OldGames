using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMissleScript : MonoBehaviour
{
    private Transform LaserMissle;
    public GameObject Explosion;
    public float MissleSpeed;
    private EnemyScript enemyScript;
    private MainMenu mainMenuScript;
    public int Damage = 1;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    void Start()
    {
        mainMenuScript = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        LaserMissle = gameObject.transform;
        Damage = mainMenuScript.LvlDamage*mainMenuScript.LvlLaserGun;
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
            Instantiate(Explosion, transform.position, Quaternion.identity);
            ExplosionFunk();
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "GameArea")
        {
            Destroy(gameObject);
        }
    }
    void ExplosionFunk() {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyScript>().Health -= Damage;
        }
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}