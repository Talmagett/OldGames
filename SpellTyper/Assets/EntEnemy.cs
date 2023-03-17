using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntEnemy : MonoBehaviour
{
    public GameObject ArrowObj;
    public float CD;
    public GameObject DeathEff;
    public Transform ArrowCreation;
    private EnemyScript enemyScript;
    private Animator EntAnim;
    private void Awake()
    {
        EntAnim = GetComponent<Animator>();
    }
    IEnumerator Start()
    {
        enemyScript = GetComponent<EnemyScript>();
        while (true)
        {
            yield return new WaitForSeconds(CD);
            EntAnim.SetTrigger("Attack");
        }
    }
    public void ArrowShot() {
        Instantiate(ArrowObj, transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if (enemyScript._isDead) {
            Instantiate(DeathEff, ArrowCreation.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
