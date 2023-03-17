using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnWheel : MonoBehaviour
{
    public float rotatespeed = 10f;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
                    transform.Rotate(Vector3.back, rotatespeed * Time.deltaTime);
        }
    }
}    
