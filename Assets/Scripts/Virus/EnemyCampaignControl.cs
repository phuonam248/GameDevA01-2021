using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCampaignControl : MonoBehaviour
{
    public enum EnemyType
    {
        whiteEnemy = 0,
        greenEnemy,
        blueEnemy,
        yellowEnemy,
        redEnemy,
        violetEnemy,
        orangeEnemy,
    };

    public enum EnemyMode{
        Campaign1,
        Campaign2,
        Survial
    }
    private EnemyMode enemyMode = EnemyMode.Campaign1;

    public GameObject explosion;
    public float speed;
    public EnemyType enemyType;
    public float dropRateCoin = 0.3f;
    List<GameObject> objects = new List<GameObject>();
    List<float> dropRate = new List<float>();
    public GameObject CoinItem;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
        objects.Add(CoinItem);
        dropRate.Add(dropRateCoin);
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 position = transform.position;
        // position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        // transform.position = position;


        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        if (min.y > transform.position.y) {
            Destroy(gameObject);
        }
    }

    public void StartFire()
    {
        switch(enemyType){
            case EnemyType.blueEnemy:
                gameObject.GetComponent<BlueVirusGun>().Fire();
                break;
            case EnemyType.greenEnemy:
                gameObject.GetComponent<GreenVirusGun>().Fire();
                break;
            case EnemyType.yellowEnemy:
                gameObject.GetComponent<YellowVirusGun>().Fire();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag")) {
            PlayExplosion();
            int index = Random.Range(0, objects.Count);
            if (Random.Range(0f, 1f) >= dropRate[index])
                Instantiate(objects[index], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void PlayExplosion() {
        GameObject anExplosion = (GameObject)Instantiate(explosion);
        anExplosion.transform.position = transform.position;
    }

    private void NoticeToCampaign1Controller()
    {
        GameObject campaign1Manager = GameObject
                    .FindGameObjectWithTag("Campaign1ManagerTag");
        campaign1Manager
            .GetComponent<CampaignControl>()
            .NoticeDestroyedEnemy();
    }
}
