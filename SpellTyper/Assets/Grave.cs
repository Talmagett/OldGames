using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public GameObject Sceleton;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        GameObject newScelet= Instantiate(Sceleton,transform.position,Quaternion.identity);
        newScelet.GetComponent<Sceleton>()._respawn = true;
        Destroy(gameObject);
    }

}
