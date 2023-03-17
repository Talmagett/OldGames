using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    public float WindSpeed;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * WindSpeed;
    }

    private void Update()
    {
        transform.position = new Vector2(0,transform.position.y);
        if (gameObject.transform.position.y >= 3) Destroy(gameObject);
    }
}
