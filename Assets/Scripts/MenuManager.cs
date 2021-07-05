using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject storeButton;
    public GameObject settingButton;
    public GameObject exitButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ClickPlayButton()
    {
        SceneManager.LoadScene("Survive");
    }

    public void ClickStoreButton()
    {
        return;
    }

    public void ClickSettingButton()
    {
        return;
    }



    public void ClickExitButton() {
        Application.Quit();
    }
}
