using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage;
    public float radius;
    public LayerMask layer;
    public GameObject EffectDestroy;
    public GameObject Rockets;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(20);
        for (int i = 0; i < 4; i++)
        {
            Instantiate(Rockets,transform.position,Quaternion.Euler(0,0,i*90));
        }
        Destroy(Instantiate(EffectDestroy, transform.position, Quaternion.identity), 3);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius,layer);
            foreach (var item in colliders)
            {
                if(item.CompareTag("Enemy"))
                item.gameObject.SendMessage("GetDamage", damage);
            }
            Destroy(Instantiate(EffectDestroy,transform.position,Quaternion.identity),3);
            Destroy(gameObject);
        }
    }
      
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
