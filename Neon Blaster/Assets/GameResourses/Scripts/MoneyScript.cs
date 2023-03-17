using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public int MoneyCost;
    private GameObject MainMenu;
    private BackgroundScript BGScript;
    private Rigidbody2D MoneyRigidbody2D;
    public AudioClip Sound;
    private HeroScript heroScript;
    private GameControllerScript gameController;
    private void Start()
    {
        MoneyRigidbody2D = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        if (GameObject.FindGameObjectWithTag("Player") != null) heroScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroScript>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"&&other.gameObject.name== "MoneyDetector")
        {
            heroScript = other.gameObject.transform.GetComponentInParent<HeroScript>();
            BGScript = GameObject.Find("Background").GetComponent<BackgroundScript>();
            if (heroScript.MultiplyToEnemy == 2)
            {
                BGScript.MoneyCount = BGScript.MoneyCount + MoneyCost;
            }
            BGScript.MoneyCount = BGScript.MoneyCount + MoneyCost;
            gameController.PlaySound(Sound, gameObject.GetComponent<AudioSource>().volume);
            gameObject.SetActive(false);
            Destroy(gameObject,0.5f);
        }
        if (other.gameObject.name == "Ground")
        {
            MoneyRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
    }

}