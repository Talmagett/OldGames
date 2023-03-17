using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DoublePairController : MonoBehaviour
{
    public static DoublePairController Instance { get; set; }
    
    public float offsetX;
    public float offsetY;
     bool _hasTimer;
     bool _hasTurns;
    [SerializeField] private DoubleMainCard originalCard;
    int ThematicId;

    public Sprite[] ImagesBasics = new Sprite[] { };
    public Sprite[] ImagesAlphabet=new Sprite[] { };    
    public Sprite[] ImagesWinter = new Sprite[] { };
    public Sprite[] ImagesValentine = new Sprite[] { };
    public Sprite[] ImagesColors = new Sprite[] { };
    public Sprite[] ImagesMagic = new Sprite[] { };
    public Sprite[] ImagesNumbers = new Sprite[] { };
    Sprite[] TempSprites;
    int gridRows;
    int gridCols;
    AudioSource audioS;
    public AudioClip[] clips;
    public AudioClip[] mistake;
    public AudioClip[] win;
    public AudioClip[] lose;
    private void Awake()
    {
        Instance = this;
    }
    List<DoubleMainCard> cardList = new List<DoubleMainCard>();
   
    private void Start()
    {            
        audioS = GetComponent<AudioSource>();
        Time.timeScale = 1;
        gridCols = 4 + PlayerPrefs.GetInt("Scale");
        
        gridRows = 2 * PlayerPrefs.GetInt("Scale") + 4;
        switch (PlayerPrefs.GetInt("GameMode")) {
            case 1:
                _hasTimer = true; _hasTurns = false; break;
            case 2:
                _hasTimer = false; _hasTurns = true; break;
            case 3:
                _hasTimer = true; _hasTurns = true; break;
            default:
                _hasTimer = false; _hasTurns = false; break;
        }
        ThematicId = PlayerPrefs.GetInt("Theme");
        switch (ThematicId) {
            case 1: TempSprites = ImagesColors;break;
            case 2: TempSprites = ImagesMagic;break;
            case 3: TempSprites = ImagesNumbers;break;
            case 4: TempSprites = ImagesValentine;break;
            case 5: TempSprites = ImagesWinter;break;
            case 6: TempSprites = ImagesAlphabet; break;
            default: TempSprites = ImagesBasics; break;
        }
        _turns = (gridCols - 4) * 10 + 20 + (ThematicId == 6 ? 20 : 0) + (ThematicId == 5 ? 10 : 0);
        _timer =  (gridCols - 4) * 15 + 21 + (ThematicId == 6 ? 20 : 0) + (ThematicId == 5 ? 10 : 0);
        timerLabel.enabled = _hasTimer;
        turnsLabel.enabled = _hasTurns;
        scoreLabel.text = "Score: " + _score;
        if (_hasTurns) turnsLabel.text = ("Turn: " + _turns);
        if (_hasTimer) timerLabel.text = ("Time: " + _timer);
        Vector3 StartPos = originalCard.transform.position;//pos of first Card
        List<int> numbersList = new List<int>(); 
        for (int k = 0; k < gridCols * gridRows; k++)
        {
            numbersList.Add(k / 2);
        }
        int[] numbers = numbersList.ToArray();
        numbers = ShuffleArray(numbers);
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                DoubleMainCard card = Instantiate(originalCard, transform) as DoubleMainCard;
                int index = j * gridCols + i;
                int id = numbers[index];
                cardList.Add(card);

                card.ChangeSprite(id, TempSprites[id % TempSprites.Length]); 

                float Xpos = (offsetX * i) + StartPos.x;
                float Ypos = (offsetY * j) + StartPos.y;
                card.transform.position = new Vector3(Xpos, Ypos, StartPos.z);
            }
        }
        Camera.main.transform.position = new Vector3(gridCols/2f-0.5f, 0.5f+1.25f*(gridCols-4), -10);
        Camera.main.orthographicSize = (gridCols+0.5f);
        transform.localScale = Vector3.one*( Screen.width / 720);
    }
    bool isGameOver;
    private void Update()
    {
        if (_hasTimer) timerLabel.text="Time: " + (int)(_timer-=Time.deltaTime);
        if ( _timer <= 0&&!isGameOver) GameOver();
    }
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int rand = Random.Range(i, newArray.Length);
            newArray[i] = newArray[rand];
            newArray[rand] = tmp;
        }
        return newArray;
    }

    //-------------------------------------------------------------
    public GameObject Effect;
    private DoubleMainCard _firstRevealed;
    private DoubleMainCard _secondRevealed;

    private int _score = 0;
    int _turns;
    float _timer;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public Text scoreLabel;
    public Text turnsLabel;
    public Text timerLabel;
    public bool CanReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(DoubleMainCard card)
    {
        
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
            if (_hasTurns)
            {
                _turns--;
                turnsLabel.text = "Turn: " + _turns;
            }
        }
        else
        {
            _secondRevealed = card;
            if (_hasTurns) {_turns--;
            turnsLabel.text = "Turn: " + _turns;
            }
            StartCoroutine(CheckMatch());
        }
        if (_turns  <= 0) GameOver();
    }
    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.GetComponent<DoubleMainCard>().cardImage.sprite == _secondRevealed.GetComponent<DoubleMainCard>().cardImage.sprite)
        {
            if (_hasTurns) _turns += 4;
            if(_hasTimer)_timer += 4;
            _score+=3;
            if (_hasTurns) turnsLabel.text = ("Turn: " + _turns);
            if (_hasTimer) timerLabel.text = ("Time: " + _timer);
            scoreLabel.text = "Score: " + _score;
            cardList.Remove(_firstRevealed);
            cardList.Remove(_secondRevealed);
            yield return new WaitForSeconds(0.5f);
            Instantiate(Effect, _firstRevealed.transform.position, Quaternion.identity);
            Instantiate(Effect, _secondRevealed.transform.position, Quaternion.identity);

            Destroy(_firstRevealed.gameObject);
            Destroy(_secondRevealed.gameObject);
            audioS.PlayOneShot(clips[Random.Range(0,clips.Length)]);
            if (cardList.Count <= 0) { Win(); }
        }
        else
        {
            if(Random.Range(0f,1f)<0.2f) audioS.PlayOneShot(mistake[Random.Range(0, mistake.Length)]);
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();

        }
        _firstRevealed = null;
        _secondRevealed = null;
    }
    public Text HighScoreText;
    void Win() {
        audioS.PlayOneShot(win[Random.Range(0, win.Length)]);
        WinPanel.SetActive(true);
        _score += (int)_timer + _turns;
        scoreLabel.text = "Score: " + _score;
        HighScoreText.text = _score.ToString();
        if (_score > PlayerPrefs.GetInt("DoubleHighScore"))
        { 
            PlayerPrefs.SetInt("DoubleHighScore", _score); 
        }
        _hasTimer = false;
        _hasTurns = false;
    }

    void GameOver()
    {
        isGameOver = true;
        audioS.PlayOneShot(lose[Random.Range(0, lose.Length)]);
        GameOverPanel.SetActive(true);

        _hasTimer = false;
        _hasTurns = false;

    }
    public void ShowAd()
    {
            isGameOver = false;
            _turns += 15;
            _timer += 15;
            GameOverPanel.SetActive(false);
            if (_hasTurns) turnsLabel.text = ("Turn: " + _turns);
            _hasTimer = true;
            _hasTurns = true;
    }
    public void Pause() {
        Time.timeScale = 0;
    }
    public void Resume() {
        Time.timeScale = 1;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
