using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcery : MonoBehaviour
{
    public GameObject SpeedEff;
    public float CastCD;
    [SerializeField]private Animator anim;
    public float Radius;
    public LayerMask layer;
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(CastCD);
            anim.SetTrigger("Cast");
        }
    }
    public void Cast() {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, Radius, layer);
        foreach (var item in hitEnemies)
        {
            Instantiate(SpeedEff, item.transform.position,Quaternion.identity);
            item.SendMessage("SpeedUp", 2f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
