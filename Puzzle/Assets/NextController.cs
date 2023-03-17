using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextController : MonoBehaviour
{
    public static NextController Instance { get; set; }
    public static GameObject nextItem;
    public static GameObject curItem;
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        
    }
    public void LoadNextImage() {
        nextItem.SetActive(true);
        gameObject.SetActive(false);
        foreach (var item in curItem.GetComponent<EmptyControl>().Puzzles)
        {

            Destroy(item);
        }
    }
}
