using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    public float Damage;
    public float FreezeDuration;
    private Vector2 FirstScale;
    private Vector2 MaxScale;
    private bool isGrow=true;
    private IEnumerator Start()
    {
        FirstScale = new Vector2(Random.Range(0.3f, 0.9f), 0);
        transform.localScale = FirstScale;
        MaxScale = new Vector2(FirstScale.x, Random.Range(0.5f,1f));
        yield return new WaitForSeconds(1.5f);
        isGrow = false;
    }

    private void Update()
    {
        if(transform.localScale.y<= MaxScale.y&& isGrow) {
            transform.localScale = Vector2.Lerp(transform.localScale, MaxScale, 6*Time.deltaTime);
        }else if(transform.localScale.y >= FirstScale.y && !isGrow) {
            transform.localScale = Vector2.Lerp(transform.localScale, FirstScale,  4*Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy")){
            collision.SendMessage("GetDamage",new Vector2(Damage,1));
            collision.GetComponent<CreatureProp>().Freeze(FreezeDuration);
        }
    }
}
