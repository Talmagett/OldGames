using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
    private GameObject HeroObject;
    public float HeroSpeed;
    private Rigidbody2D HeroRigidbody;
    public float xMin;
    public float xMax;
    public Transform[] Guns;
    public GameObject SideGuns;
    public float ShotDelay;
    private float nextShot;
    public GameObject missle;
    private GameControllerScript GameControllScript;
    private MainMenu mainMenuScript;
    public int Coeff;
    private bool[] GunActive;
    public GameObject Particle;
    public GameObject ReviveParticle;
    private int GunsLength;
    public GameObject LaserGunObject;
    private bool isReviveUsed=false;
    public AudioClip DeathSound;
    [Space]
    [Tooltip("Wind")]
    public GameObject Wind;
    public int CooldownWindBlow;
    public float CooldownWindBlowCounter;

    [Space]
    [Tooltip("SlowTime")]
    public int CooldownSlowTime;
    public int DurationSlowTime;
    public float SlowTimeCoef;
    public float CooldownSlowTimeCounter;
    private float DurationSlowTimeCounter;
    public AudioClip SlowTimeAudio;
    public GameObject TimeSlowEffect;

    [Space]
    [Tooltip("LaserBeam")]
    public GameObject Beam;
    public GameObject BeamMissle;
    public int CooldownBeam;
    public float CooldownBeamCounter;
    public float DurationLaser;
    private float DurationLaserCounter;
    private MissleScript missleScript;
    public GameObject LaserExpl;

    //Revive
    public GameObject ReviveEffect;
    public GameObject Invulnirable;
    //Smaller
    private float SmallerCooldownMax=5f;
    private float SmallerCooldownCounter=-1;
    public GameObject ScaleEffect;
    float Multiply=1f;
    //Multiply
    public int MultiplyToEnemy=1;
    private float MultiplyCooldownCounter;
    private float MultiplyCooldownMax = 5f;
    public GameObject x2;
    //Frozen
    public bool Frozen = false;
    public float IceBlastCooldownCounter;
    private float IceBlastCooldownMax = 3f;
    public GameObject IceEffect;
    //Lightning
    public GameObject LightningObject;
    void Start()
    {
        GunsLength = Guns.Length;
        GunActive = new bool[GunsLength];
        mainMenuScript = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        HeroRigidbody = GetComponent<Rigidbody2D>();
        GameControllScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        HeroSpeed = 7 + mainMenuScript.LvlShipSpeed;
        HeroObject = gameObject;
    }

    void FixedUpdate()
    {
        if (SmallerCooldownCounter >= 0)
        {
            SmallerCooldownCounter -= Time.deltaTime;

            if (transform.localScale.x > 0.5f && transform.localScale.y > 0.5f)
                Multiply -= Time.deltaTime;

            HeroObject.transform.localScale = new Vector2(Multiply, Multiply);
        }
        else
        {
            if (transform.localScale.x < 1f && transform.localScale.y < 1f)
            {
                Multiply += Time.deltaTime;
                HeroObject.transform.localScale = new Vector2(Multiply, Multiply);
            }
        }

        if (MultiplyCooldownCounter >= 0) MultiplyCooldownCounter -= Time.deltaTime;
        else
        {
            if (MultiplyToEnemy == 2)
                MultiplyToEnemy = 1;
        }


        ShotDelay = (1f - ((mainMenuScript.LvlShotDelay) * 0.01f)) * 0.1f;
        if (mainMenuScript.LvlAddGun <= 1)
        {
            for (int j = 0; j < GunsLength; j++)
            {
                if (j == 0) {
                    GunActive[j] = true;
                    continue; }
                GunActive[j] = false;
            }
        }
        else if (mainMenuScript.LvlAddGun == 2)
        {
            for (int j = 0; j < GunsLength; j++)
            {
                if (j == 1 || j == 2) { GunActive[j] = true; continue; }
                GunActive[j] = false;
            }
        }
        else if (mainMenuScript.LvlAddGun == 3)
        {
            for (int j = 0; j < GunsLength; j++)
            {
                if (j <= 2) { GunActive[j] = true; continue; }
                GunActive[j] = false;
            }
        }
        else if (mainMenuScript.LvlAddGun == 4)
        {
            for (int j = 0; j < GunsLength; j++)
            {
                if (j <= 4 && j != 0) { GunActive[j] = true; continue; }
                GunActive[j] = false;
            }
        }
        else if (mainMenuScript.LvlAddGun == 5)
        {
            for (int j = 0; j < GunsLength; j++)
            {
                if (j <= 4) { GunActive[j] = true; continue; }
                GunActive[j] = false;
            }
        }
        else if (mainMenuScript.LvlAddGun > 5)
        {
            for (int j = 0; j < GunsLength; j++)
            {
                GunActive[j] = true;
            }
        }
        if (Guns[GunsLength - 1].gameObject.activeSelf == false) SideGuns.SetActive(false);
        else SideGuns.SetActive(true);

        for (int i = 0; i < Guns.Length; i++) Guns[i].gameObject.SetActive(GunActive[i]);
        if(LaserGunObject.activeSelf==false&&mainMenuScript.LvlLaserGun>0) LaserGunObject.SetActive(true);
        else if(mainMenuScript.LvlLaserGun <= 0) LaserGunObject.SetActive(false);
        if (GameControllScript.isPlaying)
        {
            float Xmoving = Input.GetAxis("Horizontal");
            HeroRigidbody.velocity = new Vector2(Xmoving, 0) * HeroSpeed;

            float Xposition = Mathf.Clamp(HeroObject.transform.position.x, xMin, xMax);
            HeroObject.transform.position = new Vector2(Xposition, HeroObject.transform.position.y);

            if (Time.time > nextShot)
            {
                nextShot = Time.time + ShotDelay;
                
                    if (DurationLaserCounter > 0)
                    {
                    Beam.SetActive(true);
                    for (int s = 0; s < 5; s++)
                    {
                        Instantiate(BeamMissle, new Vector2(Guns[s].Find("MisslePos").position.x, Guns[s].Find("MisslePos").position.y-1), Quaternion.Euler(0, 0, Guns[s].eulerAngles.z));
                        DurationLaserCounter -= Time.deltaTime;
                    }
                    }
                    else {
                    Beam.SetActive(false);
                    foreach (Transform gunsposition in Guns)
                    {
                        if (gunsposition.gameObject.activeSelf == true)
                        {
                            Instantiate(missle, gunsposition.Find("MisslePos").position, Quaternion.Euler(0, 0, gunsposition.eulerAngles.z));
                        }
                    }
                }
                
            }
            if (mainMenuScript.WindBlowBuy) {
                CooldownWindBlowCounter -= Time.deltaTime;
                if (CooldownWindBlowCounter <= 0)
                {
                    CooldownWindBlowCounter = 0;
                    if (Input.GetKey("e")) WindBlow();
                }
            }

            if (mainMenuScript.SlowTimeBuy) {
                CooldownSlowTimeCounter -= Time.deltaTime;
                if (CooldownSlowTimeCounter <= 0)
                {
                    CooldownSlowTimeCounter = 0;
                    if (Input.GetKey("f")) TimeSlow();
                }

                DurationSlowTimeCounter -= Time.deltaTime;
                if (DurationSlowTimeCounter <= 1 && DurationSlowTimeCounter > 0)
                    {
                    if (Time.timeScale < 1f) Time.timeScale = SlowTimeCoef + (0.5f-DurationSlowTimeCounter/2);
                }
                else if (DurationSlowTimeCounter <= 0)
                {
                    DurationSlowTimeCounter = 0; Time.timeScale = 1f;
                }
            }

            if (mainMenuScript.LaserBeamBuy)
            {
                CooldownBeamCounter -= Time.deltaTime;
                if (CooldownBeamCounter <= 0)
                {
                    CooldownBeamCounter = 0;
                    if (Input.GetKey("r")) LaserBeamRay();
                }
                
            }
        }
        if (Frozen)
        {
            if (IceBlastCooldownCounter >= 0) { IceBlastCooldownCounter -= Time.deltaTime;  }
            else
            {
                GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in Enemies)
                {
                    enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                }
                Frozen = false;
            }
        }
    }

        public void OnCollisionEnter2D(Collision2D other)
        {
        if (other.gameObject.tag == "Enemy")
        {
            if (mainMenuScript.ReviveBuy && !isReviveUsed)
            {
                Invulnirable.SetActive(true);
                isReviveUsed = true;
                Instantiate(ReviveEffect, HeroObject.transform.position, Quaternion.identity);
                Instantiate(Wind, HeroObject.transform.position, Quaternion.identity);
                GameObject NewReviveParticle = Instantiate(ReviveParticle, transform.position, Quaternion.identity);
                NewReviveParticle.transform.parent = null;
                Invoke("ReviveBack", 7f);
            }
            else {
                    if (Invulnirable!=null&& Invulnirable.activeSelf == true) { }
                    else
                    {
                        Time.timeScale = 0.5f;
                        GameControllScript.PlaySound(DeathSound, 0.5f);
                        GameObject DeathParticle = Instantiate(Particle, transform.position, Quaternion.identity);
                        DeathParticle.transform.parent = null;
                        GameControllScript.GameRestart();
                        Destroy(HeroObject);
                    
                }
            }
        }
    }

        void WindBlow()
        {
            CooldownWindBlowCounter = CooldownWindBlow;
            Instantiate(Wind, HeroObject.transform.position, Quaternion.identity);
        }

        void TimeSlow()
        {
        GameControllScript.PlaySound(SlowTimeAudio,1f);
            CooldownSlowTimeCounter = CooldownSlowTime;
            Time.timeScale = SlowTimeCoef;
            DurationSlowTimeCounter = DurationSlowTime;
        Instantiate(TimeSlowEffect,new Vector2(0,2),Quaternion.identity);
        }
        void LaserBeamRay()
        {
            CooldownBeamCounter = CooldownBeam;
            DurationLaserCounter = DurationLaser;
        Instantiate(LaserExpl,transform.position,Quaternion.identity);
        }

    void ReviveBack() {
        Invulnirable.SetActive(false);
        }

        public void BonusSmaller() {
            SmallerCooldownCounter = SmallerCooldownMax;
            GameObject newScale=Instantiate(ScaleEffect, HeroObject.transform.position, Quaternion.identity);
            newScale.transform.parent = HeroObject.transform;
    }
        public void BonusMultiply()
        {
            MultiplyCooldownCounter = MultiplyCooldownMax;
            MultiplyToEnemy = 2;
            Instantiate(x2, new Vector2(0, 4f), Quaternion.identity);
    }
        public void BonusIceBlast() {
            Frozen = true;
            IceBlastCooldownCounter = IceBlastCooldownMax;
            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in Enemies)
            {
            GameObject NewIce= Instantiate(IceEffect, enemy.transform.position, Quaternion.identity);
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            NewIce.transform.parent = enemy.transform;
            }
        }
    public void BonusLightning() {
        Instantiate(LightningObject,HeroObject.transform.position,Quaternion.identity);
    }
}
