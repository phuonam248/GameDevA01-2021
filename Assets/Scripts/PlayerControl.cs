using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject gameManager; // reference to gamemanger object
    public GameObject playerBullet;
    public GameObject bulletPosition;
    public GameObject explosion;
    public float speed;

    // Reference to Lives UI text
    public Text LivesUIText;

    const int maxLives = 3;
    int lives;

    // Rotation
    public Vector2 mousePosition;
    public Rigidbody2D rb;

    private void SetRotation() {
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
        //transform.position = new Vector2(0,-3);

        // set player game object to active
        gameObject.SetActive(true);
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
        Vector2 direction = new Vector2(x,y).normalized;

        //Call the function that computes and set player's position
        Move(direction);
    }

    void Move(Vector2 direction) {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

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

    void OnTriggerEnter2D(Collider2D col) {
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || (col.tag == "AsteroidTag")) {
            PlayExplosion();

            lives--; 
            LivesUIText.text = "Lives: " + lives.ToString();

            if (lives == 0) {
                // update game manager state to game over
                gameManager.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);
                // hide player's ship when dead
                gameObject.SetActive(false);
            }
            //Destroy(gameObject);
        }
    }

    void PlayExplosion() {
        GameObject anExplosion = (GameObject)Instantiate(explosion);
        anExplosion.transform.position = transform.position;
    }

  
}
