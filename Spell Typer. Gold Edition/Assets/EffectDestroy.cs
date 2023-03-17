using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    public float Delay;
    void Start()
    {
        Destroy(gameObject,Delay);
    }

}
