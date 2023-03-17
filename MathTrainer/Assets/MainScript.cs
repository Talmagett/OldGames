using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public Text ScoreText;
    public int score;
    public int highScore;
    public int DifficultMult;

    public Text Question;
    private int firstInt;
    private int secondInt;
    private int answerInt;
    [Space]
    [SerializeField] private AudioSource audioSource;
    public AudioClip Answered;

    public Text Answer;
    private int intText;

    [Space]
    public float MaxTimer;
    public Slider Timer;
    public Image TimerImage;

    public GameObject GameOverPanel;
    private int Difficult=1;
    void Start()
    {
        Timer.maxValue = MaxTimer;
        Clear();
        GiveTask(Random.Range(1, 3));
    }

    void Update()
    {
        if (Timer.value <= 0) GameOverPanel.SetActive(true);
        //GameOver        
        else
        {
            Timer.value -= Time.deltaTime;
            if (Timer.value <= 3) TimerImage.color = Color.red;
            else TimerImage.color = Color.white;
        }
    }

    public void GiveTask(int operation) {
        switch (operation) {
            case 1: { Sum(); } break;
            case 2: { Substract(); } break;
            case 3: { Multiply(); } break;
            case 4: { Divide(); } break;
            case 5: { Pow(); } break;
        }
        Timer.value = Timer.maxValue;
    }
    private void Sum() {
        firstInt = Random.Range(1, score* DifficultMult);
        secondInt = Random.Range(1, score* DifficultMult);
        answerInt = firstInt + secondInt;
        Question.text = firstInt + "+" + secondInt + "=";
    }
    private void Substract() {        
        secondInt = Random.Range(1, score* DifficultMult);
        firstInt = Random.Range(secondInt, score * (DifficultMult+1));
        answerInt = firstInt -secondInt;
        Question.text = firstInt + "-" + secondInt + "=";
    }

    private void Multiply()
    {
        firstInt = Random.Range(1, score);
        secondInt = Random.Range(1, score);
        answerInt = firstInt * secondInt;
        Question.text = firstInt + "*" + secondInt + "=";
    }
    private void Divide()
    {
        answerInt = Random.Range(1, (int)Mathf.Ceil(score * DifficultMult / 2));
        secondInt = Random.Range(1, score * DifficultMult);
        firstInt = secondInt * answerInt;    
        Question.text = firstInt + "/" + secondInt + "=";
    } 
    private void Pow()
    {
        firstInt = Random.Range(1, (int)Mathf.Ceil(score * DifficultMult / 5f));
        secondInt = Random.Range(2, 4);
        answerInt = (int)Mathf.Pow(firstInt,secondInt);
        Question.text = firstInt + "^" + secondInt + "=";
    }
    public void AddNumber(int value) {
        intText *= 10;
        intText += value;
        Answer.text = intText.ToString();
        if (answerInt == intText)
        {
            audioSource.PlayOneShot(Answered);
            StartCoroutine(Check());
        }
    }
    IEnumerator Check() {
        yield return new WaitForSeconds(0.2f);
        score++;
        ScoreText.text = score.ToString();
        Clear();
        if(score<3) GiveTask(Random.Range(1, 3));
        else GiveTask(Random.Range(1, 6));
    }
    public void Clear() {
        intText = 0;
        Answer.text = 0.ToString();       
    }

    public void RemoveLast() {        
        intText /= 10;
        Answer.text = intText.ToString();
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name) ;
    }
    public void ToMenu() {
        SceneManager.LoadScene(0) ;
    }
}