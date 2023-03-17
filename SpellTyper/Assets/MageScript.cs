using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MageScript : MonoBehaviour
{
    public static  MageScript Mage{get;set;}
    public Slider Health;
    public LightShotAttack shotAttack;
    public GameObject PauseObj;
    public GameObject RestartOBj;
    public GameObject DeathEff;
    private float HealingCounter;
    private bool _isDead=false;
    private void Awake()
    {
        Time.timeScale = 1;
        Mage = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        GetComponent<Animator>().SetBool("ReadyToAttack",shotAttack.ReadyToAttack);
        
        if (Health.value<Health.maxValue && SpellsInstantiate.Spells.GetLvlOfHealing() >= 30) {
            HealingCounter -= Time.deltaTime;
            if (HealingCounter <= 0) {
                TakeDamage((int)(-Health.maxValue*0.03f));
                HealingCounter = 3f;
            }
        }
        if (Health.value <= 0&&!_isDead) {
            _isDead = true;
            Death();
        }
    }
    public GameObject inputSystem;
    public SpellSystemInput systemInput;
         
    void Death() {
        Instantiate(DeathEff,transform.position,Quaternion.identity);
        RestartOBj.SetActive(true);
        PauseObj.SetActive(false);
        inputSystem.SetActive(false);
        systemInput.enabled = false;
        Time.timeScale = 0;
    }
    public void TakeDamage(int _damageAmount)
    {
         Health.value -= _damageAmount;
    }

    public void LightAttackParent()
    {
        shotAttack.LightAttack();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
