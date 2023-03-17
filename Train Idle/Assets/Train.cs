using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public float Speed;
    public string Identificator;
    public GameObject DeathEff;
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * Speed;
    }
    public void Destroy()
    {
        Destroy( Instantiate(DeathEff,transform.position,Quaternion.identity),2f);
        Destroy(gameObject);
    }
    public void SetSpeed(float value) {
        Speed = value;
    }
}
