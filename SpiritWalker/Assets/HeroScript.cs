using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
    public GameObject DeathEff;
    public bool Dead;
    public Transform LvlStartPos;
    private bool isRespawning;
    [SerializeField] private SpriteRenderer HeroSprite;
    [SerializeField] private GameObject RealCheck;
    [SerializeField] private GameObject SpiritCheck;
    [SerializeField] private Rigidbody2D HeroRb;
    
    
    void Start()
    {
        transform.position = LvlStartPos.position;
        gameObject.layer = 8;
        HeroSprite.color = Color.white;
        RealCheck.SetActive(true);
        SpiritCheck.SetActive(false);
    }

    void Update()
    {
        if (!Dead&&!isRespawning)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.layer = 8;
                HeroSprite.color = Color.white;
                RealCheck.SetActive(true);
                SpiritCheck.SetActive(false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                gameObject.layer = 9;
                HeroSprite.color = Color.black;
                RealCheck.SetActive(false);
                SpiritCheck.SetActive(true);
            }
        }
        else if(Dead&&!isRespawning)
        {
            GetComponent<HeroMove>().enabled = false;
            Instantiate(DeathEff,transform.position,Quaternion.identity);
            transform.position = LvlStartPos.position;
            HeroSprite.color = new Color(0,0,0,0);
            isRespawning = true;
            HeroRb.bodyType = RigidbodyType2D.Static;
            StartCoroutine(Respawn());
        }
    }
    IEnumerator Respawn() {
        yield return new WaitForSeconds(2f);
        gameObject.layer = 8;
        HeroSprite.color = Color.white;
        RealCheck.SetActive(true);
        SpiritCheck.SetActive(false);
        GetComponent<HeroMove>().enabled = true;
        isRespawning = false;
        Dead = false;
        HeroRb.bodyType = RigidbodyType2D.Dynamic;
    }
}
