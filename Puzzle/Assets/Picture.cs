using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    //[HideInInspector]
    public Vector2 BasicPos;

    private void OnMouseDown()
    {
        Vector2 temp = EmptyControl.Instance.transform.position;
        EmptyControl.Instance.transform.position = transform.position;
        transform.position = temp;
        if ((Vector2)transform.position == BasicPos) {
            EmptyControl.Instance.CheckPuzzle();
        }
    }
}
