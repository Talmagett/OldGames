using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public float MissleSpeed;
    public GameObject ExplosionEff;
    public AudioClip ZapBegin;

    private Rigidbody2D MissleRB;
    private AudioSource _MissleAS;
    void Start()
    {
        _MissleAS = GameObject.Find("MissleSound").GetComponent<AudioSource>();
        _MissleAS.PlayOneShot(ZapBegin);
        StartCoroutine(DestroyObj());
        MissleRB=GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        MissleRB.velocity = MissleRB.velocity.normalized * MissleSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    { 
        if (other.gameObject.tag == "Ball")
        {
            _MissleAS.Play();
            Instantiate(ExplosionEff, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.collider.gameObject.tag=="Player")
        {
            _MissleAS.Play();
            Instantiate(ExplosionEff, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Missle")
        {
            _MissleAS.Play();
            Instantiate(ExplosionEff, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.collider.gameObject.tag == "Shield")
        {
            return;
        }
    }

    IEnumerator DestroyObj() {
        yield return new WaitForSeconds(4);
        _MissleAS.Play();
        Instantiate(ExplosionEff, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
