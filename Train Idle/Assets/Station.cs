using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public string Identificator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Train")) {
            if (collision.GetComponent<Train>().Identificator != Identificator)
            {
                Spawn.Instanse.LoseLive();
            }
            collision.gameObject.SendMessage("Destroy");
        }
    }
}
