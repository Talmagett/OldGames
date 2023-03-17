using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sceleton : MonoBehaviour
{
    public GameObject Grave;
    public GameObject DeathEff;
    public bool _respawn;

    void Update()
    {
        if (GetComponent<EnemyScript>()._isDead)
        {
            if(!_respawn)
            Instantiate(Grave, gameObject.transform.position, Quaternion.identity);
            Instantiate(DeathEff, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
