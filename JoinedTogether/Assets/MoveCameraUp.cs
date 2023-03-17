using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveCameraUp : MonoBehaviour
{
    public void MoveUp(float moveUp) {
        transform.DOMoveY(moveUp,2);
    }
    public void MoveDown(float moveUp) {
        transform.DOMoveY(moveUp,10);
    }
}
