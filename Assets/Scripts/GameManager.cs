using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject gameOver;
    public Text LivesText;
    public Text InstructionText;

    public enum GameManagerState {
        Opening,
        Gameplay,
        Gameover,
    }

    GameManagerState GMState;
    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
        Invoke("ChangeToGameplay", 6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateGameManagerState()
    {
        switch(GMState) 
        {
            case GameManagerState.Opening:

                InstructionText.gameObject.SetActive(true);

                gameOver.SetActive(false);
              
                LivesText.gameObject.SetActive(false);

                Invoke("ChangeToGameplay", 6f);

                playerShip.SetActive(true);
                playerShip.GetComponent<Transform>().position = new Vector2(0,-3);

                break;
            case GameManagerState.Gameplay:
                
                InstructionText.gameObject.SetActive(false);
                
                LivesText.gameObject.SetActive(true);

                // set player ship active and init player lives
                playerShip.GetComponent<PlayerControl>().Init();

                // Start Enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();


                break;
            case GameManagerState.Gameover:
                //play game over sound
                gameObject.GetComponent<AudioSource>().Play();

                // Stop spawner
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                // Display Game Over UI
                gameOver.SetActive(true);
                // Set game manager state to Opening after 8 seconds
                Invoke("ChangeToOpening", 8f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state) 
    {
        GMState = state;
        UpdateGameManagerState();
    } 

    // Function to change Game manager state to opening
    void ChangeToOpening() 
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    void ChangeToGameplay() 
    {
        SetGameManagerState(GameManagerState.Gameplay);
    }
}
