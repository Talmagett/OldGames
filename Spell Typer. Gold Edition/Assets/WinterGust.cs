using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterGust : MonoBehaviour
{
    public float Delay;
    public float Damage;
    public GameObject FrozenEff;
    public bool isFreeze;
    IEnumerator Start()
    {
        StartCoroutine(DamageToUnits());
        yield return new WaitForSeconds(Delay);
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject,2f);
    }
    IEnumerator DamageToUnits()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            foreach (var enemy in Enemies)
            {
                if (enemy == null) continue;
                enemy.GetComponent<CreatureProp>().Slow(2);
                if(isFreeze) enemy.GetComponent<CreatureProp>().Freeze(0.02f);
                enemy.SendMessage("GetDamage", new Vector2(Damage, 1));
            }
            
        }
    }
    List<GameObject> Enemies = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Enemies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemies.Remove(other.gameObject);
        }
    }
}
