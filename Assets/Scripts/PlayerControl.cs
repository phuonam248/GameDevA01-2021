using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject gameManager; // reference to gamemanger object

    // BULLET PLAYER CONTROL 
    public GameObject playerBullet;
    public GameObject bulletPosition;
    public GameObject explosion;
    public string playerBulletName;
    private int playerBulletLevel = 1;
    private int bulletMaxlevel = 6;
    public GameObject lvlUpWeapon;
    private PlayerShield shield;
    // SPEED CONTROL
    public float baseSpeed = 4;
    private float speed = 4;
    private float speedBoostTime; // speedBoostTime of player
    private readonly float speedBoostTimeUnit = 4f; // each speedBoostItem will create a shield in 4 second.
    private bool isSpeedBoosting; // true if player is in speedboostingTime
    private float currentSpeedBoostTime;


    // Reference to Lives UI text
    public Text LivesUIText;

    const int maxLives = 3;
    int lives;

    // Rotation
    public Vector2 mousePosition;
    public Rigidbody2D rb;

    private void SetRotation()
    {
        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public void Init()
    {
        lives = maxLives;

        // update lives UI text
        LivesUIText.text = "Lives: " + lives.ToString();

        // set player ship to the center of the screen
        transform.position = new Vector2(0, 0);
        InitSpeedBoost();
        shield = gameObject.GetComponent<PlayerShield>();
        shield.ActivateShield = true;

        // set player game object to active
        gameObject.SetActive(true);
    }

    private void InitSpeedBoost()
    {
        isSpeedBoosting = false;
        speedBoostTime = 0f;
        currentSpeedBoostTime = 0f;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetRotation();

        float x = Input.GetAxisRaw("Horizontal"); //the value will be -1,0,1 for left, no input, right
        float y = Input.GetAxisRaw("Vertical");  //the value will be -1,0,1 for down, no input, up

        //based on the input, we compute a direction vector, and normalize it to get a unit vector
        Vector2 direction = new Vector2(x, y).normalized;
        if (isSpeedBoosting)
        {
            UpdateSpeedBoosting();
        }

        //Call the function that computes and set player's position
        Move(direction);
    }

    private void UpdateSpeedBoosting()
    {
        currentSpeedBoostTime += Time.deltaTime;

        if (currentSpeedBoostTime > speedBoostTime)
        {
            isSpeedBoosting = false;
            currentSpeedBoostTime = 0f;
            speedBoostTime = 0f;
            speed = baseSpeed;
        }
    }


    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.285f;
        min.x = min.x + 0.285f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        // Get the player's current position
        Vector2 pos = transform.position;

        // Calculate the new position
        pos += direction * speed * Time.deltaTime;

        // Make sure the new position is not outside the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // Update player position
        transform.position = pos;

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (!shield.ActivateShield && (col.tag == "EnemyShipTag" || col.tag == "EnemyBulletTag" || col.tag == "AsteroidTag"))
        {
            PlayExplosion();

            lives--;
            LivesUIText.text = "Lives: " + lives.ToString();

            if (lives == 0)
            {
                // update game manager state to game over
                gameManager.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);
                // hide player's ship when dead
                gameObject.SetActive(false);
            }
            //Destroy(gameObject);
        }
        else if (col.tag == "LevelUpWeaponTag")
        {
            string orbBulletName = col.gameObject.GetComponent<OrbControl>().bulletName;
            if (orbBulletName != playerBulletName)
            {
                ResetBullet(orbBulletName, col.gameObject);
            }
            else
            {
                LevelUpBullet(col.gameObject);
            }
            PlayLevelUpWeapon();
        }
        else if (col.tag == "HealthTag")
        {
            lives++;
            LivesUIText.text = "Lives: " + lives.ToString();
        }
        else if (col.tag == "SpeedTag")
        {
            if (!isSpeedBoosting)
            {
                isSpeedBoosting = true;
            }
            speed += speedBoostTimeUnit;
            speedBoostTime += speedBoostTimeUnit;

        }
        else if (col.tag == "ShieldTag")
        {
            shield.ActivateShield = true;
        }
        else if (col.tag == "HealTag"){
            
        }
    }


    private void LevelUpBullet(GameObject go)
    {
        if (playerBulletLevel == bulletMaxlevel)
            return;
        playerBulletLevel += 1;
        List<GameObject> bulletPrefabs = go.GetComponent<OrbControl>().bulletPrefabs;
        if (bulletPrefabs[playerBulletLevel - 1] != playerBullet)
        {
            Debug.Log("differr");
        }
        playerBullet = bulletPrefabs[playerBulletLevel - 1];
    }

    private void ResetBullet(string orbBulletName, GameObject go)
    {
        List<GameObject> bulletPrefabs = go.GetComponent<OrbControl>().bulletPrefabs;
        bulletMaxlevel = bulletPrefabs.Count;
        playerBulletLevel = 1;
        playerBulletName = orbBulletName;
        playerBullet = bulletPrefabs[playerBulletLevel - 1];
    }

    void PlayExplosion()
    {
        GameObject anExplosion = (GameObject)Instantiate(explosion);
        anExplosion.transform.position = transform.position;
    }

    void PlayLevelUpWeapon()
    {
        GameObject anEffect = (GameObject)Instantiate(lvlUpWeapon);
        anEffect.transform.position = transform.position;
        Destroy(anEffect, 0.5f);
    }


}
