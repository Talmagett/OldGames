using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyControl : MonoBehaviour
{
    public static EmptyControl Instance { get; set; }

    public int Columns, Rows;
    public GameObject Part;
    
    public List<Sprite> parts = new List<Sprite>();
    [HideInInspector]
    public List<GameObject> Puzzles = new List<GameObject>();
    private List<Picture> Pictures = new List<Picture>();
    private List<Vector2> positions = new List<Vector2>();
    public GameObject WinWindow;
    public GameObject nextObj;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for (int colum = (Columns - 1); colum >= -(Columns - 1); colum -= 2)
        {
            for (int row = -(Rows - 1); row <= Rows - 1; row += 2)
            {
                Picture picture;
                if (row == Rows - 1 && colum == -(Columns - 1)) picture = GetComponent<Picture>();
                else
                {
                    picture = Instantiate(Part).GetComponent<Picture>();
                    if (parts.Count > 0)
                    {
                        picture.GetComponent<SpriteRenderer>().sprite = parts[0];
                        parts.RemoveAt(0);
                    }
                }
                picture.transform.position = new Vector2(row, colum);
                picture.BasicPos = picture.transform.position;
                Pictures.Add(picture);
                positions.Add(picture.BasicPos);
                Puzzles.Add(picture.gameObject);
            }
        }

        foreach (var item in Pictures)
        {
            int randPos = Random.Range(0, positions.Count);
            item.transform.position = positions[randPos];
            positions.Remove(positions[randPos]);
        }
        CheckPuzzle();
    }
    public void CheckPuzzle()
    {
        print("check");
        foreach (var item in Pictures)
        {
            if (item.BasicPos != (Vector2)item.transform.position) return;
        }
        WinWindow.SetActive(true);
        NextController.nextItem= nextObj;
        NextController.curItem = gameObject;

        foreach (var item in Pictures)
        {
            Destroy(item);
        }
    }
}
