using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGame : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
