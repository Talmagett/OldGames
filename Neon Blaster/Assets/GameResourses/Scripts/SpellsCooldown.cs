using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsCooldown : MonoBehaviour
{
    public Image LightningCD;
    public Image WindCD;
    public Image SlowTimeCD;
    public Image LaserCD;
    private HeroScript heroScript;
    private LaserGun laserGunScript;
    void Start()
    {
        
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            heroScript = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<HeroScript>();
            WindCD.fillAmount = heroScript.CooldownWindBlowCounter / heroScript.CooldownWindBlow;
            SlowTimeCD.fillAmount = heroScript.CooldownSlowTimeCounter / heroScript.CooldownSlowTime;
            LaserCD.fillAmount = heroScript.CooldownBeamCounter / heroScript.CooldownBeam;
            laserGunScript = heroScript.gameObject.GetComponentInChildren<LaserGun>();
            LightningCD.fillAmount = laserGunScript.nextShot / laserGunScript.ShotDelay;
        }
    }
}
