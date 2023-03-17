using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellArea : MonoBehaviour
{
    public float Damage;
    public float Radius;
    public int DamageType;
    public LayerMask layer;
    void Start()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position,Radius,layer);
        foreach (var enemy in hitEnemies)
        {
            enemy.SendMessage("GetDamage",new Vector2(Damage, DamageType));
            if (DamageType == 2) enemy.SendMessage("Shock",0.2f);
        }
        Destroy(gameObject,2f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,Radius);
    }
}
