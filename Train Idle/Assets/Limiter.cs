using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limiter : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void LimitPos() {
        rectTransform.anchoredPosition = new Vector2(Mathf.Clamp(rectTransform.anchoredPosition.x,xMin,xMax),transform.position.y);
    }
}
