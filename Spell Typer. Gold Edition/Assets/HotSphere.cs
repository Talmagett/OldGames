using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSphere : MonoBehaviour
{
    public float Speed;
    public GameObject Explosion;
    public GameObject FireGround;
    public GameObject FirePillar;

    public Spell HotSphereSpell;
    public AudioClip clip;
    private void Start()
    {
        MainController.instance.AudioPlayer0_5.PlayOneShot(clip);
    }
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * Speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(Explosion,transform.position,Quaternion.identity);            
            
            if (HotSphereSpell.CurrentXp >= HotSphereSpell.XPToUpgrade[1])
            {
                var obj = Instantiate(FirePillar, new Vector2(transform.position.x, -3), Quaternion.identity);
                obj.transform.position = new Vector2(transform.position.x, -3);
            }
            else if (HotSphereSpell.CurrentXp >= HotSphereSpell.XPToUpgrade[0]) {
                var obj = Instantiate(FireGround, transform.position, Quaternion.identity);
                obj.transform.position = new Vector2(transform.position.x, -3);
            }
            Destroy(gameObject);
        }
    }
}
