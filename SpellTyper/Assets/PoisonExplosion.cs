using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonExplosion : MonoBehaviour
{
    public float rangeOfCloud;
    public LayerMask WhatIsLayerEnemy;

    void Start()
    {
        Collider2D[] EnemiesObjs = Physics2D.OverlapCircleAll(transform.position, rangeOfCloud, WhatIsLayerEnemy);
        foreach (Collider2D enemy in EnemiesObjs)
        {
            if(enemy.GetComponent<EnemyScript>()) enemy.GetComponent<EnemyScript>().TakeDamage((int)(enemy.GetComponent<EnemyScript>().Health.maxValue * 0.1f));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeOfCloud);
    }
}
