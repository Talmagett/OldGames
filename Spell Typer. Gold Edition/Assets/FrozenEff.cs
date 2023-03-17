using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenEff : MonoBehaviour
{
    private CreatureProp enemy;
    void Start()
    {
        enemy=GetComponentInParent<CreatureProp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.FrozenCounter <= 0) { Destroy(gameObject); }
    }
}
