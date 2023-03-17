using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy") Destroy(gameObject,1f);
        else Physics2D.IgnoreCollision(other.collider.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
    }
}
