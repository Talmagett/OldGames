using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public float Timer = 1;
    public GameObject[] Objs;
    IEnumerator Start()
    {
        while (true)
        {
            int rand = Random.Range(0, Objs.Length);
            GameObject newObj= Instantiate(Objs[rand], Vector3.zero, Quaternion.identity);
            newObj.transform.rotation = Quaternion.Euler(0f,0f,Random.Range(0f,360f));
            yield return new WaitForSeconds(Timer);
        }
    }
}
