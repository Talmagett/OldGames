using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Damage;
    public float Speed;
    int Element;
    public GameObject[] Effects;
    public Gradient[] gradients;
    [SerializeField] TrailRenderer line;
    void Start()
    {
        Element = Random.Range(0, 4);
        Effects[Element].SetActive(true);
        line.colorGradient = gradients[Element];
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * Speed;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("GetDamage", new Vector2(Damage, Element));
            Destroy(gameObject);
        }
    }
    public void SetDmg(float mult) {
        Damage*= mult;
    }
}
