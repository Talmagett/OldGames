using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovingPlat : MonoBehaviour
{
    public Transform[] points;
    public float Speed;
    private int _CurIndex;
    private void Start()
    {
        transform.position = points[_CurIndex].position;
    }
    private void Update()
    {
        Move();
    }

    void Move() {
        if (_CurIndex <= points.Length - 1) {
            transform.position = Vector2.MoveTowards(transform.position,points[_CurIndex].position,Speed*Time.deltaTime);
            if (Mathf.Approximately( transform.position.y ,points[_CurIndex].position.y)&& Mathf.Approximately(transform.position.x, points[_CurIndex].position.x)) _CurIndex++;

        }
        else { _CurIndex = 0; }
    }
}
