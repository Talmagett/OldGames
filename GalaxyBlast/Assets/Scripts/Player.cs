using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public static Player instance;
    public int MaxHealth;
    public Slider hpSlider;
    public GameObject LoseWindow;
    private float health;
    bool isDead;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }
    public IEnumerator Start()
    {
        Heal();
        yield return new WaitForSeconds(8);
        Destroy( GetComponent<Animator>());
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        hpSlider.value = health;
        if (health <= 0 && !isDead)
        {
            Destruction();
        }
    }
    public void Heal()
    {
        health = MaxHealth;
        hpSlider.value = health;
    }
    //'Player's' destruction procedure
    void Destruction()
    {
        isDead = true;
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
        LoseWindow.SetActive(true);
        LevelController.Instance.SetMusic(LevelController.Instance.LoseMusic);
    }
    public void WinScene() {
        SceneManager.LoadScene("Win");
    }

}
















