using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    public GameObject PausePanel;
    public TimeText timeText;
    public float moveSpeed = 600f;
    public GameObject MoneyEff;
    float movement = 0f;

    void Update()
    {
        movement= Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (other.gameObject.tag == "Boost") {
            timeText.TimeCounter += Random.Range(1,10);
            Destroy(other.gameObject);
            GameObject Eff =Instantiate(MoneyEff,other.transform.position,Quaternion.identity);
            Destroy(Eff,3f);
        }
    }
}
