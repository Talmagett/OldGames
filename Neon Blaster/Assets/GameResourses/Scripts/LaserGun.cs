using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public float ShotDelay;
    public float nextShot;
    public GameObject missle;
    public GameObject Gun;
    public GameObject Lightning;
    public GameObject LightningShoot;
    private MainMenu mainMenuScript;

    void Start()
    {
        mainMenuScript = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        ShotDelay = 10-0.5f*mainMenuScript.LvlLaserGun;
    }

    void FixedUpdate()
    {
        
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Angle - 90);
        if (nextShot <= 0)
        {
            nextShot = 0;
            Lightning.SetActive(true);
            if (Input.GetButton("Fire1"))
            {
                nextShot = ShotDelay;
                StartCoroutine(Shoot());
            }
        }
        else
        {
            nextShot -= Time.deltaTime;
            Lightning.SetActive(false);
        }
    }
    private IEnumerator Shoot() {
        for (int i = 0; i < mainMenuScript.LvlLaserGun; i++)
        {
            Instantiate(missle, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z));
            GameObject newL = Instantiate(LightningShoot, Gun.transform.position, Quaternion.identity);
            newL.transform.parent = Gun.transform;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
