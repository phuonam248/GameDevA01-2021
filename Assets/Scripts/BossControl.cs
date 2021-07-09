using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    public GameObject BossFight;
    public GameObject Explosion;
    public GameObject HealthBar;
    public SpriteRenderer spriteRenderer;
    public Sprite LaughFace;
    public Sprite AngryFace;
    public Sprite CryFace;

    int health;
    float speed;
    string direction = "l";
    Vector2 startingPosition = new Vector2(0f, 2.1f);


    int phase2Health = 75;
    int phase3Health = 30;

    public void Init() {
        
        health = 100;
        HealthBar.GetComponent<Transform>().localScale = new Vector2(1f,1f);
    }

    void Start()
    {
        speed = 2f;
        //HealthBar.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(BossFight.GetComponent<BossFight>().Phase)
        {
            case global::BossFight.BossFightPhase.WaitingToStart:
                MoveToStartingPosition();
                if (transform.position.y <= startingPosition.y){
                    BossFight.GetComponent<BossFight>().ChangeToPhaseOne();
                }
                break;

            case global::BossFight.BossFightPhase.Phase_1:
                MoveHorizontal();
                break;

            case global::BossFight.BossFightPhase.Phase_2:
                MoveHorizontal();
                break;

            case global::BossFight.BossFightPhase.Phase_3:
                MoveHorizontal();
                break;
        }
       
    }

    // When the game starts, boss will move from ouside the screen to the starting position
    // In this time, boss receives no damage
    void MoveToStartingPosition() {
        if (transform.position.y >= startingPosition.y) {
            Vector2 position = new Vector2 (transform.position.x, transform.position.y - speed*Time.deltaTime);
            transform.position = position;
        }
    }


    // In Phase 1, the boss only moves horizontally
    void MoveHorizontal() {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        max.x = max.x - 2f;
        min.x = min.x + 2f;

        Vector2 pos = transform.position;
        
        if (direction == "l") {
            pos.x -= speed*Time.deltaTime;
            if (pos.x <= min.x)
                direction = "r";
        }
        else if (direction == "r") {
            pos.x += speed*Time.deltaTime;
            if (pos.x >= max.x)
                direction = "l";
        }

        transform.position = pos;
    }




    IEnumerator OnTriggerEnter2D(Collider2D col) {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag")) {
            if (health > 0)
            {
                health--; 
                HealthBarDecrease();
                if (health == phase2Health) {
                    spriteRenderer.sprite = AngryFace;
                    BossFight.GetComponent<BossFight>().ChangeToPhaseTwo();
                }
                else if (health == phase3Health) {
                    spriteRenderer.sprite = CryFace;
                    BossFight.GetComponent<BossFight>().ChangeToPhaseThree();
                }

                else if (health == 25) {

                }

                if (health == 0) {
                   PlayExplosion();
                   Destroy(gameObject);
                   BossFight.GetComponent<BossFight>().ChangeToVictory();
                }
            }

            gameObject.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(.2f);
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            
        }
    }

    void HealthBarDecrease() {
        Vector2 HP =  HealthBar.GetComponent<Transform>().localScale;
        if (HP.x > 0f) {
            float lossHP = 0.01f;
            if ((HP.x - lossHP) <= 0f)
                HP = new Vector2 (0f, HP.y);
            else
                HP = new Vector2 (HP.x - lossHP, HP.y);
            HealthBar.GetComponent<Transform>().localScale = HP;
        }
    }

    void PlayExplosion() {
        GameObject anExplosion = (GameObject)Instantiate(Explosion);
        anExplosion.transform.position = transform.position;
    }
    

}
