using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleMainCard : MonoBehaviour
{
    public SpriteRenderer cardImage;
    //[SerializeField] private GameObject Card_Back;
     private Animator CardAnim;
    public void OnMouseDown()
    {

        if ((!IsPointerOverUIObject()))
        {
            CardAnim = GetComponent<Animator>();
            if (transform.rotation.y == 0 && DoublePairController.Instance.CanReveal)
            {
                CardAnim.SetBool("Rotated", true);
                DoublePairController.Instance.CardRevealed(this);
            }
        }
    }
    private int _id;
    public int Id {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite ImageOfCard) {
        _id = id;
        cardImage.sprite = ImageOfCard;
    }

    public void Unreveal() {
        CardAnim.SetBool("Rotated", false);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
