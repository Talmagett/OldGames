using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShopSystemWindow : MonoBehaviour, IPointerClickHandler
{
    [TextArea(2, 8)]
    public string textOnWindow;
    public Image BG;
    public Text TextToWindow;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        { }
        else if (eventData.button == PointerEventData.InputButton.Right)
            Enter();
    }

    public void Enter()
    {
            BG.gameObject.SetActive(true);
            TextToWindow.text = textOnWindow;
    }
}
