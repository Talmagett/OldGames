using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portator : MonoBehaviour
{
    public Transform LeftTran;
    public Transform RightTran;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Train"))
        {
            string check = collision.GetComponent<Train>().Identificator;
            if (check=="red"||check=="yellow") collision.transform.position = RightTran.position;
            else collision.transform.position = LeftTran.position;
        }
    }
}
