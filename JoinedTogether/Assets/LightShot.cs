using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShot : MonoBehaviour
{
    public float Speed;
    private Torch parentCreator;
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * Speed;
    }
    public void SetParent(Torch parent) {
        parentCreator = parent;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Torch"))
        {
            if (collision.gameObject != parentCreator.gameObject && !collision.GetComponent<Torch>().Activated)
            {
                collision.GetComponent<Torch>().Activated = true;
                collision.GetComponent<Torch>().CreateLink(parentCreator);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Buildings")) {
            Destroy(gameObject);
        }
    }
}
