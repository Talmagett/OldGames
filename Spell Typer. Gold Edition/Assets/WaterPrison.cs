using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPrison : MonoBehaviour
{
    public Spell WaterPrisonSpell;
    public AudioClip Clip3;
    public AudioClip Clip5;
    public AudioClip Clip7;
    private float Duration=3;
    private Vector2 FirstScale=Vector2.zero;
    private Vector2 MaxScale=Vector2.one;
    private bool isGrow = true;
    AudioSource source;
    private IEnumerator Start()
    {
        source = GetComponent<AudioSource>();
        transform.localScale = FirstScale;
        if (WaterPrisonSpell.CurrentXp >= WaterPrisonSpell.XPToUpgrade[0])
        {
            Duration = 5;
            source.clip = Clip5;
            if (WaterPrisonSpell.CurrentXp >= WaterPrisonSpell.XPToUpgrade[1])
            {
                MaxScale = Vector2.one * 1.5f;
                if (WaterPrisonSpell.CurrentXp >= WaterPrisonSpell.XPToUpgrade[2])
                {
                    Duration = 7;
                    source.clip = Clip7;
                }
            }
        }
        else source.clip = Clip3;
        source.Play();
        yield return new WaitForSeconds(Duration);
        isGrow = false;
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        if (transform.localScale.y <= MaxScale.y && isGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, MaxScale, 3 * Time.deltaTime);
        }
        else if (transform.localScale.y >= FirstScale.y && !isGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, FirstScale, 6 * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<CreatureProp>().GetWet(3);
        }
    }
}
