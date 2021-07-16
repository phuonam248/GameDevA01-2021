using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class CampaignControl : MonoBehaviour
{
    private BezierFollow bf;

    [SerializeField]
    private PathCreator[] pathCreator;
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private Transform[] spawnPos;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartCampaign1()
    {
        SpawnRound1();
    }


    private IEnumerator SpawnYellowEnemies()
    {
        int numOfEnemies = 12;
        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[2]);
            enemy.transform.position = spawnPos[3].position;
            enemy.GetComponent<Follower>().pathCreator = pathCreator[3];
            yield return new WaitForSeconds(1.1f);
        }
    }

    private IEnumerator SpawnBlueEnemies()
    {

        int numOfEnemies = 12;
        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[0]);
            enemy.transform.position = spawnPos[0].position;
            enemy.GetComponent<Follower>().pathCreator = pathCreator[0];
            yield return new WaitForSeconds(1.1f);
        }
    }

    private IEnumerator SpawnGreenEnemies()
    {
        int numOfEnemies = 12;
        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[1]);
            enemy.transform.position = spawnPos[5].position;
            enemy.GetComponent<Follower>().pathCreator = pathCreator[5];
            yield return new WaitForSeconds(1.1f);
        }
    }

    private void SpawnRound1()
    {
        int numOfEnemies = 9;
        float[] posInLine0 = new float[numOfEnemies];
        float[] posInLine5 = new float[numOfEnemies];
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        for (int i = 0; i < numOfEnemies; i++)
        {
            posInLine0[i] = (float)(i - numOfEnemies/2) * max.x / 5;
            posInLine5[i] = (float)(i - numOfEnemies/2) * max.x / 5;
        }
        StartCoroutine(SpawnEnemyLine(numOfEnemies, posInLine0, 0));
        StartCoroutine(SpawnEnemyLine(numOfEnemies, posInLine5, 5));
    }

    private IEnumerator SpawnEnemyLine(int numOfEnemies, float[] posInLine, int line)
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[0]);
            enemy.transform.position = spawnPos[line].position;
            enemy.GetComponent<Follower>().pathCreator = pathCreator[line];
            enemy.GetComponent<Follower>().SetDestination(posInLine[numOfEnemies-i-1]);
            yield return new WaitForSeconds(1f);
        }
    }
}
