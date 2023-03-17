using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    public float AngleRight;
    public float AngleLeft;
    public float AngleUp;
    public float AngleDown;
    public float minSwipeDistY;
    public float minSwipeDistX;    
    public bool isLeft;
    public bool isRight;
    public bool isUp;
    public bool isDown;
    [Space]
    public LineRenderer LeftLine;    
    public LineRenderer RightLine;
    public LineRenderer UpLine;
    public LineRenderer DownLine;

    public SpriteRenderer arrowPhoto;
    public Material LeftMat;
    public Material RightMat;
    public Material UpMat;
    public Material DownMat;
    private Vector2 startPos;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;
                case TouchPhase.Ended:
                    if (isUp || isDown)
                    {
                        float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                        if (swipeDistVertical > minSwipeDistY)
                        {
                            float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                            if (swipeValue > 0 && isUp)//up swipe
                            {
                                transform.rotation = Quaternion.Euler(0, 0, AngleUp);
                                arrowPhoto.transform.rotation= Quaternion.Euler(0, 0, AngleUp);
                                SortLines(1);
                            }

                            else if (swipeValue < 0 && isDown)//down swipe
                            {
                                transform.rotation = Quaternion.Euler(0, 0, AngleDown);
                                arrowPhoto.transform.rotation = Quaternion.Euler(0, 0, AngleDown);
                                SortLines(3);
                            }

                        }
                    }
                    if (isLeft || isRight)
                    {
                        float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
                        if (swipeDistHorizontal > minSwipeDistX)
                        {
                            float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
                            if (swipeValue > 0 && isRight)//right swipe
                            {
                                transform.rotation = Quaternion.Euler(0, 0, AngleRight);
                                arrowPhoto.transform.rotation = Quaternion.Euler(0, 0, AngleRight);
                                SortLines(0);
                            }
                            else if (swipeValue < 0 && isLeft)//left swipe
                            {
                                transform.rotation = Quaternion.Euler(0, 0, AngleLeft);
                                arrowPhoto.transform.rotation = Quaternion.Euler(0, 0, AngleLeft);
                                SortLines(2);
                            }
                        }
                    }

                    break;
            }
        }
    }
    void SortLines(int index) {
        if (isLeft)LeftLine.sortingOrder = -1;
        if(isRight)RightLine.sortingOrder = -1;
        if (isUp)UpLine.sortingOrder = -1;
        if(isDown)DownLine.sortingOrder = -1;
        switch (index) {
            case 0: RightLine.sortingOrder = 0; arrowPhoto.material = RightMat; break;
            case 1: UpLine.sortingOrder = 0; arrowPhoto.material = UpMat; break;
            case 2: LeftLine.sortingOrder = 0; arrowPhoto.material = LeftMat; break;
            case 3: DownLine.sortingOrder = 0; arrowPhoto.material = DownMat; break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Train")) {
            collision.transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z);
        }
    }
}
