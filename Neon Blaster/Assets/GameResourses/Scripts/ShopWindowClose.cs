using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindowClose : MonoBehaviour
{
    private Transform BG;

    private void Start()
    {
        BG = transform.parent;
    }
    public void Exiting()
    {
        BG.gameObject.SetActive(false);
    }
}
