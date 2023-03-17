using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Infection : MonoBehaviour
{
    public float Speed;
    public float Damage;
    public Spell InfectionSpell;

    public AudioClip Cast;

    AudioSource source;
    bool isSearchingEnemy=true;
    GameObject enemyOn;
    Slider enemyHp;
    Collider colliderArea;
    float LifeTime=6;
    bool extraLife;
    bool Increasing;
    float DamageCounter;
    void Start()
    {
        colliderArea = GetComponent<Collider>();
        source = GetComponent<AudioSource>();
        MainController.instance.AudioPlayer0_8.PlayOneShot(Cast);
        if (InfectionSpell.CurrentXp >= InfectionSpell.XPToUpgrade[0])
        {
            Damage *= 1.5f;
            if (InfectionSpell.CurrentXp >= InfectionSpell.XPToUpgrade[1])
            {
                extraLife = true;
                if (InfectionSpell.CurrentXp >= InfectionSpell.XPToUpgrade[2])
                {

                    Increasing = true;
                }
            }
        }
    }
    bool firsttarget;
    void Update()
    {
        if (isSearchingEnemy)
        {
            if(!firsttarget)
                transform.position += transform.up * Time.deltaTime * Speed;
            else
                transform.position += Vector3.right * Time.deltaTime * Speed;
        }
        else {
            if (enemyOn)
            {
                colliderArea.enabled = false;
                transform.position = enemyOn.transform.position;
                if (DamageCounter < 0)
                {
                    if (Increasing)
                        enemyOn.SendMessage("GetDamage", new Vector2(Damage * (enemyHp.value <= enemyHp.maxValue * 0.5f ? 2 : 1), 3));
                    else
                        enemyOn.SendMessage("GetDamage", new Vector2(Damage, 3));
                    DamageCounter = 0.2f;
                }

                DamageCounter -= Time.deltaTime;
                LifeTime -= Time.deltaTime;

            }
            else {
                colliderArea.enabled = true;
                isSearchingEnemy = true;
                source.enabled = false;
                if (extraLife)
                    LifeTime = 6;
            }
        }
        if (LifeTime <= 0) Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyOn = collision.gameObject;
            enemyHp = enemyOn.GetComponent<CreatureProp>().HPSlider;
            isSearchingEnemy = false;
            firsttarget = true;
            source.enabled = true;
        }
    }
}
