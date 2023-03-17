using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdBlast : MonoBehaviour
{
    public float Damage;
    public float Radius;
    public LayerMask layer;
    public GameObject FrozenEff;
    public Spell ColdBlastSpell;
    float freezeTime=1;
    bool isSlow;
    private void Start()
    {
        if (ColdBlastSpell.CurrentXp >= ColdBlastSpell.XPToUpgrade[0])
        {
            freezeTime *= 2;
            if (ColdBlastSpell.CurrentXp >= ColdBlastSpell.XPToUpgrade[1])
            {
                Damage *= 2;
                if (ColdBlastSpell.CurrentXp >= ColdBlastSpell.XPToUpgrade[2])
                {
                    isSlow = true;
                }
            }
        }
    }
    public void Explode()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, Radius, layer);
        foreach (var enemy in hitEnemies)
        {
            enemy.SendMessage("GetDamage", new Vector2(Damage, 1));
            enemy.SendMessage("Freeze", freezeTime);
            if (isSlow) enemy.SendMessage("Slow", freezeTime*2);
            Instantiate(FrozenEff,enemy.transform.position,Quaternion.identity);
        }
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 1f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
