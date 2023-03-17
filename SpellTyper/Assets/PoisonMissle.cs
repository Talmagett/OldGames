using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMissle : MonoBehaviour
{
    public float PoisonMissleSpeed;
    public GameObject PoisonCloud;
    private Rigidbody2D _objRb;
    void Start()
    {
        _objRb = GetComponent<Rigidbody2D>();
        _objRb.velocity = Vector2.right * PoisonMissleSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(PoisonCloud, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
