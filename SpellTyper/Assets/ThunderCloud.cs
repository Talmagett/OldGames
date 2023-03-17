using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCloud : MonoBehaviour
{
    public GameObject LightningObj;
    public Transform CreatePlace;
    void Start()
    {
        StartCoroutine(LightningFall());
        Destroy(gameObject,3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator LightningFall()
    {
        float LightningDelay = 1;
        if (SpellsInstantiate.Spells.GetLvlOfLightning() >= SpellsInstantiate.Spells.LightningMax.maxValue)LightningDelay = 0.4f; 
        else if (SpellsInstantiate.Spells.GetLvlOfLightning() >= 20) LightningDelay = 0.45f;
        else if (SpellsInstantiate.Spells.GetLvlOfLightning() >= 16) LightningDelay = 0.5f;
        else if (SpellsInstantiate.Spells.GetLvlOfLightning() >= 12) LightningDelay = 0.6f;
        else if (SpellsInstantiate.Spells.GetLvlOfLightning() >= 8) LightningDelay = 0.8f;
        else LightningDelay = 1;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(LightningDelay);
            Instantiate(LightningObj, CreatePlace.position,Quaternion.identity);
        }
    }
}
