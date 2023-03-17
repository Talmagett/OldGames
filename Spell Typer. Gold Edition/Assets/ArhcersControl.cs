using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArhcersControl : MonoBehaviour
{
    public static ArhcersControl instanse { get; set; }
    public Text RandText;

    public GameObject Arrow;
    public GameObject HitEffect;
    private void Awake()
    {
        instanse = this;
    }
    private void Start()
    {
        NewWord();
    }
    public void NewWord() {
        string newWord= RandomString(Random.Range(3,10));
        char[] a = newWord.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        newWord= new string(a);

        RandText.text = newWord;
    }
    
    private static System.Random random = new System.Random();
    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Range(1, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());
    }

    public void Shot(int len) {
            Instantiate(Arrow, new Vector2( transform.position.x, SpellCast.instance.HeroPlayer.transform.position.y+1), Quaternion.Euler(0, 0, 270)).SendMessage("SetDmg", 1 + len * 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HeroComponent.instance.GetDamage(other.gameObject.GetComponent<CreatureProp>().HPSlider.value);
            other.gameObject.GetComponent<CreatureProp>().Die();
            Instantiate(HitEffect, other.gameObject.transform.position,Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("EnemyProjectile"))
        {
            HeroComponent.instance.GetDamage(other.gameObject.GetComponent<EnemyMissle>().Damage);
            Instantiate(HitEffect, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

}
