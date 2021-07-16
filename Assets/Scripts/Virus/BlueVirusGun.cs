using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueVirusGun : MonoBehaviour
{
    public GameObject enemyBullet;

    public float delayFire = 1;
    // Start is called before the first frame update
    private Vector2 direction = new Vector2(0f, -1f);
    void Start()
    {
        Fire();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Fire()
    {
        float ranTime = Random.Range(2f, 3f);
        Invoke("FireByDirection", ranTime);
    }

    void FireByDirection()
    {
        GameObject bullet = (GameObject)Instantiate(enemyBullet);
        bullet.transform.position = transform.GetChild(0).transform.position;
        bullet.GetComponent<EnemyBullet>().setDirection(direction);
        float ranTime = Random.Range(2f, 3f);
        Invoke("FireByDirection", ranTime);
    }


}


