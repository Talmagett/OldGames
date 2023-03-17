using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideChanger : MonoBehaviour
{
    public GameObject FirstObj;
    public GameObject SecondObj;
    private Collider2D collider2D;
    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 touchPos = new Vector2(wp.x, wp.y);
                if (collider2D == Physics2D.OverlapPoint(touchPos))
                {
                    SecondObj.SetActive(!SecondObj.activeSelf);
                    FirstObj.SetActive(!FirstObj.activeSelf);
                }
            }
        }
    }
}