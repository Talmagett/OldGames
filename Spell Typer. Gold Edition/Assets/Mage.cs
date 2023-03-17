using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public Animator anim;
    public Transform ShotPos;
    public GameObject Missle;
    public float AttackCd;
    float attackCdCounter;
    GameObject LastObj;
    float scaleMagn;
    private void Start()
    {
        transform.position = new Vector2(transform.position.x-Random.Range(2,10), transform.position.y);
        attackCdCounter = AttackCd;
        StartCoroutine(CreateNew());
    }
    void Update()
    {
        if (attackCdCounter < 0&& scaleMagn>1) {
            anim.SetTrigger("Attack");
            attackCdCounter = AttackCd;
        }
        if (LastObj)
        {
            scaleMagn += Time.deltaTime*0.2f;
            LastObj.transform.localScale = Vector3.one*scaleMagn;
            attackCdCounter -= Time.deltaTime;
        }        
    }

    public void Attack() {
        if (LastObj) {LastObj.GetComponent<EnemyMissle>().enabled = true;
            LastObj.transform.parent = null;
        }
        StartCoroutine(CreateNew());
    }

    IEnumerator CreateNew() {
        yield return new WaitForSeconds(1);
        LastObj=Instantiate(Missle, ShotPos.position, Quaternion.identity);
        LastObj.transform.parent = transform;
        scaleMagn = 0;
    }
}
