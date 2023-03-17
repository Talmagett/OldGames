using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject[] Objects;

    public float Angle;
    public int Amount;
    public float timer;
    public GameObject WinPanel;
    public bool isDecrease;
    
    IEnumerator Start()
    {

        
        for (int i = 0; i < Amount; i++)
        {
            yield return new WaitForSeconds(timer);
            Instantiate(Objects[Random.Range(0, Objects.Length)], transform.position, Quaternion.Euler(0, 0, Angle - 90));
            if (isDecrease) {
                timer = Mathf.Clamp(timer - 0.01f, 0.5f, 1);
                Amount++;
        }
        }
        yield return new WaitForSeconds(4);
        WinPanel.SetActive(true);
    }

    public void NextLvl(int value) {
        SceneManager.LoadScene(value);
    }
}
