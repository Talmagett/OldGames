using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float width;
    public float height;
    public LayerMask WhatIsLayerEnemy;
    
    public Transform Center;
    private int Damage=10;
    void Start()
    {
        if (SpellsInstantiate.Spells.GetLvlOfLaserBeam() >= SpellsInstantiate.Spells.LaserBeamMax.maxValue) Damage = 20;
        else if (SpellsInstantiate.Spells.GetLvlOfLaserBeam() >= 20) Damage = 18;
        else if (SpellsInstantiate.Spells.GetLvlOfLaserBeam() >= 16) Damage = 16;
        else if (SpellsInstantiate.Spells.GetLvlOfLaserBeam() >= 12) Damage = 14;
        else if (SpellsInstantiate.Spells.GetLvlOfLaserBeam() >= 8) Damage = 12;
        else Damage = 10;
    }

    public void DamageToEnemy() {
        Collider2D[] EnemiesObjs = Physics2D.OverlapBoxAll(Center.position, new Vector2(width,height),0, WhatIsLayerEnemy);
        foreach (Collider2D enemy in EnemiesObjs)
        {
            if (enemy.tag == "Enemy")
            {
                if (SpellsInstantiate.Spells.GetLvlOfLaserBeam() >= SpellsInstantiate.Spells.LaserBeamMax.maxValue)
                {
                    float ExtraDamage = enemy.GetComponent<EnemyScript>().Health.maxValue - enemy.GetComponent<EnemyScript>().Health.value;
                    Damage += (int)(ExtraDamage * 0.1f);
                }
                enemy.GetComponent<EnemyScript>().TakeDamage(Damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Center.position, new Vector2(width, height));
    }
}
