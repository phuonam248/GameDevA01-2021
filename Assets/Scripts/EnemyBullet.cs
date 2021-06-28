using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed;
    Vector2 _direction;
    bool isReady;

    void Awake() {
        speed = 3f;
        isReady = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setDirection(Vector2 direction) {
        _direction = direction.normalized;
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReady) {
            // get bullet's current position
            Vector2 position = transform.position;

            // compute bullet's next position
            position += _direction * speed * Time.deltaTime;

            // update bullet position
            transform.position = position;

            // destroy bullet when it goes out the screen
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
            if ((position.x < min.x) || (position.x > max.x) ||
                (position.y < min.y) || (position.y > max.y)) {
                    Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerTag") || (col.tag == "PlayerBulletTag"))
        {
            Destroy(gameObject);
        }
    }



}
