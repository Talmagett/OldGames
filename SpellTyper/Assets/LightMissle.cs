using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMissle : MonoBehaviour
{
    public float MissleObjSpeed;
    public int DamageAmount;
    public GameObject DeathEff;
    public float rangeOfFireExpl;
    public LayerMask WhatIsLayerEnemy;
    public float DamageIncreased;

    private Rigidbody2D _objRb;
    void Start()
    {
        _objRb = GetComponent<Rigidbody2D>();
        _objRb.velocity = Vector2.right * MissleObjSpeed;
        DamageAmount += (int)DamageIncreased;
        transform.localScale = new Vector2(transform.localScale.x + DamageIncreased * 0.05f, transform.localScale.y+ DamageIncreased * 0.05f);
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Collider2D[] EnemiesObjs = Physics2D.OverlapCircleAll(transform.position, rangeOfFireExpl, WhatIsLayerEnemy);
            foreach (Collider2D enemy in EnemiesObjs)
            {
                if (enemy.GetComponent<EnemyScript>()) enemy.GetComponent<EnemyScript>().TakeDamage(DamageAmount);
            }
            Instantiate(DeathEff, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeOfFireExpl);
    }
}
