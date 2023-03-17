using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float Damage;

    public LineRenderer lineRenderer;
    public Transform Begin;
    public LayerMask enemiesLayer;
    public GameObject LaserBegin;
    public Spell LaserBeamSpell;
    private float DamageTick;
    private bool hasExtraDmg;
    IEnumerator Start()
    {
        if (LaserBeamSpell.CurrentXp >= LaserBeamSpell.XPToUpgrade[0])
        {
            Damage *= 1.5f;
            lineRenderer.startColor = Color.blue;
            if (LaserBeamSpell.CurrentXp >= LaserBeamSpell.XPToUpgrade[1])
            {
                hasExtraDmg = true;
                lineRenderer.startColor = Color.red;
            }
        }
            yield return new WaitForSeconds(3.55f);
        print(x);
        DamageTick = 10f;
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        lineRenderer.enabled = false;
        foreach (var item in particles)
        {
            item.Stop();
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    int x;
    void Update()
    {
        Vector2 dir = transform.right;
        RaycastHit[] hit = Physics.RaycastAll(transform.position, dir, 1000, enemiesLayer);  
        Debug.DrawRay(transform.position, dir);
        if (hit.Length>0&&DamageTick <= 0&& (LaserBegin.activeSelf))
        {
            foreach (var item in hit)
            {
                if (item.collider.gameObject.layer == 6)
                {
                    float enemyMaxHp = item.collider.gameObject.GetComponent<CreatureProp>().HPMax;
                        item.collider.gameObject.SendMessage("GetDamage", new Vector2(Damage / 2+(hasExtraDmg ? enemyMaxHp*0.001f:0), 0));
                        item.collider.gameObject.SendMessage("GetDamage", new Vector2(Damage / 2 + (hasExtraDmg ? enemyMaxHp * 0.001f : 0), 2));                    
                }
            }
            DamageTick = 0.1f;
            x++;
        }
        lineRenderer.SetPosition(1, Vector2.right*100);
        DamageTick -= Time.deltaTime;
    }
}
