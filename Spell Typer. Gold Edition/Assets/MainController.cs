using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;
public class MainController : MonoBehaviour
{
    public static MainController instance { get; set; }
    public RectTransform SpellsInfoWindow;
    public RectTransform SpellsBG;

    public Text RandText;

    public GameObject MessageWindow;

    public InputField spellInput;
    public List<Spell> spellsData=new List<Spell>();

    public AudioSource AudioPlayer1;
    public AudioSource AudioPlayer0_2;
    public AudioSource AudioPlayer0_5;
    public AudioSource AudioPlayer0_8;
    private void Awake()
    {
        instance = this;
        ClearEmpty();
        Spell[] spells = (Spell[])Resources.FindObjectsOfTypeAll(typeof(Spell));
        foreach (Spell item in spells)
        {
            spellsData.Add(item);
        }
    }
    void ClearEmpty() {
        RemoveNull(spellsData);
    }
    static void RemoveNull<T>(List<T> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i] == null)
            {
                list.RemoveAt(i); // O(n)
            }
        }
    }
    public void PreventPaste()
    {/*
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && (Input.GetKey(KeyCode.V) || Input.GetKeyUp(KeyCode.V)))
        {
            spellInput.text = "";
        }*/
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            spellInput.text = "";
        }
        if (SpellsInfoWindow.localScale.x == 0)
        {
            spellInput.ActivateInputField();
            spellInput.Select();
            spellInput.caretPosition = spellInput.text.Length;
        }
        else {

            spellInput.DeactivateInputField();
        }
        if (Input.GetKeyDown(KeyCode.Tab)) Pause();
    }
    public void Pause()
    {
        if (SpellsInfoWindow.localScale.x == 0)
        {
            Time.timeScale = 0f;            
            SpellsInfoWindow.localScale = Vector2.one;
            SpellsBG.localScale = Vector2.one;
        }
        else
        {
            Time.timeScale = 1;            
            SpellsInfoWindow.localScale = Vector2.zero;
            SpellsBG.localScale = Vector2.zero;
        }
    }


    public void CheckInputSpell() {
        string spellValue = spellInput.text;
        if (RandText.text == spellValue)
        {
            ArhcersControl.instanse.NewWord();
            ArhcersControl.instanse.Shot(spellValue.Length);
            spellInput.text = "";
        }
        foreach (var spell in spellsData)
        {
            if (spell.name == spellValue) {
                SpellCast.instance.CastSpell(spell);
                spellInput.text = "";
                break;
            }        
        }
    }
    private Queue<string> MessagesToShow= new Queue<string>();
    private bool isShowing;
    public void AddMessage(string spellName) {
        MessagesToShow.Enqueue(spellName);
        if (!isShowing)
        {
            StartCoroutine(ShowMessage());
        }
    }
    public IEnumerator ShowMessage() {
    ShowNew:
        GameObject MessageLast = Instantiate(MessageWindow,spellInput.transform.parent);
        Text TextToShow = MessageLast.GetComponentInChildren<Text>();
        TextToShow.text = MessagesToShow.Dequeue();
        Destroy(MessageLast, 3f);
        isShowing = true;
        yield return new WaitForSeconds(3f);
        if (MessagesToShow.Count > 0) { goto ShowNew; }
        else isShowing = false; 
    }
    public void ClearXP()
    {        
        foreach (Spell item in spellsData)
        {
            item.CurrentXp = 0;
            item.isMax = false;
        }
        spellsData.Clear();
    }


    public void SaveHP() {
        PlayerPrefs.SetFloat("PlayerHp",HeroComponent.instance.HPSlider.value);
    }
}
