using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y+speed*Time.deltaTime);
        transform.position = position;

        Vector2 top = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.y > top.y)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyTag") || (col.tag=="EnemyBulletTag") || (col.tag == "Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}
