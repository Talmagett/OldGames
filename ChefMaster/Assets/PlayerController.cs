using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }
    public Text Score;
    private static int ScoreAmount;
    private AudioSource source;
    public AudioClip[] Correct;
    public AudioClip[] Mistake;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        source = GetComponent<AudioSource>();
        Score.text = PlayerPrefs.GetInt("ScoreCur",0).ToString() ;
    }

    public void PlayCorrect() {
        source.PlayOneShot(Correct[Random.Range(0, Correct.Length)]);
    }
    public void PlayMistake() {
        source.PlayOneShot(Mistake[Random.Range(0, Mistake.Length)]);
    }
    public void AddScore() {
        ScoreAmount++;
        PlayerPrefs.SetInt("ScoreCur", ScoreAmount);
        Score.text = ScoreAmount.ToString();
    }
    public bool left;
    public bool right;
    public bool up;
    public bool down;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && right) {
            transform.rotation = Quaternion.Euler(0,0,0);
        } if (Input.GetKeyDown(KeyCode.LeftArrow) && left) {
            transform.rotation = Quaternion.Euler(0,0,180);
        } if (Input.GetKeyDown(KeyCode.UpArrow) && up) {
            transform.rotation = Quaternion.Euler(0,0,90);
        } if (Input.GetKeyDown(KeyCode.DownArrow) && down) {
            transform.rotation = Quaternion.Euler(0,0,270);
        }
    }
}
