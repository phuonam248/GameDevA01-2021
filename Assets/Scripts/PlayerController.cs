using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerBullet;
    public GameObject BulletPos1;
    public GameObject BulletPos2;
    public GameObject Explosion;
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject bullet1 = (GameObject)Instantiate(PlayerBullet);
            GameObject bullet2 = (GameObject)Instantiate(PlayerBullet);
            bullet1.transform.position = BulletPos1.transform.position;
            bullet2.transform.position = BulletPos2.transform.position;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 bot = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 top = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        top.x = top.x - 0.225f;
        top.y = top.y - 0.285f;
        bot.x = bot.x + 0.225f;
        bot.y = bot.y + 0.285f;
        Vector2 position = transform.position;
        position += direction * speed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, bot.x, top.x);
        position.y = Mathf.Clamp(position.y, bot.y, top.y);
        transform.position = position;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag=="EnemyTag") || (col.tag=="EnemyBulletTag") || (col.tag == "Asteroid"))
        {
            ExplosionEvent();
            Destroy(gameObject);
        }
    }
    void ExplosionEvent()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
}
