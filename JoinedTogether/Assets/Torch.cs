using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class Torch : MonoBehaviour
{
    public bool Activated;
    public Transform TorchLightPos;
    public Transform LaserConnector;
    public bool isGenerator;
    public UnityEvent eventToDo = new UnityEvent();
    private LineRenderer Beam;
    private bool isConnected;
    private Transform bullet;
    private void Start()
    {
        if (Activated&&!isGenerator) {
            GetComponent<SpriteRenderer>().sprite = ElectricSystem.III.LightTorch;
        }
    }
    private void OnMouseDown()
    {
        if (Activated&& !isConnected) {
            isConnected = true;
            bullet = Instantiate(ElectricSystem.III.LightShot, TorchLightPos.position, Quaternion.identity).transform;
            bullet.GetComponent<LightShot>().SetParent(this);
        }
    }
    private void OnMouseDrag()
    {
        if (Activated && isConnected)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad+90;
            // Rotate Object
            bullet.rotation = Quaternion.Euler(0,0, AngleDeg);
        }
    }
    private void OnMouseUp()
    {
        if (Activated && isConnected)
        {
            isConnected = false;
            bullet.GetComponent<LightShot>().enabled = true;
        }
    }
    public void CreateLink(Torch parentLink) {
        GetComponent<SpriteRenderer>().sprite = ElectricSystem.III.LightTorch;
        Beam = Instantiate(ElectricSystem.III.LightLine);
        Beam.SetPosition(0, LaserConnector.position);
        Beam.SetPosition(1, parentLink.LaserConnector.position);
        eventToDo.Invoke();
    }
    public void SetActivate() {
        Activated = true;
    }
}