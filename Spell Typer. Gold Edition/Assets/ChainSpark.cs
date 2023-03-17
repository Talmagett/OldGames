using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSpark : MonoBehaviour
{
    public float Damage;
    public int JumpCountsMax;
    public Spell ChainSparkSpell;

    public float Radius;
    public LayerMask layer;

    public AudioClip[] clips;
    public GameObject HitEff;
    private int JumpCounts;
    private float DelayJump=0.1f;
    private float ChanceToShock=0.05f;
    private float CritDmg=1.2f;
    private void Start()
    {
        if (ChainSparkSpell.CurrentXp >= ChainSparkSpell.XPToUpgrade[0]) {
            JumpCountsMax = 7;
            if (ChainSparkSpell.CurrentXp >= ChainSparkSpell.XPToUpgrade[1])
            {
                ChanceToShock = 0.1f;
                if (ChainSparkSpell.CurrentXp >= ChainSparkSpell.XPToUpgrade[2])
                {
                    JumpCountsMax = 9;
                    if (ChainSparkSpell.CurrentXp >= ChainSparkSpell.XPToUpgrade[3])
                    {
                        ChanceToShock = 0.2f;
                        if (ChainSparkSpell.CurrentXp >= ChainSparkSpell.XPToUpgrade[3])
                        {
                            CritDmg = 1.5f;
                        }
                    }
                }
            }
        }
    }
    private GameObject CurEnemy;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector2 enemyPos;
    private void Update()
    {
        if(DelayJump <= 0)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, Radius, layer);
            if (hitEnemies.Length > 0)
            {
                
                GameObject newPickEnemy = null;
                int whilecounter=0;
                do
                {
                    whilecounter++;
                    if (hitEnemies.Length < 2 && JumpCounts > 0)
                        Destroy(gameObject);
                    newPickEnemy = hitEnemies[Random.Range(0, hitEnemies.Length)].gameObject;
                }
                while (CurEnemy == newPickEnemy&& whilecounter<10);
                CurEnemy = newPickEnemy;
                
                enemyPos = new Vector2(CurEnemy.transform.position.x + Random.Range(-0.5f, 0.5f), CurEnemy.transform.position.y + Random.Range(-0.5f, 0.5f));
                transform.position = enemyPos;
                Instantiate(HitEff,enemyPos,Quaternion.identity);
                if(JumpCounts%2==0) MainController.instance.AudioPlayer0_5.PlayOneShot(clips[Random.Range(0, clips.Length)]);
                if (Random.Range(0f, 1f) > ChanceToShock)
                {
                    CurEnemy.SendMessage("Shock");
                    CurEnemy.SendMessage("GetDamage", new Vector2(Damage * CritDmg, 2));
                }
                else 
                    CurEnemy.SendMessage("GetDamage", new Vector2(Damage, 2));

                JumpCounts++;
                DelayJump += 0.2f;
                if (JumpCounts >= JumpCountsMax)
                {
                    transform.position = enemyPos;
                    DelayJump += 3f;
                    Destroy(gameObject, 0.2f);
                }

            }
        }
        DelayJump -= Time.deltaTime;
        if (DelayJump < -0.8f) Destroy(gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
