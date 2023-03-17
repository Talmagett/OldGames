using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyScript : MonoBehaviour
{
    public float Delay;
    void Start()
    {
        Invoke("Destroy", Delay);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
