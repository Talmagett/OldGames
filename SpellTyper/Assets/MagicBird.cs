using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBird : MonoBehaviour
{
    public GameObject Arrow;
    public Transform CastPlace;
    IEnumerator Start()
    {
        int Cycles = 10;
        if (SpellsInstantiate.Spells.GetLvlOfSummon() >= (int)SpellsInstantiate.Spells.SummonMax.maxValue) {
            Cycles = 20;
        }
        
        float ZRot = transform.rotation.z;
        for (int i = 0; i < Cycles; i++) {
            Instantiate(Arrow, CastPlace.position, Quaternion.identity) ;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
