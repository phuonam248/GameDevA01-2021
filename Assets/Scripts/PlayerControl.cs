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
    public Transform canvas;
    public Image heart;
    

    List<Image> heartList = new List<Image>();

    public float speed;

    const int maxLives = 5;
    int startLives = 3;
    int lives;
    
    int gameMode;

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
        lives = startLives;
        // update player's ship's health (heart)
        RenderHeart();

    }

    
    void Start()
    {
        gameMode = InGameSetting.GameMode;
       
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

    IEnumerator OnTriggerEnter2D(Collider2D col) {
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || 
            (col.tag == "AsteroidTag")  || (col.tag == "BossTag")        ||
            (col.tag == "YellowBulletTag")) 
        {
            if (lives > 0)
            {
                lives--; 
                Destroy(heartList[lives].gameObject);

                if (lives == 0) {
                    PlayExplosion();
                    // update game manager state to game over
                    if (gameMode == 0) {
                        gameManager.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);
                    }
                    else if (gameMode == 2) {
                        gameManager.GetComponent<BossFight>().ChangeToGameover();
                    }
                    
                    
                    // hide player's ship when dead
                    gameObject.SetActive(false);
                }

                gameObject.GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSeconds(.2f);
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
            
        }
    }

    void PlayExplosion() {
        GameObject anExplosion = (GameObject)Instantiate(explosion);
        anExplosion.transform.position = transform.position;
    }

    void RenderHeart() {
        heartList = new List<Image>();
        for (int i = 0; i < lives; i++) {
            Image newHeart = (Image)Instantiate(heart);
            newHeart.transform.SetParent(canvas, false);
            Vector2 postion = newHeart.transform.position;
            postion.x += (40*i); // 3 hearts cannot be displayed in the same position, right?
            newHeart.transform.position = postion; // update heart's position
            heartList.Add(newHeart);
        }
    }

  
}
