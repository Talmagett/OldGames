using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public float Damage;
    public ParticleSystem part;

    IEnumerator Start()
    {
        part = GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(6);
        GetComponentInChildren<ParticleSystem>().Stop();
        Destroy(gameObject, 2f);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SendMessage("GetDamage", new Vector2( Damage, 0));
        }
    }
}
