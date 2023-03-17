using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SpellCast : MonoBehaviour
{
    public static SpellCast instance { get; set; }

    public GameObject Castle;
    public GameObject HeroPlayer;
    public GameObject Staff;
    public Animator PlayerAnim;
    private float CastingTime;
    private void Awake()
    {
        instance = this;
    }
    float Angle;
    private void Update()
    {
        CastingTime -= Time.deltaTime;
        PlayerAnim.SetFloat("Cast", CastingTime);
    }
    public void CastSpell(Spell spellInfo)
    {
        if (spellInfo.CurrentXp < spellInfo.XPToUpgrade[spellInfo.XPToUpgrade.Length - 1])
        {
            spellInfo.CurrentXp++;
            for (int i = 0; i < spellInfo.XPToUpgrade.Length; i++)
            {
                if (spellInfo.CurrentXp == spellInfo.XPToUpgrade[i])
                    MainController.instance.AddMessage(spellInfo.name + " was upgraded");
                if (spellInfo.CurrentXp >= spellInfo.XPToUpgrade[spellInfo.XPToUpgrade.Length-1]) spellInfo.isMax = true;
            }
        }

        string SpellName = String.Concat(spellInfo.name.Where(c => !Char.IsWhiteSpace(c)));
        Angle = Staff.transform.eulerAngles.x;
        SendMessage(SpellName, spellInfo);
    }

    GameObject FlameObj;
    public Spell FlameSpell;
    void Flame(Spell spellInfo)
    {
        FlameObj = Instantiate(spellInfo.ObjToCreate, Staff.transform.position, Quaternion.Euler(0, 0, Angle), Staff.transform);
        if (FlameSpell.CurrentXp >= FlameSpell.XPToUpgrade[0])
        {
            var main = FlameObj.GetComponent<ParticleSystem>().main;
            main.startSpeed = 4;
            if (FlameSpell.CurrentXp >= FlameSpell.XPToUpgrade[1])
            {
                var emission = FlameObj.GetComponent<ParticleSystem>().emission;
                emission.rateOverTime = 8;
                if (FlameSpell.CurrentXp >= FlameSpell.XPToUpgrade[2])
                {
                    ParticleSystem.ColorOverLifetimeModule colorModule = FlameObj.GetComponent<ParticleSystem>().colorOverLifetime;
                    GradientColorKey[] gck = new GradientColorKey[2];
                    gck[0].color = new Color(0, 0.5f, 1);
                    gck[0].time = 0.0F;
                    gck[1].color = new Color(1, 0.5f, 0);
                    gck[1].time = 1.0F;
                    Gradient gradient = new Gradient();
                    gradient.SetKeys(gck, colorModule.color.gradient.alphaKeys);
                    colorModule.color = gradient;
                    FlameObj.GetComponent<Flame>().Damage *= 1.5f;
                }
            }
        }
        if (CastingTime < 6) CastingTime = 6;
    }


    GameObject WinterGustObj;
    public Spell WinterGustSpell;
    void WinterGust(Spell spellInfo)
    {
        WinterGustObj = Instantiate(spellInfo.ObjToCreate, Staff.transform.position, Quaternion.Euler(0, 0, Angle), Staff.transform);
        if (WinterGustSpell.CurrentXp >= WinterGustSpell.XPToUpgrade[0])
        {
            WinterGustObj.GetComponent<WinterGust>().Damage *= 1.5f;
            if (WinterGustSpell.CurrentXp >= WinterGustSpell.XPToUpgrade[1])
            {
                WinterGustObj.GetComponent<WinterGust>().isFreeze =true;
            }
        }
        if (CastingTime < WinterGustObj.GetComponent<WinterGust>().Delay) CastingTime = WinterGustObj.GetComponent<WinterGust>().Delay;
    }
    public Spell IceSpikeSpell;
    int MaxSpikes=20;
    void IceSpikes(Spell spellInfo) {
        if (IceSpikeSpell.CurrentXp >= IceSpikeSpell.XPToUpgrade[0])
        {
            MaxSpikes = 50;
        }
        if (CastingTime < 1) CastingTime = 1;
       StartCoroutine(IceSpikeGenerator(spellInfo));
    }

    public AudioClip IceSound;
    IEnumerator IceSpikeGenerator(Spell spellInfo)
    {
        float BasePos = Staff.transform.position.x;
        for (int i = 0; i < MaxSpikes; i++)
        {
            GameObject LastSpike = Instantiate(spellInfo.ObjToCreate, new Vector2(BasePos + i*0.5f,-1.2f), Quaternion.identity);

            if (IceSpikeSpell.CurrentXp >= IceSpikeSpell.XPToUpgrade[1])
            {
                LastSpike.GetComponent<IceSpike>().Damage *= 2f;
                LastSpike.GetComponent<SpriteRenderer>().color = new Color(0.4f,0.4f,1);
            }
            if (i % 4 == 0) MainController.instance.AudioPlayer0_2.PlayOneShot(IceSound);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.02f,0.07f));
        }        
    }

    void ColdBlast(Spell spellInfo)
    {
        WinterGustObj = Instantiate(spellInfo.ObjToCreate, Staff.transform.position, Quaternion.Euler(0, 90, 0), Staff.transform);
        if (CastingTime <3) CastingTime = 3;
    }
    void StormRage(Spell spellInfo)
    {
          Instantiate(spellInfo.ObjToCreate, spellInfo.ObjToCreate.transform.position, Quaternion.identity);
            if (CastingTime < 1) CastingTime = 1;
    }

    void ChainSpark(Spell spellInfo) {
        Instantiate(spellInfo.ObjToCreate,Staff.transform.position,Quaternion.identity,Staff.transform);
        if (CastingTime < 0.8f) CastingTime = 0.8f;
    }
    public Spell ElectricMineSpell;
    void ElectricMine(Spell spellInfo)
    {
        StartCoroutine( MineGenerator(spellInfo));
        if (CastingTime < 1) CastingTime = 1;
    }
    IEnumerator MineGenerator(Spell spellInfo) {
        int MissleCount = 1;
        float ImpulsePwr=100;
        bool ExtrDmg=false;
        if (ElectricMineSpell.CurrentXp >= ElectricMineSpell.XPToUpgrade[0])
        {
            MissleCount = 2;
            if (ElectricMineSpell.CurrentXp >= ElectricMineSpell.XPToUpgrade[2])
            {
                ExtrDmg = true;
                if (ElectricMineSpell.CurrentXp >= ElectricMineSpell.XPToUpgrade[2])
                {
                    MissleCount = 3;
                }
            }
        }
        for (int i = 0; i < MissleCount; i++)
        {
            GameObject LastMine = Instantiate(spellInfo.ObjToCreate, Staff.transform.position, Quaternion.Euler(0, 0, Angle));
            LastMine.GetComponent<Rigidbody>().AddForce(LastMine.transform.right* ImpulsePwr);
            if(ExtrDmg) LastMine.GetComponent<ElectricMine>().DmgMultCoeff = 4;
            yield return new WaitForSeconds(0.5f);
        }
    }
    void LightningOrb(Spell spellInfo)
    {
        Instantiate(spellInfo.ObjToCreate, new Vector2(Staff.transform.position.x, 3f), Quaternion.Euler(0, 0, Angle-90));
        if (CastingTime < 1) CastingTime = 1;
    }

    public Spell LaserBeamSpell;
    void LaserBeam(Spell spellInfo)
    {
        Instantiate(spellInfo.ObjToCreate, Staff.transform);
        if (CastingTime < 3.8f) CastingTime = 3.8f;
    }
    public Spell FireArrowsSpell;
    void FireArrows(Spell spellInfo)
    {
        StartCoroutine(FireArrowsGenerator(spellInfo));
    }

    IEnumerator FireArrowsGenerator(Spell spellInfo) {
        int ArrowsCount = 3;
        float DmgMult = 1.4f;
        if (FireArrowsSpell.CurrentXp >= FireArrowsSpell.XPToUpgrade[1])
        {
            ArrowsCount = 4;
            if (FireArrowsSpell.CurrentXp >= FireArrowsSpell.XPToUpgrade[3])
            {
                ArrowsCount = 5;
            }
        }
        if (CastingTime < ArrowsCount / 5) CastingTime = ArrowsCount / 5;
        for (int i = 0; i < ArrowsCount; i++)
        {
            GameObject LastFireArrow = Instantiate(spellInfo.ObjToCreate, new Vector2(Staff.transform.position.x + UnityEngine.Random.Range(0, 1f), Staff.transform.position.y + UnityEngine.Random.Range(-0.5f, 1)), Quaternion.Euler(0, 0, Angle - 90),HeroPlayer.transform);
            LastFireArrow.GetComponent<Animator>().speed = UnityEngine.Random.Range(0.5f, 1.5f);
            if (FireArrowsSpell.CurrentXp >= FireArrowsSpell.XPToUpgrade[0])
            {
                LastFireArrow.GetComponent<FireArrow>().Damage *= DmgMult;
                LastFireArrow.GetComponent<FireArrow>().Speed *= 1.5f;
                if (FireArrowsSpell.CurrentXp >= FireArrowsSpell.XPToUpgrade[2])
                {
                    DmgMult += 0.2f;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    void HotSphere(Spell spellInfo) {        
        Instantiate(spellInfo.ObjToCreate, Staff.transform.position, Quaternion.Euler(0, 0, Angle-90));
        if (CastingTime < 1) CastingTime = 1;
    }
    void WaterPrison(Spell spellInfo)
    {
        Instantiate(spellInfo.ObjToCreate, Staff.transform.position, Quaternion.Euler(0, 0, Angle - 90));
        if (CastingTime < 1) CastingTime = 1;
    }

    void Healing(Spell spellInfo) {
        Instantiate(spellInfo.ObjToCreate, Castle.transform.position, Quaternion.Euler(0, 0, 0), Castle.transform);
        if (CastingTime < 1) CastingTime = 1;
    }

    public Spell BloodDrainSpell;
    void BloodDrain(Spell spellInfo)
    {
        int BloodObjs = 3;
        if (BloodDrainSpell.CurrentXp >= BloodDrainSpell.XPToUpgrade[0])
        {
            BloodObjs = 5;
        }
        for (int i = 0; i < BloodObjs; i++)
        {
            Instantiate(spellInfo.ObjToCreate).SendMessage("SetPlayer", Castle.transform);           
        }
        if (CastingTime < 1) CastingTime = 1;
    }

    void Infection(Spell spellInfo) {
        Instantiate(spellInfo.ObjToCreate, Staff.transform.position, Quaternion.Euler(0, 0, Angle - 90));
        if (CastingTime < 1) CastingTime = 1;
    }
}
