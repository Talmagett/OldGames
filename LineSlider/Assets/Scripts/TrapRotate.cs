using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRotate : MonoBehaviour
{
    public float RotatingSpeed;
    public Vector3 Direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotatingSpeed*Time.deltaTime* Direction);
    }
}
