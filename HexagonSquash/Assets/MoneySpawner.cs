using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    private float Timer;
    public GameObject Objs;
    IEnumerator Start()
    {
        while (true)
        {
            int Timer = Random.Range(5, 10);
            yield return new WaitForSeconds(Timer);
            Instantiate(Objs, Vector3.zero, Quaternion.identity);
        }
    }
}
