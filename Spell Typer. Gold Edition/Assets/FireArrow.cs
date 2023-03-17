using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    public float Speed;
    public float Damage;
    public GameObject DeathEff;
    private void Start()
    {
        transform.parent = null;
    }
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * Speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.SendMessage("GetDamage",new Vector2(Damage,0));
            Instantiate(DeathEff,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
