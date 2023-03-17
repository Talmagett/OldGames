using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HurricaneScript : MonoBehaviour
{
    public ParticleSystem part;
    public GameObject ObjDelay;
    public bool StartToDestroy;

    private float Delay=0;
    private bool isMax;
    void Start()
    {
        Animator animThis = GetComponent<Animator>();
        if (SpellsInstantiate.Spells.GetLvlOfHurricane() >= SpellsInstantiate.Spells.WindBlowMax.maxValue) { animThis.speed = 0.5f; isMax = true; }
        else if (SpellsInstantiate.Spells.GetLvlOfHurricane() >= 20) animThis.speed = 0.6f;
        else if (SpellsInstantiate.Spells.GetLvlOfHurricane() >= 16) animThis.speed = 0.7f;
        else if (SpellsInstantiate.Spells.GetLvlOfHurricane() >= 12) animThis.speed = 0.8f;
        else if (SpellsInstantiate.Spells.GetLvlOfHurricane() >= 8) animThis.speed = 0.9f;
        else animThis.speed = 1;
    }

    private void Update()
    {
        if (StartToDestroy && !isMax)
        {
            StartCoroutine(Destroy());
        }
        else if (isMax && StartToDestroy) {
            Delay = 3f;
            StartCoroutine(Destroy());
        }
    }

    public IEnumerator Destroy() {
        yield return new WaitForSeconds(Delay);
        part.Stop();
        ObjDelay.SetActive(false);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        
    }
}

    

