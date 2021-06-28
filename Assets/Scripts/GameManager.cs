using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject playButton;
    public GameObject enemySpawner;
    public GameObject gameOver;
    public Text LivesText;

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
                // hide game over UI
                gameOver.SetActive(false);
                // hide score UI

                // hide Lives UI
                LivesText.gameObject.SetActive(false);
                // set play button to active
                playButton.SetActive(true);

                break;
            case GameManagerState.Gameplay:
                // hide play button
                playButton.SetActive(false);
                // display score UI

                // display Lives UI
                LivesText.gameObject.SetActive(true);

                // set player ship active and init player lives
                playerShip.GetComponent<PlayerControl>().Init();

                // Start Enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();


                break;
            case GameManagerState.Gameover:
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

    // when play button is clicked, call this function
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    // Function to change Game manager state to opening
    void ChangeToOpening() 
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
