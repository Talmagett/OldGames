using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float ArrowObjSpeed;
    public int DamageAmount;
    public GameObject DeathEff;
    private Rigidbody2D _objRb;
    void Start()
    {
        _objRb = GetComponent<Rigidbody2D>();
        _objRb.velocity = -Vector2.right * ArrowObjSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<MageScript>())other.gameObject.GetComponent<MageScript>().TakeDamage(DamageAmount);
            if(other.gameObject.GetComponent<Shield>()) other.gameObject.GetComponent<Shield>().TakeDamage(DamageAmount);
            Instantiate(DeathEff,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
