using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    void Start()
    {
        float rand = Random.Range(0f,360f);
        transform.position = new Vector2(1.8f*Mathf.Sin(rand), 1.8f * Mathf.Cos(rand));
    }

}
