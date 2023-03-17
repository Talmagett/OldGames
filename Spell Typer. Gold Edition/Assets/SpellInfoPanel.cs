using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using static System.Convert;
public class SpellInfoPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Spell spellData;
    public Image XPDisplay;
    public GameObject linePrev;

    public Text SpellTitle;
    public Image IconSpell;

    [HideInInspector]
    public bool isRoot;
    public List<GameObject> PreviousSpells;
    private LineRenderer[] LineToPrev;
    private void Start()
    {
        LineToPrev = new LineRenderer[ PreviousSpells.Count];
        SpellTitle.text = spellData.name;
        spellData.CurrentXp = PlayerPrefs.GetInt(spellData.name);
        if (!isRoot)
        {
            for (int i = 0; i < PreviousSpells.Count; i++)
            {
                LineToPrev[i] = Instantiate(linePrev, transform.position, Quaternion.identity, transform).GetComponent<LineRenderer>();
                LineToPrev[i].positionCount = 2;
            }

            int counter = 0;
            foreach (var item in PreviousSpells)
            {
                if (item.GetComponent<SpellInfoPanel>().spellData.isMax)
                {
                    counter++;
                    if (counter >= PreviousSpells.Count)
                    {
                        active = true;
                    }
                }
            }
        }
        else {
            active = true;
        }

    }
    public void OnPointerEnter(PointerEventData data)
    {

    }

    public void OnPointerExit(PointerEventData data)
    {

    }
    bool active;
    private void Update()
    {
        XPDisplay.fillAmount = (float)spellData.CurrentXp / spellData.XPToUpgrade[spellData.XPToUpgrade.Length - 1];
        spellData.isMax = ToBoolean((int)XPDisplay.fillAmount);

        if (active)
        {
            foreach (var item in PreviousSpells)
            {
                SpellTitle.enabled = true;
                IconSpell.color = Color.white;
            }
            if (!isRoot)
            {
                for (int i = 0, k = 0; i < PreviousSpells.Count; i++, k++)
                {
                    LineToPrev[i].enabled = true;
                    LineToPrev[i].SetPosition(0, transform.position);
                    LineToPrev[i].SetPosition(1, PreviousSpells[i].transform.position);
                }
            }
        }
        else
        {
            SpellTitle.enabled = false;
            IconSpell.color = Color.black;
            if (!isRoot)
            {
                for (int i = 0; i < PreviousSpells.Count; i++)
                {
                    LineToPrev[i].enabled = false;
                }
            }
            int counter = 0;
            foreach (var item in PreviousSpells)
            {
                if (item.GetComponent<SpellInfoPanel>().spellData.isMax)
                {
                    counter++;
                    if (counter >= PreviousSpells.Count)
                    {
                        active = true;
                        MainController.instance.AddMessage("You gain new spell " + spellData.name);
                    }
                }
            }
        }
        PlayerPrefs.SetInt(spellData.name, spellData.CurrentXp);
    }
}
/*

[CustomEditor(typeof(SpellInfoPanel), true)]
public class ListTestEditorDrawer : Editor
{
    private SerializedProperty PrevSpellsList;
    public override void OnInspectorGUI()
    {
        // DrawDefaultInspector();
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        DrawPropertiesExcluding(serializedObject, "PreviousSpells");
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();


        var myScript = target as SpellInfoPanel;
        myScript.isRoot = GUILayout.Toggle(myScript.isRoot, "is Root");
        PrevSpellsList = serializedObject.FindProperty("PreviousSpells");
        //EditorGUI.BeginDisabledGroup(!myScript.isRoot);
        if (!myScript.isRoot)
        {
            EditorGUILayout.PropertyField(PrevSpellsList, new GUIContent("Require Spells"), true);
            serializedObject.ApplyModifiedProperties();
        }

        //EditorGUI.EndDisabledGroup();
    }
}*/