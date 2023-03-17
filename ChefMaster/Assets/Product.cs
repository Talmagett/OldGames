using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public float ForceMagn;
    public string BoxName;
    public GameObject EffCorrect;
    public GameObject EffMistake;

    private void Update()
    {
        transform.GetChild(0).rotation = Quaternion.identity;
        transform.position += transform.up * ForceMagn * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.rotation = Quaternion.Euler(0, 0, other.transform.GetChild(0).rotation.eulerAngles.z + 270);
            print(other.transform.GetChild(0).rotation.eulerAngles.z);
        }
        else if (other.CompareTag("Box"))
        {
            {
                if (other.name == BoxName)
                {
                    PlayerController.Instance.AddScore();
                    Destroy(Instantiate(EffCorrect, other.transform.position, Quaternion.identity),1.5f);
                    PlayerController.Instance.PlayCorrect();
                    Destroy(gameObject);
                }
                else {
                    Destroy(Instantiate(EffMistake, transform.position, Quaternion.identity),1.5f);
                    PlayerController.Instance.PlayMistake();
                    Destroy(gameObject);
                }
        }
        }
    }
}
