using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    public string BonusName;
    private HeroScript heroScript;
    public AudioClip Sound;
    private GameControllerScript gameController;
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null) heroScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroScript>();
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            heroScript = other.gameObject.transform.GetComponentInParent<HeroScript>();
            gameController.PlaySound(Sound,gameObject.GetComponent<AudioSource>().volume);
            if (BonusName == "Smaller")
            {
                heroScript.BonusSmaller();
            }
            if (BonusName == "Multiply")
            {
                heroScript.BonusMultiply();
            }
            if (BonusName == "IceBlast")
            {
                heroScript.BonusIceBlast();
            }
            if (BonusName == "Lightning")
            {
                heroScript.BonusLightning();
            }
            Destroy(gameObject);
        }
    }
}
