using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tempest : MonoBehaviour
{
    public GameObject Lightning;
    public float Damage;
    public float LightningDamage;
    public Vector3 Area;
    public Spell TempestSpell;
    public AudioClip[] hits;
    public LayerMask layerEnemy;
    ParticleSystem part;
    public float AttackTime=0.5f;
    float timeCounter;
    bool die;
    IEnumerator Start()
    {

        part = GetComponent<ParticleSystem>();
        if (TempestSpell.CurrentXp >= TempestSpell.XPToUpgrade[0])
        {
            Damage *= 1.2f;
            LightningDamage *= 1.5f;
            if (TempestSpell.CurrentXp >= TempestSpell.XPToUpgrade[1])
            {
                AttackTime /=1.5f;
                if (TempestSpell.CurrentXp >= TempestSpell.XPToUpgrade[2])
                {
                    var emission = part.emission;
                    emission.rateOverTime = 25;
                }
            }
        }

        yield return new WaitForSeconds(6f);
        part.Stop();
        die = true;

        Destroy(gameObject,3);
    }

    private void Update()
    {
        if(die) GetComponent<AudioSource>().volume -=Time.deltaTime;

        if (timeCounter < 0)
        {
            Collider[] hitEnemies = Physics.OverlapBox(transform.position, Area, Quaternion.identity,layerEnemy);
            if (hitEnemies.Length > 0)
            {
                GameObject target = hitEnemies[Random.Range(0, hitEnemies.Length)].gameObject;
                Transform lightningPos = Instantiate(Lightning, new Vector2(target.transform.position.x, target.transform.position.y + 10), Quaternion.identity).transform;
                StartCoroutine(Attack(target, lightningPos));
                timeCounter = AttackTime;
            }
        }
        else timeCounter -= Time.deltaTime;


    }
   

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SendMessage("GetDamage", new Vector2(Damage, 1));
            other.gameObject.SendMessage("Slow", 1);
        }
    }
    IEnumerator Attack(GameObject target, Transform lightning)
    {
        yield return new WaitForSeconds(0.05f);
        if (target)
        {
            lightning.position = target.transform.position;
            MainController.instance.AudioPlayer0_5.PlayOneShot(hits[Random.Range(0, hits.Length)]);
            target.SendMessage("Shock");
            target.SendMessage("GetDamage", new Vector2(LightningDamage, 2));

            Destroy(lightning.gameObject, 1f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, Area);
    }
}
