using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdEffect : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<EnemyScript>().FrozenCounter < 0) Destroy(gameObject);
    }
}
