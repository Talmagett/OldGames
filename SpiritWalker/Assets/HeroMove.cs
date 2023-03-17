using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    public float Speed;
    public float jumpForce;
    [SerializeField] private Rigidbody2D HeroRb;
    [SerializeField] private SpriteRenderer sprite;
    public bool isGrounded = false;
    private float _isGroundRemember;

    void Update()
    {
        float Xpos = Input.GetAxisRaw("Horizontal");
        HeroRb.velocity = new Vector2(Xpos * Speed, HeroRb.velocity.y);  
        Jump();        
    }
    void Jump()
    {
        _isGroundRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) {
            _isGroundRemember = 0.3f;
        }

        if (_isGroundRemember >0f&& isGrounded)
        {            
            HeroRb.velocity = new Vector2(HeroRb.velocity.x, jumpForce);
        }
    }
}
