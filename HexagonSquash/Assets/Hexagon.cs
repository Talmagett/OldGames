using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    
    public float shrinksSpeed = 3f;
    void Start()
    {
        
        transform.localScale = Vector3.one * 15f;
    }

    
    void Update()
    {
        transform.localScale -= Vector3.one * shrinksSpeed * Time.deltaTime;
        if (transform.localScale.x <= 0.5f) {
            Destroy(gameObject);
        }
    }
}
