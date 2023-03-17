using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMother : MonoBehaviour
{
    public GameObject SpiderSon;
    public GameObject SpiderDeadEff;
    public int SpiderCount;
    public GameObject DeathEff;
    
    void Update()
    {
        if (GetComponent<EnemyScript>()._isDead) {
            for (int i = 0; i < SpiderCount; i++)
            {
                Instantiate(SpiderSon,new Vector2( transform.position.x+i*2,transform.position.y),Quaternion.identity);
            }
            Instantiate(SpiderDeadEff,transform.position,Quaternion.identity);
            Instantiate(DeathEff, transform.position, Quaternion.identity);
            Destroy(gameObject); }
    }
}
