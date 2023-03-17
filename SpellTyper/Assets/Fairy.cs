using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour
{
    public GameObject Heal;
    public float HealCD;
    public int HealAmount;
    public float rangeOfHeal;
    public LayerMask WhatIsLayerEnemy;
    public GameObject DeathEff;
    private EnemyScript enemyScript;
    private EnemyAttack enemyAttackScript;
    private Animator EnemyAnimator;
    private float HealCounter;
    void Start()
    {
        enemyScript = GetComponent<EnemyScript>();
        HealCounter = HealCD;
        EnemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        HealCounter -= Time.deltaTime;
        if (HealCounter <= 0)
        {
            EnemyAnimator.SetTrigger("Heal");
            HealCounter = HealCD;
            
        }
        if (enemyScript._isDead)
        {
    
            Instantiate(DeathEff, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
        public void HealFairy() {
            GameObject HealEffLast = Instantiate(Heal, transform.position, Quaternion.identity);
            HealEffLast.transform.parent = transform;
            Collider2D[] EnemiesObjs = Physics2D.OverlapCircleAll(transform.position, rangeOfHeal, WhatIsLayerEnemy);
            foreach (Collider2D enemy in EnemiesObjs)
            {
                if (enemy.GetComponent<EnemyScript>()) enemy.GetComponent<EnemyScript>().TakeDamage(-HealAmount);
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangeOfHeal);
        }

    } 

