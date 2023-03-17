using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ObjDepending : MonoBehaviour
{
    public Sprite DarkMode;
    public Sprite LightMode;
    public Torch ElectricTorchDependense;

    public UnityEvent eventToDo = new UnityEvent();
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        ElectricTorchDependense.eventToDo.AddListener(TurnOn);
        ElectricTorchDependense.eventToDo.AddListener(eventToDo.Invoke);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (ElectricTorchDependense.Activated) spriteRenderer.sprite = LightMode;
        else { spriteRenderer.sprite = DarkMode; }
    }
    public void TurnOn() {
        spriteRenderer.sprite = LightMode;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,ElectricTorchDependense.LaserConnector.position);
    }
}
