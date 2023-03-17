using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class SpellSystemInput : MonoBehaviour
{
    public InputField SpellTextInput;
    public Transform CasterTransform;
    public Transform MageTransform;
    public GameObject[] CasterEffects;
    public GameObject PausePanel;
    public Animator MageAnim;
    
    public Text SpellsPer10s;

    
    private int _casterIndex;
    private string LastWord;
    private bool _CheckWord;
    void Start()
    {

    }

    public void ReadFromFile(string Spell) {
        _CheckWord = true;
        if (SpellTextInput.text != "")
        {
            if (SpellTextInput.text == LastWord)
                SpellsInstantiate.Spells.LastWordCreated = true;

            else
                SpellsInstantiate.Spells.LastWordCreated = false;
        }
        
        switch (Spell) {
            case "Огненный Шар":
            case "Fire Ball":
                {
                    SpellsInstantiate.Spells.FireBallSpell(CasterTransform);
                    _casterIndex = 0;
                }
                break;
            case "Ice Shard":
            case "Осколок Льда":
                {
                    SpellsInstantiate.Spells.IceBlastSpell(CasterTransform);
                    _casterIndex = 1;
                }
                break;
            case "Lightning":
            case "Молния":
                {
                    SpellsInstantiate.Spells.LightningSpell(CasterTransform);
                    _casterIndex = 2;
                }
                break;
            case "Hurricane":
            case "Ураган":
                {
                    SpellsInstantiate.Spells.WindBlow();
                    _casterIndex = 3;
                }break;
            case "Poison Cloud":
            case "Облако Яда":
                {
                    SpellsInstantiate.Spells.PoisonCloudMissle(CasterTransform);
                    _casterIndex = 4;
                }
                break;
            case "Malediction":
            case "Проклятие":
                {
                    SpellsInstantiate.Spells.Malediction();
                    _casterIndex = 5;
                }
                break;
            case "Healing":
            case "Исцеление":
                {
                    SpellsInstantiate.Spells.Healing(MageTransform);
                    _casterIndex = 6;
                }
                break;
            case "Shield":
            case "Щит":
                {
                    SpellsInstantiate.Spells.Shield();
                    _casterIndex = 7;
                }
                break;
            case "Summon":
            case "Призыв":
                {
                    SpellsInstantiate.Spells.Summon(CasterTransform);
                    _casterIndex = 8;
                }
                break;
            case "Laser Beam":
            case "Лазерный Луч":
                {
                    SpellsInstantiate.Spells.LaserBeam(CasterTransform);
                    _casterIndex = 9;
                }
                break;
            
            default: _CheckWord = false; break;
            }

        if (_CheckWord) {
            
            SpellsPer10s.text = ((float)SpellsInstantiate.Spells.GetSpellCount() / (Time.time/10)).ToString();
            MageAnim.SetTrigger("Cast");
            
            LastWord = Spell;
            for (int i = 0; i < CasterEffects.Length; i++) {
                if (i == _casterIndex) continue;
                CasterEffects[i].SetActive(false);
            }
            CasterEffects[_casterIndex].SetActive(true);
        }
        SpellTextInput.text = "";
    }
    
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || (Input.GetKey(KeyCode.RightControl))){
            SpellTextInput.text = "";
        }
        SpellTextInput.ActivateInputField();
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            SpellTextInput.caretPosition = 0; // desired cursor position
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

    }
    

    public void Pause() {
        if (Time.timeScale == 1)
        {
            SpellTextInput.enabled = false;
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            SpellTextInput.enabled = true;
            PausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
