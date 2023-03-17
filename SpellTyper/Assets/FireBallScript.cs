using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireBallScript : MonoBehaviour
{
    public float FireBallObjSpeed;
    public int DamageAmount;
    public GameObject FireEffect;
    public float rangeOfFireExpl;
    public LayerMask WhatIsLayerEnemy;
    public GameObject Explosion;
    public float DamageIncreased;

    private Rigidbody2D _objRb;
    void Start()
    {
        _objRb = GetComponent<Rigidbody2D>();
        _objRb.velocity = Vector2.right* FireBallObjSpeed;
    }


    private void Update()
    {
        if (SpellsInstantiate.Spells.GetLvlOfFireBall() >= 30)
        {
            DamageIncreased += Time.deltaTime*5;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DamageAmount += (int)DamageIncreased;
            if (SpellsInstantiate.Spells.GetLvlOfFireBall() >= 30)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Collider2D[] EnemiesObjs = Physics2D.OverlapCircleAll(transform.position, rangeOfFireExpl, WhatIsLayerEnemy);
                foreach (Collider2D enemy in EnemiesObjs)
                {
                    if (enemy.GetComponent<EnemyScript>()) enemy.GetComponent<EnemyScript>().TakeDamage(DamageAmount);
                }
            }
            else
            {
                other.gameObject.GetComponent<EnemyScript>().TakeDamage(DamageAmount);
            }
            Instantiate(FireEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeOfFireExpl);
    }
}
