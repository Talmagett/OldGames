using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Spell")]
public class Spell : ScriptableObject
{
    public GameObject ObjToCreate;
    [Space]
    public int CurrentXp;
    public int[] XPToUpgrade;
    [HideInInspector]
    public bool isMax;
}
