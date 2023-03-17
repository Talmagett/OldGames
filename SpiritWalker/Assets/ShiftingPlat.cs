using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftingPlat : MonoBehaviour
{
    public bool active;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Collider2D collider2D;
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            active = !active;
            if (active) { collider2D.enabled = true; sprite.color = new Color(1, 1, 1, 1); }
            else { collider2D.enabled = false; sprite.color = new Color(1, 1, 1, 0.1f); }
        }
    }

}
