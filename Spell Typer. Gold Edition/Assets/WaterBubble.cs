using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubble : MonoBehaviour
{
    public float Speed;
    public GameObject WaterPrison;
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
            Instantiate(WaterPrison,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
