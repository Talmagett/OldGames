using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGroundWallTime : MonoBehaviour
{
    public float DelayTime;
    public float DelayDeathTime;

    public float Damage;

    private List<GameObject> Enemies=new List<GameObject>();

    IEnumerator Start()
    {
        StartCoroutine(Death());
        while (true)
        {
            if (Enemies.Count > 0)
            {
                foreach (GameObject enemy in Enemies.ToArray())
                {
                    if (enemy != null)
                    enemy.SendMessage("GetDamage",new Vector2(Damage,0));
                    else Enemies.Remove(enemy);
                }
            }
            yield return new WaitForSeconds(DelayTime);
        }
    }

    IEnumerator Death() {
        yield return new WaitForSeconds(DelayDeathTime);
        GetComponent<ParticleSystem>().Stop();
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            Enemies.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            Enemies.Remove(collision.gameObject);
        }
    }
}
