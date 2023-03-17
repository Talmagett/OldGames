using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissle : MonoBehaviour
{
    public float Speed;
    public float Damage;

    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * Speed;
    }

}
