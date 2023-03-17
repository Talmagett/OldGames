using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSystem : MonoBehaviour
{
    public static ElectricSystem III { get; set; }
    public LineRenderer LightLine;
    public GameObject LightShot;
    public Sprite DarkTorch;
    public Sprite LightTorch;
    private void Awake()
    {
        III = this;
    }
}