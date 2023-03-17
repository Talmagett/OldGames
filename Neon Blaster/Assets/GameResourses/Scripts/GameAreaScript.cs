using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaScript : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer!=(5)) Destroy(other.gameObject);
    }
}
