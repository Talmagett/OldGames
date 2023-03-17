using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimActivate : MonoBehaviour
{
    private Animator BtnAnimator;
    private void Start()
    {
        BtnAnimator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Ball") {
            if (gameObject.name == "Red"|| gameObject.name == "Blue") {
                BtnAnimator.Play("ChangeColor");
            }
            else
            BtnAnimator.Play("ButtonAnim");
        }
    }
}
