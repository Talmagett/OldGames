using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float TimerMax;
    int counter=0;
    public List<EnemyWave> enemyWave = new List<EnemyWave>();
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int rand = UnityEngine.Random.Range(0, (MainController.instance.spellsData.Count - 1 < enemyWave.Count ? MainController.instance.spellsData.Count - 1 : enemyWave.Count));
            for (int i = 0; i < enemyWave[rand].count; i++)
            {
                GameObject lastObj = Instantiate(enemyWave[rand].Enemy, new Vector2(transform.position.x, enemyWave[rand].Enemy.transform.position.y + UnityEngine.Random.Range(-2f, 2f)), enemyWave[rand].Enemy.transform.rotation);
                if (counter == 7)
                {
                    counter = -1;
                    lastObj.GetComponent<CreatureProp>().isBoss = true;
                }
                yield return new WaitForSeconds(0.5f);
            }
            counter++;
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > MainController.instance.spellsData.Count * 4 + 5)
            yield return new WaitForSeconds(TimerMax); 
            yield return new WaitForSeconds(TimerMax);
        }
    }
}

[Serializable]
public struct EnemyWave
{
    public GameObject Enemy;
    public int count;
}
