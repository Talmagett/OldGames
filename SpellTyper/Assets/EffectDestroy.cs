using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    public float DelayTime;
    void Start()
    {
        Destroy(gameObject,DelayTime);
    }
}
