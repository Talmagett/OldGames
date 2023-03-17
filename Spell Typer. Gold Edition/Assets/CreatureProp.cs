using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreatureProp : MonoBehaviour
{
    public bool isBoss;
    public float HPMax;
    public Slider HPSlider;
    public Resist Fire;
    public Resist Ice;
    public Resist Lightning;
    [Space]
    public GameObject canvasObj;
    private Animator animThis;
    [Space]
    public float FrozenCounter;
    public float SlowCounter;
    public float ShockCounter;
    public float WetCounter;
    public float AccelerationCounter;
    [Space]
    public float MoveSpeed;
    bool Dying;
    public enum Resist {
        Normal,
        Armour,
        Immune,
        Vulnerable
    }
    void Start()
    {        
        if (isBoss) {HPMax *= 2;
        transform.localScale *= 2;
            MoveSpeed /=2;
        }
        HPSlider.maxValue = HPMax;
        HPSlider.value = HPMax;
        animThis = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!Dying) { 
            transform.position += -Vector3.right * MoveSpeed * Time.deltaTime * (WetCounter > 0 ? 0.7f : 1) * (FrozenCounter > 0 ? 0.01f : 1) * (SlowCounter > 0 ? 0.6f : 1) * (ShockCounter > 0 ? 0 : 1) * (AccelerationCounter > 0 ? 1.5f : 1);
            animThis.speed = 1 * (WetCounter > 0 ? 0.7f : 1) * (FrozenCounter > 0 ? 0.01f : 1) * (SlowCounter > 0 ? 0.6f : 1) * (ShockCounter > 0 ? 0 : 1); 
        }

        if (FrozenCounter > 0)
            FrozenCounter -= Time.deltaTime;
        if (WetCounter > 0)
            WetCounter -= Time.deltaTime;
        if (ShockCounter > 0)
            ShockCounter -= Time.deltaTime;
        if (SlowCounter > 0)
            SlowCounter -= Time.deltaTime;
        if (AccelerationCounter > 0)
            AccelerationCounter -= Time.deltaTime;
    }

    public void GetDamage(Vector2 value)
    {
        if (!Dying)
        {
            float DamageDealt = 0;
            switch (value.y)
            {
                case 0: DamageDealt = value.x * GetResist(Fire) * (WetCounter > 0 ? 0.2f : 1); break;
                case 1: DamageDealt = value.x * GetResist(Ice) * (WetCounter > 0 ? 1.2f : 1); break;
                case 2: DamageDealt = value.x * GetResist(Lightning) * (WetCounter > 0 ? 1.5f : 1); break;
                case 3: DamageDealt = value.x; break;
            }
            HPSlider.value -= DamageDealt;
            if (HPSlider.value <= 0) Die();
        }
    }

    public float GetResist(Resist element) {
        switch (element) {
            case Resist.Armour: return 0.5f;
            case Resist.Immune: return 0.1f;
            case Resist.Vulnerable: return 2;
            default: return 1;
        }
    }

    public void Freeze(float value) {
        if (FrozenCounter < value) FrozenCounter = value * (WetCounter > 0 ? 2 : 1);
    }

    public void Slow(float value) {
        if (SlowCounter < value) SlowCounter = value * (WetCounter > 0 ? 2 : 1);
    }
    
    public void SpeedUp(float value) {
        if (AccelerationCounter < value) AccelerationCounter = value;
    }

    public void Shock() {
        if (ShockCounter < (WetCounter > 0 ? 0.2f : 0.1f)) ShockCounter = (WetCounter > 0 ? 0.2f : 0.1f);
    }

    
    public void GetWet(float value) {
        if (WetCounter < value) WetCounter = value;
    }

    public void Melt()
    {
        FrozenCounter = 0;
        SlowCounter = 0;
    }

    public void Heal(float value) {
        HPSlider.value += value;
    }

    public void Die() {
        Dying = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        canvasObj.SetActive(false);
        animThis.speed = 1;
        animThis.SetTrigger("Die");
        Destroy(gameObject,1f);
    }
}
