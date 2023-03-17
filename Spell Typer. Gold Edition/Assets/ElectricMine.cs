using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricMine : MonoBehaviour
{
    public GameObject Explosion;
    public float DmgMultCoeff=2;
    public AudioClip Mine;
    private float DamageMult;
    private IEnumerator Start()
    {
        MainController.instance.AudioPlayer0_5.PlayOneShot(Mine);
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.25f);
            DamageMult += DmgMultCoeff;
        }
        Explode();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        SpellArea electric= Instantiate(Explosion,transform.position,Quaternion.identity).GetComponent<SpellArea>();
        electric.Damage += DamageMult;
        Destroy(gameObject);
    }
}
