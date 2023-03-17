using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public GameObject Staff;
         
    public float MoveSpeed;
    public float xMin,xMax;
    public float yMin,yMax;
    public float FlyMagn;

    public GameObject LeftFootEff, RightFootEff;
    private Rigidbody HeroRb;
    private Animator anim;
    private bool isAndroid;
    void Start()
    {
        HeroRb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        LeftFootEff.SetActive(false);
        RightFootEff.SetActive(false);
        isAndroid =PlayerPrefs.GetInt("Android")==1 ? true : false;
    }

    float Ypos = 0;
    void Update()
    {
        float Xpos =Input.GetAxis("Horizontal");
        anim.SetInteger("Velocity", (int)(Xpos*10));
        Ypos = ((Input.GetAxisRaw("Vertical") > 0 ? 1:(Input.GetAxisRaw("Vertical") < 0 ? -1:0)));
        HeroRb.velocity = new Vector2(Xpos* MoveSpeed, HeroRb.velocity.y);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x,xMin, xMax), Mathf.Clamp(Mathf.Lerp(transform.position.y,(Ypos>0? yMax:(Ypos<0?yMin: transform.position.y)),Time.deltaTime*FlyMagn), yMin, yMax));
        if (transform.position.y > -1)
        {
            LeftFootEff.SetActive(true);
            RightFootEff.SetActive(true);
        }
        else {

            LeftFootEff.SetActive(false);
            RightFootEff.SetActive(false);
        }

    }

    public void AndroidChange() {
        isAndroid = !isAndroid;
        PlayerPrefs.SetInt("Android", isAndroid?1:0);
    }
         
}
