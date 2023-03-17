using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void NextLvl(int value)
    {
        SceneManager.LoadScene(value);
    }
}
