using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorGround : MonoBehaviour
{
    [SerializeField] private HeroMove heroMove;
    private void OnCollisionEnter2D(Collision2D other)
    {
        heroMove.isGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        heroMove.isGrounded = false;
    }
}
