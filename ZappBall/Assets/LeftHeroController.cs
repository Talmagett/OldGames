using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftHeroController : MonoBehaviour
{
    public float HeroSpeed;
    public GameObject Missle;
    public float Magnitude;
    public GameObject Shield;
    public Slider EnergyUI;

    private Rigidbody2D HeroRB;
    private float VerMove;
    private float _energyCounter = 0;
    void Start()
    {
        HeroRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        EnergyUI.value = _energyCounter;
        if (Input.GetKey(KeyCode.W))
        {
            VerMove = HeroSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            VerMove = -HeroSpeed;
        }

        if (Input.GetKeyDown(KeyCode.D)&&_energyCounter>2) {
            GameObject MissleElect = Instantiate(Missle, transform.position, Quaternion.Euler(0, -90, 0));
            _energyCounter -= 2;
            MissleElect.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Magnitude);
        }
        if (Input.GetKey(KeyCode.A)) {
            Shield.SetActive(true);
        }
        else {
            Shield.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        HeroRB.velocity = Vector2.zero;
        HeroRB.position += new Vector2(0, VerMove * Time.deltaTime);
        _energyCounter = Mathf.Clamp(_energyCounter,0,8);
        if (!Shield.activeSelf)
        {
        _energyCounter += Time.fixedDeltaTime;
        }
                
    }
}
