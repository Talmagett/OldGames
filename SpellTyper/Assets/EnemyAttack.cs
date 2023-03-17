using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float rangeOfAttack;
    public LayerMask WhatIsLayerPlayer;
    public float AttackTimer;
    public int DamageAmount;
    [Space]
    public AudioClip[] AttackClips;

    private Animator EnemyAnimator;
    private float AttackCDCounter;
    private AudioSource Source;
    void Awake()
    {
        Source = GameObject.Find("Audio").GetComponent<AudioSource>();
    }
    private void Start()
    {
        AttackCDCounter = AttackTimer;
        EnemyAnimator=GetComponent<Animator>();
    }
    void Update()
    {
        AttackCDCounter -= Time.deltaTime;
        if (AttackCDCounter<=0) {
            Collider2D[] MageOrShield = Physics2D.OverlapCircleAll(transform.position, rangeOfAttack, WhatIsLayerPlayer);
            foreach (Collider2D mage in MageOrShield)
            {
                if (mage.gameObject.tag == "Player")
                {
                    EnemyAnimator.SetTrigger("Attack");
                    AttackCDCounter = AttackTimer;
                }
            } 
        }
    }

    
    
    public void AttackFunc() {
        AttackCDCounter = AttackTimer;
        Collider2D[] MageOrShield = Physics2D.OverlapCircleAll(transform.position, rangeOfAttack, WhatIsLayerPlayer);
        foreach (Collider2D mage in MageOrShield)
        {
            if (mage.gameObject.tag == "Player")
            {
                if (mage.gameObject.GetComponent<MageScript>()) mage.gameObject.GetComponent<MageScript>().TakeDamage(DamageAmount);
                if (mage.gameObject.GetComponent<Shield>()) mage.gameObject.GetComponent<Shield>().TakeDamage(DamageAmount);
                if (AttackClips.Length >0) {
                    int Rand = Random.Range(0, AttackClips.Length);
                    Source.PlayOneShot(AttackClips[Rand]);
                }
            }
        }
        
        
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeOfAttack);
    }
}
