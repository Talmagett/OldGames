using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{
    public Slider Health;
    public float EnemySpeed;
    public float FrozenCounter;
    public float FrozenMagnitude;
    [Range(0, 11)]
    public float isWeak;
    public GameObject ExtraPoisonCloud;
    public float IsPoisonedCounter;
    public bool _isDead;
    public float AttackRange;
    private Animator EnemyAnim;
    private Rigidbody2D _enemyRB;
    private GameObject Mage;
    private float MaledictionMagnitude;

    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        Mage = GameObject.FindGameObjectWithTag("Player");
        _enemyRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (SpellsInstantiate.Spells.GetLvlOfMalediction() < SpellsInstantiate.Spells.MaledictionMax.maxValue && isWeak > 0)
        {
            isWeak -= Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, Mage.transform.position) <= AttackRange)
        {
            EnemyAnim.SetBool("Idle", true);
        }
        else
        {
            EnemyAnim.SetBool("Idle", false);
            if (FrozenCounter > 0)
            {
                FrozenCounter -= Time.deltaTime;
                _enemyRB.velocity = -Vector2.right * EnemySpeed * FrozenMagnitude;
            }
            else
            {
                _enemyRB.velocity = -Vector2.right * EnemySpeed;
            }
        }
        if (IsPoisonedCounter > -1) IsPoisonedCounter -= Time.deltaTime;
        if (Health.value <= 0)
        {
            if (IsPoisonedCounter > 0)
            {
                Instantiate(ExtraPoisonCloud, transform.position, Quaternion.identity);
            }
            Dead();
        }
    }

    public void TakeDamage(int _damageAmount)
    {

        if (isWeak > 1)
        {
            if (SpellsInstantiate.Spells.GetLvlOfMalediction() >= SpellsInstantiate.Spells.MaledictionMax.maxValue) { MaledictionMagnitude = 2f; }
            else if (SpellsInstantiate.Spells.GetLvlOfMalediction() >= 20) { MaledictionMagnitude = 1.85f; }
            else if (SpellsInstantiate.Spells.GetLvlOfMalediction() >= 16) { MaledictionMagnitude = 1.7f; }
            else if (SpellsInstantiate.Spells.GetLvlOfMalediction() >= 12) { MaledictionMagnitude = 1.55f; }
            else if (SpellsInstantiate.Spells.GetLvlOfMalediction() >= 8) { MaledictionMagnitude = 1.4f; }
            else MaledictionMagnitude = 1.2f;
            Health.value -= MaledictionMagnitude * _damageAmount;
        }
        else
        {
            Health.value -= _damageAmount;
        }
    }
    void Dead()
    {
        _isDead = true;
    }

}
