using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class IceBlastScript : MonoBehaviour
{
    public float IceBlastObjSpeed;
    public int DamageAmount;
    public float FrozenTime;
    public GameObject IceEffect;
    public GameObject childrenParticle;

    public float rangeOfCloud;
    public LayerMask WhatIsLayerEnemy;

    private float IceMagnitude;
    private Rigidbody2D _objRb;
    
    void Start()
    {
        _objRb = GetComponent<Rigidbody2D>();
        _objRb.velocity = Vector2.right * IceBlastObjSpeed;

    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (SpellsInstantiate.Spells.GetLvlOfIceBlast() >= SpellsInstantiate.Spells.IceBlastMax.maxValue) { FrozenTime = 3; IceMagnitude = 0f; }
            else {
                
                if (SpellsInstantiate.Spells.GetLvlOfIceBlast() >= 20) { FrozenTime = 3f; IceMagnitude = 0.08f; }
                else if (SpellsInstantiate.Spells.GetLvlOfIceBlast() >= 16) { FrozenTime = 2.5f; IceMagnitude = 0.16f; }
                else if (SpellsInstantiate.Spells.GetLvlOfIceBlast() >= 8) { FrozenTime = 2f; IceMagnitude = 0.24f; }
                else if (SpellsInstantiate.Spells.GetLvlOfIceBlast() >= 8) { FrozenTime = 1.5f; IceMagnitude = 0.32f; }
                else {FrozenTime = 1; IceMagnitude = 0.4f; }
            }
            Collider2D[] EnemiesObjs = Physics2D.OverlapCircleAll(transform.position, rangeOfCloud, WhatIsLayerEnemy);
            foreach (Collider2D enemy in EnemiesObjs)
            {
                if(enemy.GetComponent<EnemyScript>()) enemy.GetComponent<EnemyScript>().FrozenMagnitude = IceMagnitude;
                if (enemy.GetComponent<EnemyScript>()) enemy.GetComponent<EnemyScript>().FrozenCounter = FrozenTime;
            }
            GameObject IceEffectOnEnemy = Instantiate(IceEffect, transform.position, Quaternion.identity);
            IceEffectOnEnemy.transform.parent = other.transform;
            IceEffectOnEnemy.transform.position = other.transform.position;
            childrenParticle.transform.parent = null;
            childrenParticle.GetComponent<ParticleSystem>().Stop();
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeOfCloud);
    }
}
