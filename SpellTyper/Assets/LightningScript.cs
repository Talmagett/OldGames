using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public int DamageAmount;
    public GameObject LightningEffect;
    private Rigidbody2D _objRb;
    private GameObject randEnemy;
    void Start()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (Enemies.Length > 0)
        {
            randEnemy = Enemies[Random.Range(0, Enemies.Length)];

            _objRb = GetComponent<Rigidbody2D>();
        }
        else {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(randEnemy)
        transform.position = randEnemy.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.GetComponent<EnemyScript>()) other.gameObject.GetComponent<EnemyScript>().TakeDamage(DamageAmount);
            Instantiate(LightningEffect, transform.position, Quaternion.identity);
            Destroy(gameObject,1f);
        }
    }
}
