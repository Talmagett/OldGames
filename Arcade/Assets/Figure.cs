 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public GameObject DeathEff;
    public GameObject BombEff;
    public GameObject LightningEff;
    public Vector3 Center;
    public float FallTimer;
    private float TimerCounter;
    public static int height=22;
    public static int width=10;
    public State BlockType;
    private Spawner spawner;
    public enum State { 
        Normal,
        Bomb,
        Lightning
    }
    private static Transform[,] grid = new Transform[width,height];

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        FallTimer = spawner.FallTime;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            float Xpos = Input.GetAxisRaw("Horizontal");
            transform.position += Vector3.right * Xpos;
            if (!ValidMove())
                transform.position -= Vector3.right * Xpos;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(Center),Vector3.forward,-90);
            
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(Center), Vector3.forward, 90);
        }
        if (ValidMove())
        {
            if (TimerCounter < 0)
            {
                transform.position += -Vector3.up;
                TimerCounter = FallTimer;
            }
            TimerCounter -= (Input.GetKey(KeyCode.DownArrow) ? Time.deltaTime * 10 : Time.deltaTime);
            if (!ValidMove()) {
                transform.position += Vector3.up;
                AddToField();
                
                this.enabled = false;
                spawner.CreateNewFigure();
            }
        }
    }
    void CheckTheField() {
        for (int i = height-1; i>=0; i--)
        {
            if (HasLine(i)) {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i) {
        for (int j = 0; j < width; j++) {
            if (grid[j, i] == null) return false;
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            spawner.AddScore();
            GameObject eff = Instantiate(DeathEff, grid[j, i].transform.position, Quaternion.identity);
            var main = eff.GetComponent<ParticleSystem>().main;
            main.startColor = GetComponentInChildren<SpriteRenderer>().color;
            Destroy(eff, 2);
            Destroy(grid[j,i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i) {
        for (int y = i; y < height; y++) {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null) 
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= Vector3.up;
                }
            }
        }
    }


    void Bomb() {
        Destroy(Instantiate(BombEff, transform.position, Quaternion.identity), 2);
        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedY = Mathf.RoundToInt(transform.position.y);
        for (int i = (roundedX>0)?-1:0; i < ((roundedX < width-1)?2:1); i++)
        {
            for (int j = (roundedY>0)?-1:0; j < 2; j++)
            {
                if (grid[roundedX+i, roundedY+j] != null)
                {                   
                    Destroy(grid[roundedX + i, roundedY + j].gameObject);
                    spawner.AddScore();
                    grid[roundedX + i, roundedY + j] = null;
                }
            }
        }
        Destroy(gameObject);
    }
    void Lightning() {
        Destroy(Instantiate(LightningEff,new Vector2( transform.position.x,0), Quaternion.identity), 2);
        int roundedX = Mathf.RoundToInt(transform.position.x);
            for (int j = 0; j < height; j++)
            {
                if (grid[roundedX, j] != null)
                {
                    Destroy(grid[roundedX,  j].gameObject);
                       spawner.AddScore();
                    grid[roundedX, j] = null;
                }
        }
        Destroy(gameObject);
    }
    void AddToField() {
        if (BlockType == State.Bomb) {
            Bomb();
        }
        else if (BlockType == State.Lightning) {
            Lightning();
        }
        else
        {
            foreach (Transform children in transform)
            {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);
                if (roundedY > 17f)
                {
                    spawner.GameOver();
                    
                }
                grid[roundedX, roundedY] = children;
            }
            
        }
        CheckTheField();
    }
    bool ValidMove() {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY > height) {
                
                return false;
            }
            if (grid[roundedX, roundedY] != null) return false;   }
        return true;
    }
}
