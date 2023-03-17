using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp( transform.position.y + Input.GetAxis("Vertical"), -2.2f, 0));
    }
}
