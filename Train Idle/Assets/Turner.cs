using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Train"))
        {
            collision.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
        }
    }
}
