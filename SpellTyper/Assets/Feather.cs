using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{
    public float Speed;
    public int Damage;
    public GameObject Explosion;
    private float Delay;
    public float rangeOfFireExpl;
    public LayerMask WhatIsLayerEnemy;
    IEnumerator Start()
    {
        Delay = 5f;
        if (SpellsInstantiate.Spells.GetLvlOfSummon() >= (int)SpellsInstantiate.Spells.SummonMax.maxValue) { Damage = 10;
            Delay = 10f;
        }
        else if (SpellsInstantiate.Spells.GetLvlOfSummon() >= 20) Damage = 9;
        else if (SpellsInstantiate.Spells.GetLvlOfSummon() >= 16) Damage = 8;
        else if (SpellsInstantiate.Spells.GetLvlOfSummon() >= 12) Damage = 7;
        else if (SpellsInstantiate.Spells.GetLvlOfSummon() >= 8) Damage = 6;
        else Damage = 5;
        
        yield return new WaitForSeconds(Delay);
        Boom();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Boom();
        }

    }
    public void Boom() {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Collider2D[] EnemiesObjs = Physics2D.OverlapCircleAll(transform.position, rangeOfFireExpl, WhatIsLayerEnemy);
        foreach (Collider2D enemy in EnemiesObjs)
        {
            if(enemy.GetComponent<EnemyScript>()) enemy.GetComponent<EnemyScript>().TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeOfFireExpl);
    }
}
