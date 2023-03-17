using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSpawnChıld : MonoBehaviour
{
    public GameObject GolemChild;
    public float SpawnCD;
    public GameObject DeathEff;
    
    IEnumerator Start()
    {
        while (true)
        {
            Instantiate(GolemChild, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnCD);
        }
    }
    private void Update()
    {
        if (GetComponent<EnemyScript>()._isDead)
        {
            Instantiate(DeathEff, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
