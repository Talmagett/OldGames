using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloudScript : MonoBehaviour
{
    
    public float rangeOfCloud;
    public LayerMask WhatIsLayerEnemy;
    public GameObject PoisonEff;


    private int PoisonDamage;
    private bool PoisonMax;
    IEnumerator Start()
    {
        if (SpellsInstantiate.Spells.GetLvlOfPoisonCloud() >= 30) {PoisonDamage = 10; PoisonMax = true; }
        else if (SpellsInstantiate.Spells.GetLvlOfPoisonCloud() >= 20) PoisonDamage = 10;
        else if (SpellsInstantiate.Spells.GetLvlOfPoisonCloud() >= 16) PoisonDamage = 9;
        else if (SpellsInstantiate.Spells.GetLvlOfPoisonCloud() >= 12) PoisonDamage = 8;
        else if (SpellsInstantiate.Spells.GetLvlOfPoisonCloud() >= 8) PoisonDamage = 7;
        else PoisonDamage = 6;
        for (int i = 0; i < 8; i++)
        {
            Collider2D[] EnemiesObjs = Physics2D.OverlapCircleAll(transform.position,rangeOfCloud,WhatIsLayerEnemy);
            foreach (Collider2D enemy in EnemiesObjs)
            {
                if(enemy.GetComponent<EnemyScript>())enemy.GetComponent<EnemyScript>().TakeDamage(PoisonDamage);
                if (PoisonMax)
                {
                    Instantiate(PoisonEff, enemy.transform.position, Quaternion.identity);
                    if(enemy.GetComponent<EnemyScript>()) enemy.GetComponent<EnemyScript>().IsPoisonedCounter = 2;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,rangeOfCloud);
    }
}
