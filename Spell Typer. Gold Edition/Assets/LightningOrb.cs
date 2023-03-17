using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningOrb : MonoBehaviour
{
    public Spell LightningOrbSpell;
    public GameObject Lightning;
    public float Speed;
    public float AttackDelay;
    private float AttackDelayCounter;
    public float Damage;
    public float Radius;
    public LayerMask layer;

    public AudioClip[] hits;
    int maxTarget=1;
    private void Start()
    {
        if (LightningOrbSpell.CurrentXp >= LightningOrbSpell.XPToUpgrade[0])
        {
            maxTarget = 2; 
            if (LightningOrbSpell.CurrentXp >= LightningOrbSpell.XPToUpgrade[1])
            {
                AttackDelay = 0.4f;
                Damage *= 1.2f;
                if (LightningOrbSpell.CurrentXp >= LightningOrbSpell.XPToUpgrade[2])
                {
                    maxTarget = 4;
                }
            }
        }
    }
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * Speed;
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, Radius, layer);
        if (AttackDelayCounter < 0&& hitEnemies.Length>0)
        {
            MainController.instance.AudioPlayer0_5.PlayOneShot(hits[Random.Range(0, hits.Length)]);
            AttackDelayCounter = AttackDelay;
            for (int i = 0; i < (hitEnemies.Length> maxTarget? maxTarget: hitEnemies.Length); i++)
            {
                    GameObject target = hitEnemies[i].gameObject;
                    Transform lightningPos = Instantiate(Lightning, transform.position, Quaternion.identity).transform;
                    StartCoroutine(Attack(target, lightningPos));
            }
        }
        else {
            AttackDelayCounter -= Time.deltaTime;
        }
    }
    IEnumerator Attack(GameObject target,Transform lightning)
    {
        yield return new WaitForSeconds(0.05f);
        if (target)
        {
            lightning.position = target.transform.position;
            target.SendMessage("GetDamage", new Vector2(Damage, 2));
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
