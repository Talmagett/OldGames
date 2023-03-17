using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int gridRows;
    public int gridCols;
    public float offsetX;
    public float offsetY;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] Images;
    private void Start()
    {
        turnsLabel.text = "Turn: " + _turns;
        Vector3 StartPos = originalCard.transform.position;//pos of first Card
        List<int> numbersList = new List<int>();
        for (int k = 0; k < gridCols * gridRows; k++) {
            numbersList.Add(k / 2);
        }
        int[] numbers = numbersList.ToArray();
        numbers = ShuffleArray(numbers);
        for (int i = 0; i < gridCols; i++) {
            for (int j = 0; j < gridRows; j++) {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else {
                    card = Instantiate(originalCard) as MainCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id,Images[id% Images.Length]);

                float Xpos = (offsetX * i) + StartPos.x;
                float Ypos = (offsetY * j) + StartPos.y;
                card.transform.position = new Vector3(Xpos,Ypos,StartPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers) {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++) {
            int tmp = newArray[i];
            int rand = Random.Range(i,newArray.Length);
            newArray[i] = newArray[rand];
            newArray[rand]=tmp;
        }
        return newArray;
    }

    //-------------------------------------------------------------
    public GameObject Effect;
    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;
    public int _turns = 0;
    [SerializeField] private TextMesh scoreLabel;
    [SerializeField] private TextMesh turnsLabel;
    public bool CanReveal {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card) {
        if (_turns == 0) Restart();
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
            _turns--;
            turnsLabel.text = "Turn: " + _turns;
        }
        else {
            _secondRevealed = card;
            _turns--;
            turnsLabel.text = "Turn: " + _turns;
            StartCoroutine(CheckMatch());
        }
    }
    private IEnumerator CheckMatch() {
        if (_firstRevealed.GetComponent<SpriteRenderer>().sprite == _secondRevealed.GetComponent<SpriteRenderer>().sprite) 
        {
            _turns+=4;
            _score++;
            turnsLabel.text = "Turn: " + _turns;
            scoreLabel.text = "Score: " + _score;
            yield return new WaitForSeconds(0.3f);
            Instantiate(Effect, _firstRevealed.transform.position, Quaternion.identity);
            Instantiate(Effect, _secondRevealed.transform.position, Quaternion.identity);
            Destroy(_firstRevealed.gameObject);
            Destroy(_secondRevealed.gameObject);
        }
        else 
        { 
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();

        }
        _firstRevealed = null;
        _secondRevealed = null;
    }

    public void Restart() {
        SceneManager.LoadScene(0);
    }
}
 