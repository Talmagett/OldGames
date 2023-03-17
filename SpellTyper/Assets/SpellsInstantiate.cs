using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsInstantiate : MonoBehaviour
{
    public static SpellsInstantiate Spells { get; set; }
    public bool LastWordCreated;
    public GameObject Mage;
    public GameObject FireBallObj;
    public GameObject IceBlastObj;
    public GameObject LightningObj;
    public GameObject LightningOnObj;
    public GameObject WindBlowObj;
    public GameObject PoisonCloudMissleObj;
    public GameObject MaledictionObj;
    public GameObject HealingObj;
    public GameObject ShieldObj;
    public GameObject SummonObj;
    public GameObject LaserBeamObj;
    [Space]

    public Slider FireBallMax;
    public Slider IceBlastMax;
    public Slider LightningMax;
    public Slider WindBlowMax;
    public Slider PoisonCloudMax;
    public Slider MaledictionMax;
    public Slider HealingMax;
    public Slider ShieldMax;
    public Slider SummonMax;
    public Slider LaserBeamMax;
    [Space]
    public AudioClip MaledictionClip;
    public LightShotAttack LightSystem;
    private int SpellsCount=0;
    private bool _isLightningMax;
    private bool ShieldCD; 
    private void Awake()
    {
        Spells = this;
    }

    public void FireBallSpell(Transform Casterpos)
    {
        StartCoroutine( FireBallCreation(Casterpos));
        SpellsCount++;
        LightSystem.IncreaserLightDamage();
        if (!LastWordCreated) FireBallMax.value++;
    }

    IEnumerator FireBallCreation(Transform Casterpos) {
        int fireCount = 1;
        float ChanceToDouble=0;
        if((int)FireBallMax.value>= 20) ChanceToDouble = 40;
        else if ((int)FireBallMax.value >= 16) ChanceToDouble = 30;
        else if ((int)FireBallMax.value >= 12) ChanceToDouble = 20;
        else if ((int)FireBallMax.value >= 8) ChanceToDouble = 10;
        else ChanceToDouble = 0;
        if (Random.Range(0, 100) <= ChanceToDouble) fireCount = 2;
        for (int i = 0; i < fireCount; i++)
        {
            Instantiate(FireBallObj, Casterpos.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }


    public void IceBlastSpell(Transform Casterpos)
    {
        if (!LastWordCreated) IceBlastMax.value++;
        Instantiate(IceBlastObj, Casterpos.position, Quaternion.identity);
        SpellsCount++;
        LightSystem.IncreaserLightDamage();
    }
    public void LightningSpell(Transform Casterpos)
    {
        if (!LastWordCreated) LightningMax.value++;
        if (LightningMax.value >= LightningMax.maxValue&& !_isLightningMax) {
            _isLightningMax = true;
            StartCoroutine(LightningStorm());
        }
        Instantiate(LightningObj, Vector2.zero, Quaternion.identity);
        SpellsCount++;
        LightSystem.IncreaserLightDamage();
    }

    IEnumerator LightningStorm() {

        while (_isLightningMax) {
            Instantiate(LightningOnObj, new Vector2(0,30), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }

    public void WindBlow()
    {
        if (!LastWordCreated) WindBlowMax.value++;
        Instantiate(WindBlowObj, Vector2.zero, Quaternion.identity);
        SpellsCount++;
        LightSystem.IncreaserLightDamage();
    }
    IEnumerator WindBlowStop() {
        float windBlow = 1f;
        if ((int)WindBlowMax.value >= WindBlowMax.maxValue) windBlow = 3;
        else if ((int)WindBlowMax.value >= 20) windBlow = 3;
        else if ((int)WindBlowMax.value >= 16) windBlow = 2.5f;
        else if ((int)WindBlowMax.value >= 12) windBlow = 2;
        else if ((int)WindBlowMax.value >= 8) windBlow = 1.5f;
        else windBlow = 1;
        yield return new WaitForSeconds(windBlow);

        WindBlowObj.SetActive(false);
    }
    public void PoisonCloudMissle(Transform Casterpos) {
        
            if (!LastWordCreated) PoisonCloudMax.value++;
            Instantiate(PoisonCloudMissleObj, Casterpos.position, Quaternion.identity);
        SpellsCount++;
        LightSystem.IncreaserLightDamage();
    }

    
    public void Malediction() {
        GameObject.Find("Audio").GetComponent<AudioSource>().PlayOneShot(MaledictionClip);
        SpellsCount++;
        LightSystem.IncreaserLightDamage();
        if (!LastWordCreated) MaledictionMax.value++;
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in Enemies)
        {
                enemy.GetComponent<EnemyScript>().isWeak = 11;
                GameObject MaledictEff = Instantiate(MaledictionObj, enemy.transform.position, Quaternion.identity);
                MaledictEff.transform.parent = enemy.transform;
                if (MaledictionMax.value >= MaledictionMax.maxValue) {
                    MaledictEff.GetComponent<EffectDestroy>().enabled = false;
                }
            
        }
    }

    public void Healing(Transform Casterpos) {
        if (!LastWordCreated&& Mage.GetComponent<MageScript>().Health.value< Mage.GetComponent<MageScript>().Health.maxValue)
        { HealingMax.value++;
            int _HealthMax;
            if ((int)HealingMax.value >= HealingMax.maxValue) _HealthMax = 300;
            else if ((int)HealingMax.value >= 20) _HealthMax = 300;
            else if ((int)HealingMax.value >= 16) _HealthMax = 250;
            else if ((int)HealingMax.value >= 12) _HealthMax = 200;
            else if ((int)HealingMax.value >= 8) _HealthMax = 150;
            else _HealthMax = 100;
            MageScript.Mage.Health.maxValue = _HealthMax;
        }
        SpellsCount++;
        LightSystem.IncreaserLightDamage();
        Instantiate(HealingObj, Casterpos.position, Quaternion.identity);
        int _healingAmount;
        if ((int)HealingMax.value >= 20) _healingAmount = 30;
        else if ((int)HealingMax.value >= 16) _healingAmount = 25;
        else if ((int)HealingMax.value >= 12) _healingAmount = 20;
        else if ((int)HealingMax.value >= 8) _healingAmount = 15;
        else _healingAmount = 10;
        MageScript.Mage.TakeDamage(-_healingAmount);
    }
    public void Shield( )
    {
        if (ShieldObj.GetComponent<Shield>().Health != ShieldObj.GetComponent<Shield>().MaxHealth)
        {
            SpellsCount++;
            if (!LastWordCreated) ShieldMax.value++;
            ShieldObj.GetComponent<Shield>().Health = ShieldObj.GetComponent<Shield>().MaxHealth;
            ShieldObj.GetComponent<Shield>().CheckLVL();
            LightSystem.IncreaserLightDamage();
        }
    }
    public void Summon(Transform Casterpos)
    {
        if (!LastWordCreated) SummonMax.value++;
        Instantiate(SummonObj, Casterpos.position, Quaternion.identity);
        SpellsCount++;
        LightSystem.IncreaserLightDamage();
    }
    public void LaserBeam(Transform Casterpos)
    {
        if (!LastWordCreated) LaserBeamMax.value++;
        Instantiate(LaserBeamObj,LaserBeamObj.transform.position,Quaternion.identity);
        SpellsCount++;
        LightSystem.IncreaserLightDamage();
    }
    public int GetSpellCount() {
        return SpellsCount;
    }

    public int GetLvlOfFireBall() {
        return (int)FireBallMax.value;
    }

    public int GetLvlOfIceBlast()
    {
        return (int)IceBlastMax.value;
    }
    
    public int GetLvlOfLightning()
    {
        return (int)LightningMax.value;
    }
    public int GetLvlOfHurricane()
    {
        return (int)WindBlowMax.value;
    }
    public int GetLvlOfPoisonCloud() {
        return (int)PoisonCloudMax.value;
    }
    public int GetLvlOfMalediction()
    {
        return (int)MaledictionMax.value;
    }
    public int GetLvlOfShield()
    {
        return (int)ShieldMax.value;
    }
    public int GetLvlOfSummon() {
        return (int)SummonMax.value;
    }
    public int GetLvlOfHealing() {
        return (int)HealingMax.value;
    }
    public int GetLvlOfLaserBeam() {
        return (int)LaserBeamMax.value;
    }

}
