using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public static LevelController Instance { get; set; }
    //Serializable classes implements
    public Wave[] enemyWaves;
    public GameObject[] Enemies;
    public GameObject BossApperience;
    public GameObject Boss;
    public GameObject powerUp;
    public float timeForNewPowerup;
    public GameObject[] planets;
    public float timeBetweenPlanets;
    public float planetsSpeed;
    List<GameObject> planetsList = new List<GameObject>();

    private AudioSource source;
    public AudioClip BossMusic;
    public AudioClip LoseMusic;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(PlanetsCreation());
        StartCoroutine(CreateEnemyWave());
    }

    public void SetMusic(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    //Create a new wave after a delay
    IEnumerator CreateEnemyWave()
    {
        while (Player.instance != null&&ScoreCounter.Instance.GetScore()<600)
        {
            int randWave = Random.Range(0, enemyWaves.Length);
            Wave wave = Instantiate(enemyWaves[randWave].gameObject).GetComponent<Wave>();
            wave.enemy = Enemies[(Mathf.FloorToInt(ScoreCounter.Instance.GetScore() / 200))>2?2: Mathf.FloorToInt(ScoreCounter.Instance.GetScore() / 200)];
            yield return new WaitForSeconds(8);
        }
        yield return new WaitForSeconds(10);
        SetMusic(BossMusic);
        Instantiate(BossApperience, new Vector2(0,9),Quaternion.identity);

        yield return new WaitForSeconds(3);
        Instantiate(Boss,new Vector2(0,9),Quaternion.identity);
    }

    //endless coroutine generating 'levelUp' bonuses. 
   
    IEnumerator PowerupBonusCreation() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeForNewPowerup);
            Instantiate(
                powerUp,
                //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border 
                new Vector2(Random.Range(-8, 8), 12), 
                Quaternion.identity
                );
        }
    }

    IEnumerator PlanetsCreation()
    {
        //Create a new list copying the arrey
        for (int i = 0; i < planets.Length; i++)
        {
            planetsList.Add(planets[i]);
        }
        yield return new WaitForSeconds(10);
        while (true)
        {
            ////choose random object from the list, generate and delete it
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex],new Vector2(Random.Range(-8,8),12), Quaternion.Euler(0,0, Random.rotation.z));
            planetsList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planetsList.Add(planets[i]);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
