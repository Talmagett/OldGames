using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField] private SceneController controller;

    [SerializeField] private GameObject Card_Back;
    [SerializeField] private Animator CardRotating;
    public void OnMouseDown()
    {
        if (transform.rotation.y == 0 && controller.CanReveal)
        {
            CardRotating.SetBool("Rotated", true);
            controller.CardRevealed(this);
    }
    }

    private int _id;
    public int Id {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite ImageOfCard) {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = ImageOfCard;
    }

    public void Unreveal() {
        CardRotating.SetBool("Rotated", false);
    }
}
