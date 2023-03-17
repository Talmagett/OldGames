using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private Vector3 target;
    public GameObject Qcursor;
    public Sprite AimSprite;
    private GameControllerScript gameController;
    void Start()
    {
        Cursor.visible = false;
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
    }

    void Update()
    {
        
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.y));
        Qcursor.transform.position = new Vector2(target.x, target.y);
        if (gameController.isPlaying)
            Qcursor.GetComponent<SpriteRenderer>().sprite = AimSprite;
    }
}
