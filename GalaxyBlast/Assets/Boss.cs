using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject Projectile1;
    public GameObject Projectile2;
    public GameObject Projectile3;
    public Transform LeftGun;
    public Transform RightGun;
    void Start()
    {
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot() {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 16;j++)
            {
                if (Player.instance != null)
                {
                    Vector3 dir = Player.instance.transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                    Instantiate(Projectile1, LeftGun.position, Quaternion.identity).transform.rotation = Quaternion.Euler(0, 0, angle+20);
                    Instantiate(Projectile1, RightGun.position, Quaternion.identity).transform.rotation = Quaternion.Euler(0, 0, angle-20);
                    Instantiate(Projectile1, transform.position, Quaternion.identity).transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                yield return new WaitForSeconds(0.4f);
            }
            yield return new WaitForSeconds(2);
        }
        StartCoroutine(SecondStage());
    }
    IEnumerator SecondStage() {
        for (int j = 0; j < 2; j++)
        {
            for (int k = 0; k < 5; k++)
            {
                for (int i = 0; i < 18; i++)
                {
                    Instantiate(Projectile2, transform.position, Quaternion.Euler(0, 0, i * 20+k*10));
                }
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(2);
        }
        StartCoroutine(ThirdStage());
    }

    IEnumerator ThirdStage() {
        for (int j = -2; j < 3; j++)
        {
            for (int k = -7; k <= 7; k+=2)
            {
                Instantiate(Projectile3, new Vector2(j+k, 8), Quaternion.Euler(0, 0,180));
            }

            yield return new WaitForSeconds(2);
        }
        StartCoroutine(Shoot());
    }
}
