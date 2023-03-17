using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>
public class Enemy : MonoBehaviour {

    #region FIELDS
    [Tooltip("Health points in integer")]
    public int MaxHealth;
    private float health;
    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    public int EnemyLvl;
    public bool isBoss;
    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path
    #endregion

    private void Start()
    {
        if (!isBoss)
        {
            MaxHealth += MaxHealth * (PlayerShooting.instance.weaponPower / 3); 
            Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
            Invoke("ActivateShooting", Random.Range(3 + shotTimeMin, 3 + shotTimeMax)); 
        }
        health = MaxHealth;
        
    }

    //coroutine making a shot
    void ActivateShooting() 
    {
        if (Random.value < (float)shotChance / 100)                             //if random value less than shot probability, making a shot
        {
            if (Player.instance != null)
            {
                Vector3 dir = Player.instance.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                if (EnemyLvl == 1) Instantiate(Projectile, transform.position, Quaternion.identity).transform.rotation = Quaternion.Euler(0, 0, angle);
                if (EnemyLvl == 2)
                {
                    Instantiate(Projectile, transform.position, Quaternion.identity).transform.rotation = Quaternion.Euler(0, 0, angle + 5);
                    Instantiate(Projectile, transform.position, Quaternion.identity).transform.rotation = Quaternion.Euler(0, 0, angle - 5);
                }
                if (EnemyLvl == 3)
                {
                    Instantiate(Projectile, transform.position, Quaternion.identity).transform.rotation = Quaternion.Euler(0, 0, angle + 15);
                    Instantiate(Projectile, transform.position, Quaternion.identity).transform.rotation = Quaternion.Euler(0, 0, angle);
                    Instantiate(Projectile, transform.position, Quaternion.identity).transform.rotation = Quaternion.Euler(0, 0, angle - 15);
                }
            }
        }
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(float damage)
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
        {
            Destruction();
            ScoreCounter.Instance.AddScore(MaxHealth);
        }
        else
            Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
    }    

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile.GetComponent<Projectile>() != null)
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            else
                Player.instance.GetDamage(1);
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        Destroy(gameObject);
        if (isBoss) { Player.instance.WinScene(); }
    }
}
