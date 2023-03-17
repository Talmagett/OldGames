using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShotAttack : MonoBehaviour
{
    public Animator Mage;
    public bool ReadyToAttack;
    public int Damage;
    public GameObject LightMissle;
    private int DamageToShare;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaserLightDamage() {
        DamageToShare += Damage;
    }

    public void LightAttack()
    {
        GameObject LightMis= Instantiate(LightMissle, transform.position, Quaternion.identity);
        LightMis.GetComponent<LightMissle>().DamageIncreased = DamageToShare;
        GetComponent<Animator>().SetTrigger("Attacked");
        DamageToShare = 0;
        ReadyToAttack = false;
    }
}
