using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControlTouch : MonoBehaviour
{
    public float AngleFirst;
    public float AngleLast;
    public LineRenderer FirstLine;
    public LineRenderer LastLine;
    public SpriteRenderer arrowPhoto;
    public Material FirstMat;
    public Material LastMat;
    bool turned;
    void Update()
    {        
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject() ||    EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (turned)
                {
                    transform.rotation = Quaternion.Euler(0, 0, AngleFirst);
                    FirstLine.sortingOrder = -1;
                    LastLine.sortingOrder = 0;
                    arrowPhoto.material = LastMat;
                }

                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, AngleLast);
                    FirstLine.sortingOrder = 0;
                    LastLine.sortingOrder = -1;
                    arrowPhoto.material = FirstMat;
                }
                turned = !turned;
            }
        }
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Train"))
        {
            collision.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
        }
    }
}
