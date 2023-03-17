using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//guns objects in 'Player's' hierarchy
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX; 
}

public class PlayerShooting : MonoBehaviour {

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;
    public GameObject Rocket;
    public GameObject Mine;
    public GameObject LightningBlast;
    //time for a new shot
    [HideInInspector] public float nextFire;


    [Tooltip("current weapon power")]
    [Range(1, 7)]       //change it if you wish
    public int weaponPower = 1;
    [HideInInspector]
    public int ProjIndex=-1;

    public Guns guns;
    bool shootingIsActive = true; 
    [HideInInspector] public int maxweaponPower = 4; 
    public static PlayerShooting instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (shootingIsActive)
        {
            if (Time.time > nextFire)
            {
                MakeAShot();                                                         
                nextFire = Time.time + 1 / fireRate;
            }
        }
    }
    public bool Miner;
    int MineCreator = 0;
    int RocketCounter=0;
    int ShipShield=0;
    void MakeAShot() 
    {
        switch (weaponPower) // according to weapon power 'pooling' the defined anount of projectiles, on the defined position, in the defined rotation
        {
            case 1:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();
                break;
            case 2:
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, Vector3.zero);
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, Vector3.zero);
                guns.rightGunVFX.Play();
                break;
            case 3:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX.Play();
                break;
            case 4:
                guns.leftGunVFX.Play();
                guns.rightGunVFX.Play();
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                break;
            case 5:
                guns.leftGunVFX.Play();
                guns.rightGunVFX.Play();
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -10));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 10));
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                break;
            default:
                guns.leftGunVFX.Play();
                guns.rightGunVFX.Play();

                if (Miner)
                {
                    MineCreator++;
                    if (MineCreator > 3) {
                        MineCreator = 0;                        
                        Destroy(Instantiate(Mine, guns.centralGun.transform.position, Quaternion.identity).GetComponent<DirectMoving>(),Random.Range(0.4f,0.8f));
                    }
                }
                RocketCounter++;
                if (RocketCounter > 5) {
                    RocketCounter = 0;
                    CreateLazerShot(Rocket, guns.leftGun.transform.position, Vector3.zero);
                    CreateLazerShot(Rocket, guns.rightGun.transform.position, Vector3.zero);
                }
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -10));
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 10));
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                break;
        }
    }
    public void ShotBlast() {
        for (int i = 0; i < 20; i++)
        {
            Instantiate(LightningBlast, guns.centralGun.transform.position, Quaternion.Euler(0,0,i*18));
        }
    }
    public void LvlUp() {
        Player.instance.Heal();
        if (weaponPower < 6) weaponPower++;
        else
        {
            if (!Miner) { Miner = true; weaponPower++; }
            else { ShotBlast(); }
        }
    }
    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        Instantiate(lazer, pos, Quaternion.Euler(rot));
    }
}
