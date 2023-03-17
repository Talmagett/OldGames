using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int Health;
    public int MaxHealth;
    public SpriteRenderer ImageOfShield;
    public Collider2D coll2D;
    public GameObject Mage;
    public GameObject BrokenEff;
    public GameObject effectOfShield;
    public AudioSource Clip;

    void Start()
    {
        if (SpellsInstantiate.Spells.GetLvlOfShield() >= (int)SpellsInstantiate.Spells.ShieldMax.maxValue) MaxHealth = 50;
        else if (SpellsInstantiate.Spells.GetLvlOfShield() >= 20) MaxHealth = 40;
        else if (SpellsInstantiate.Spells.GetLvlOfShield() >= 16) MaxHealth = 35;
        else if (SpellsInstantiate.Spells.GetLvlOfShield() >= 12) MaxHealth = 30;
        else if (SpellsInstantiate.Spells.GetLvlOfShield() >= 8) MaxHealth = 25;
        else MaxHealth = 20;
        transform.position = Mage.transform.position;
    }

    void FixedUpdate()
    {
        ImageOfShield.color = new Color(ImageOfShield.color.r, ImageOfShield.color.g, ImageOfShield.color.b, (float)Health / (float)MaxHealth);
        coll2D.enabled = true;
        if (Health <= 0)
        {
            coll2D.enabled = false;
            effectOfShield.SetActive(false);
        }
        else
        {
            effectOfShield.SetActive(true);
        }
    }

    public void TakeDamage(int _damageAmount)
    {
        Health -= _damageAmount;
        if (Health <= 0)
        {
            Instantiate(BrokenEff, transform.position, Quaternion.identity);
            if (SpellsInstantiate.Spells.GetLvlOfShield() < (int)SpellsInstantiate.Spells.ShieldMax.maxValue) Mage.GetComponent<MageScript>().TakeDamage(-Health);
        }
    }
    public void CheckLVL(){
        Clip.Play();
        if (SpellsInstantiate.Spells.GetLvlOfShield() >= (int)SpellsInstantiate.Spells.ShieldMax.maxValue) MaxHealth = 50;
        else if (SpellsInstantiate.Spells.GetLvlOfShield() >= 20) MaxHealth = 40;
        else if (SpellsInstantiate.Spells.GetLvlOfShield() >= 16) MaxHealth = 35;
        else if (SpellsInstantiate.Spells.GetLvlOfShield() >= 12) MaxHealth = 30;
        else if (SpellsInstantiate.Spells.GetLvlOfShield() >= 8) MaxHealth = 25;
        else MaxHealth = 20;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.name == "Mage") {
            Physics2D.IgnoreCollision(other.collider,GetComponent<Collider2D>());
        }
    }
}
