using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject DeathEff;
    void Update()
    {

        if (GetComponent<EnemyScript>()._isDead) {
                    
    Instantiate(DeathEff, transform.position, Quaternion.identity);
    Destroy(gameObject); }
    }
}
