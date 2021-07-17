using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public GameObject explosion;
    GameObject scoreText;
    public float speed;
    int health;
    public float dropRateCoin = 0.3f;
    List<GameObject> objects = new List<GameObject>();
    List<float> dropRate = new List<float>();
    public GameObject CoinItem;
    // Start is called before the first frame update
    void Start()
    {
        health = 2;
        scoreText = GameObject.FindGameObjectWithTag("ScoreTag");
        objects.Add(CoinItem);
        dropRate.Add(dropRateCoin);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        if (min.y > transform.position.y) {
            Destroy(gameObject);
        }
    }

    IEnumerator  OnTriggerEnter2D(Collider2D col) {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag")) {
            health--;
           
            if (health == 0) {
                // scoreText.GetComponent<GameScore>().Score += 200;
                PlayExplosion();
                int index = Random.Range(0, objects.Count);
                if (Random.Range(0f, 1f) >= dropRate[index])
                    Instantiate(objects[index], transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            gameObject.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(.2f);
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            
        }
    }

    void PlayExplosion() {
        GameObject anExplosion = (GameObject)Instantiate(explosion);
        anExplosion.transform.position = transform.position;
    }
}
