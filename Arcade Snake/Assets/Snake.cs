using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float TimerMove;
    public float Speed;
    public GameObject PartBody;
    public List<GameObject> bodies = new List<GameObject>();
    bool GameOver;
    public GameObject HitEffect;
    public LineRenderer liner;
    float angle;
    public GameObject Restart;
    IEnumerator Start()
    {
        AddBody(); AddBody(); AddBody();
        for (int i = bodies.Count - 1; i > 0; i--)
        {
            bodies[i].transform.position = bodies[i - 1].transform.position;
        }
        while (!GameOver)
        {
            yield return new WaitForSeconds(TimerMove);
            float HeadAngle = transform.rotation.eulerAngles.z;
            if (HeadAngle != angle)
            {
                if ((angle==90|| angle==270) &&(HeadAngle == 0 || HeadAngle == 180))
                    transform.rotation = Quaternion.Euler(0, 0, angle);
                else if ((angle == 0 || angle == 180)&&(HeadAngle == 270 || HeadAngle == 90))
                    transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            for (int i = bodies.Count-1; i> 0; i--)
            {
                bodies[i].transform.position = bodies[i - 1].transform.position;
                liner.SetPosition(i, bodies[i].transform.position);
            }
            transform.position += transform.up * Speed;
            liner.SetPosition(0,transform.position);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            angle = 270;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            angle = 0;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            angle = 90;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            angle = 180;
    }
    public void AddBody() {
        Transform PrevLast = bodies[bodies.Count - 2].transform;
        Transform Last = bodies[bodies.Count - 1].transform;
        float xDelta = Last.position.x - PrevLast.position.x;
        float yDelta = Last.position.y - PrevLast.position.y;
        Vector2 spawnPos = new Vector2(bodies[bodies.Count - 1].transform.position.x+ xDelta, bodies[bodies.Count - 1].transform.position.y+ yDelta);
        bodies.Add(Instantiate(PartBody, spawnPos, Quaternion.identity));
        liner.positionCount = bodies.Count;
        liner.SetPosition(bodies.Count-1, bodies[bodies.Count-1].transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall")) {
            GameOver = true;
            GetComponent<Collider2D>().enabled = false;
            Instantiate(HitEffect,transform.position,Quaternion.identity);
            Restart.SetActive(true);
            Destroy(this);

        }
    }
}
