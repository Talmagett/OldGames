using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orber : MonoBehaviour
{
    public float Radius;
    public LayerMask layer;

    IEnumerator Start()
    {
        while (true)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, Radius, layer);
            foreach (var enemy in hitEnemies)
            {
                enemy.SendMessage("Melt");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
