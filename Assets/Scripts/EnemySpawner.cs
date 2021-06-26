using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    float maxSpawnRate = 5f; //seconds
    // Start is called before the first frame update
    void Start()
    {
        Invoke("spawnEnemy", maxSpawnRate);

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnEnemy() {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        //spawn enemy
        GameObject anEnemy = (GameObject)Instantiate(enemy);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn() {
        float spawnInSenconds;
        if (maxSpawnRate > 1f) {
            //pick a number between 1 and maxSpawnRate
            spawnInSenconds = Random.Range(1f, maxSpawnRate);
        }
        else {
            spawnInSenconds = 1f;
        }

        Invoke("spawnEnemy", spawnInSenconds);
    }

    //Function to increase difficulty
    void IncreaseSpawnRate() {
        if (maxSpawnRate > 1f) {
            maxSpawnRate--;
        }
        else {
            CancelInvoke("IncreaseSpawnRate");
        }
    }
}
