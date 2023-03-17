using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    private GameObject EnemyObject;
    private Rigidbody2D EnemyRigidbody;
    public float Mag;
    private GameObject[] Enemies;
    public Text HealthText;
    public int Health;
    public float HealthMult;
    public GameObject NextEnemy;
    public int EnemyCostToScore;
    private BackgroundScript scoreScript;
    public GameObject MoneyObject;
    public int EnemyCost;
    public GameObject Particle;
    public GameObject BonusSmaller;
    public GameObject BonusMultiply;
    public GameObject BonusIceBlast;
    public GameObject BonusLightning;
    private MainMenu MenuScript;
    private HeroScript heroScript;
    float Angle;

    private void Start()
    {
        EnemyObject = gameObject;
        MenuScript= GameObject.Find("MainMenu").GetComponent<MainMenu>();
        scoreScript = GameObject.FindGameObjectWithTag("BG").GetComponent<BackgroundScript>();
        EnemyRigidbody = GetComponent<Rigidbody2D>();
        float Angle = Random.Range(-1f,1f);
        EnemyRigidbody.AddForce(new Vector2(Angle / 5, -0.3f) * Mag / 1.5f, ForceMode2D.Impulse);
        Health = (int)(MenuScript.LvlDamage*MenuScript.LvlAddGun *HealthMult*10)+(int)(scoreScript.ScoreCount/100*HealthMult);
        if (GameObject.FindGameObjectWithTag("Player") != null) heroScript = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<HeroScript>();
        EnemyCost = EnemyCost + Health / 100;
     
    }

    private void Update()
    {
        HealthText.text = Health.ToString();
        
        if (Health <= 0) {
            GameObject Money = Instantiate(MoneyObject, EnemyObject.transform.position, Quaternion.Euler(0, 0, 0));
            Instantiate(Particle, transform.position, Quaternion.Euler(0, 0, 0));
            Money.GetComponent<MoneyScript>().MoneyCost = EnemyCost;
            float Chance = Random.Range(0, 100);
            if (Chance < MenuScript.LvlBonuses*3) {
                int RandomBonus = Random.Range(1, 5);
                switch (RandomBonus) {
                    case 1: Instantiate(BonusSmaller, EnemyObject.transform.position, Quaternion.Euler(0, 0, 0)); break;
                    case 2: Instantiate(BonusMultiply, EnemyObject.transform.position, Quaternion.Euler(0, 0, 0)); break;
                    case 3: Instantiate(BonusIceBlast, EnemyObject.transform.position, Quaternion.Euler(0, 0, 0)); break;
                    case 4: Instantiate(BonusLightning, EnemyObject.transform.position, Quaternion.Euler(0, 0, 0)); break;
                }
            }
            for (int i = 1; i <= 2; i++)
            {
                Angle = Mathf.Pow(-1, i) * 0.5f;
                GameObject EnemyNext = Instantiate(NextEnemy, new Vector2(transform.position.x + Angle, transform.position.y), Quaternion.Euler(0, 0, 0));
                if (EnemyNext.name == "Empty") break;
                EnemyNext.GetComponent<Rigidbody2D>().AddForce(new Vector2(Angle / 5, 1) * Mag / 1.5f, ForceMode2D.Impulse);
            }
            scoreScript.ScoreCount += EnemyCostToScore * heroScript.MultiplyToEnemy;
            Destroy(EnemyObject);
            
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).rotation = Quaternion.Euler(0,0,-transform.rotation.z);
        }
        Enemies = FindObjectsOfType<GameObject>();
        foreach (GameObject enemy in Enemies)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                Physics2D.IgnoreCollision(EnemyObject.GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Ground")
        {
            float RandomAngle = Random.Range(-0.5f, 0.5f);
                EnemyRigidbody.AddForce(new Vector2(RandomAngle,1) * Mag, ForceMode2D.Impulse);
        }
        if (other.gameObject.name == "RightWall") {
            EnemyRigidbody.AddForce(-Vector2.right * Mag/5, ForceMode2D.Impulse);
        }
        if (other.gameObject.name == "LeftWall")
        {
            EnemyRigidbody.AddForce(Vector2.right * Mag /5, ForceMode2D.Impulse);
        }

    }
}