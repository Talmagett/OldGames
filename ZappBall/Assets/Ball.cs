using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Magnitude;
    public float SpeedOverTime;
    public AudioClip PlayerColliding;

    private Rigidbody2D _ballRB;
    private AudioSource _ASBall;
    void Start()
    {
        _ASBall = GetComponent<AudioSource>();
        _ballRB = GetComponent<Rigidbody2D>();
        int angleRand=Random.Range(0,2);
        _ballRB.AddForce(Vector2.right*Mathf.Pow(-1, angleRand)* Magnitude);
    }
        
    void FixedUpdate()
    {
        _ballRB.velocity = _ballRB.velocity.normalized * SpeedOverTime;
        if (SpeedOverTime <= 30)
        {
            SpeedOverTime += 0.001f * Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Gate") {
            if (other.collider.gameObject.name=="RedGate") {
                GameController.BlueScore.AddScoreToBlue();
            }
            else if (other.collider.gameObject.name == "BlueGate")
            {
                GameController.RedScore.AddScoreToRed();
                
            }
            Destroy(gameObject);
        }
        if (other.collider.gameObject.tag == "Player"|| other.collider.gameObject.tag == "Shield")
            _ASBall.PlayOneShot(PlayerColliding);
    }
}
