using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public float Damage;
    public Spell HealSpell;
    private GameObject[] enemies;
    private GameObject enemy;
    private Transform PlayerPos;
    private float HealAmount;
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 0) Destroy(gameObject);
        else
        {
            enemy = enemies[Random.Range(0, enemies.Length)];
            transform.position = new Vector2( enemy.transform.position.x+Random.Range(-0.5f,0.5f), enemy.transform.position.y + Random.Range(-0.5f, 0.5f));
            if (HealSpell.CurrentXp >= HealSpell.XPToUpgrade[1])
            {
                Damage *= 1.5f;
                if (HealSpell.CurrentXp >= HealSpell.XPToUpgrade[2])
                {
                    HealAmount = Damage * 2;
                }
                else {
                    HealAmount = Damage;
                }

            }
            else HealAmount = Damage;
            enemy.SendMessage("GetDamage",new Vector2(Damage,3));
        }
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerPos.position, Time.deltaTime*2);
        if (Vector3.Distance(transform.position, PlayerPos.position) < 1) {
            HeroComponent.instance.Heal(HealAmount);
            Destroy(gameObject);
        }


    }
    public void SetPlayer(Transform playerPos) {
        PlayerPos = playerPos;
    }
}
