using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public float HealingAmount=50;
    public int HealIndexer=1;
    public AudioClip clip;

    public Spell HealingOrbsSpell;
    IEnumerator Start()
    {
        if (HealingOrbsSpell.CurrentXp >= HealingOrbsSpell.XPToUpgrade[0])
        {
            HealingAmount = 75;
            if (HealingOrbsSpell.CurrentXp >= HealingOrbsSpell.XPToUpgrade[1])
            {
                HealingAmount = 25;
                HealIndexer = 4;
                if (HealingOrbsSpell.CurrentXp >= HealingOrbsSpell.XPToUpgrade[2])
                {
                    HealIndexer = 12;
                }
            }
        }
        for (int i = 0; i < HealIndexer; i++)
        {
            HeroComponent.instance.Heal(HealingAmount);
            if (i % 4 == 0) MainController.instance.AudioPlayer0_5.PlayOneShot(clip);
            yield return new WaitForSeconds(0.25f);
        }
        GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject,3f);
    }
}
