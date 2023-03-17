using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
    public float XRange;
    public float YRange;
    public Text ScoreText;
    public Snake snake;
    void Start()
    {
        newPos();
    }

    void newPos() {
        List<Vector2> AreaNewPos = new List<Vector2>();
        for (float i = -XRange; i <= XRange; i+=0.5f)
        {
            for (float j = -YRange; j <= YRange; j+=0.5f)
            {
                AreaNewPos.Add(new Vector2( i, j));
            }
        }
        foreach (var item in snake.bodies)
        {
            AreaNewPos.Remove(new Vector2(item.transform.position.x, item.transform.position.y));
        }
        transform.position = AreaNewPos[Random.Range( 0, AreaNewPos.Count)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Controller.AddScore(ScoreText);
            newPos();
            collision.SendMessage("AddBody");
        }
    }
    public void Restart() {
        SceneManager.LoadScene(0);
    }
}
