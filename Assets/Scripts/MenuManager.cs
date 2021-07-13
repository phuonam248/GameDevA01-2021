using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public GameObject compaignButton;
    public GameObject compaignOneButton;
    public GameObject compaignTwoButton;
    public GameObject surviveButton;
    public GameObject storeButton;
    public GameObject settingButton;
    public GameObject exitButton;
    public GameObject backButton;
    public GameObject BgMusicToggle;
    public GameObject SoundEffectToggle;
    public Text CoinText;

    // Start is called before the first frame update
    void Start()
    {
        BgMusicToggle.GetComponent<Toggle>().isOn = InGameSetting.BackgroundMusic;
        SoundEffectToggle.GetComponent<Toggle>().isOn = InGameSetting.SoundEffect;
        CoinText.text = GameStateManager.Instance.Coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ClickSurviveButton()
    {   
        InGameSetting.GameMode = 0;
        SceneManager.LoadScene("Survive");
    }

    public void ClickCampaignButton()
    {
        compaignButton.SetActive(false);
        surviveButton.SetActive(false);
        settingButton.SetActive(false);
        storeButton.SetActive(false);
        exitButton.SetActive(false);

        compaignOneButton.SetActive(true);
        compaignTwoButton.SetActive(true);
        backButton.SetActive(true);
    }

    public void ClickCampaignTwoButton() {
        InGameSetting.GameMode = 2;
        SceneManager.LoadScene("CampaignTwo");
    }


    public void ClickBackButton()
    {
        compaignButton.SetActive(true);
        surviveButton.SetActive(true);
        settingButton.SetActive(true);
        storeButton.SetActive(true);
        exitButton.SetActive(true);

        compaignOneButton.SetActive(false);
        compaignTwoButton.SetActive(false);
        BgMusicToggle.SetActive(false);
        SoundEffectToggle.SetActive(false);        
        backButton.SetActive(false);

    }

    public void ClickStoreButton()
    {
        SceneManager.LoadScene("Store");
    }

    public void ClickSettingButton()
    {
        compaignButton.SetActive(false);
        surviveButton.SetActive(false);
        settingButton.SetActive(false);
        storeButton.SetActive(false);
        exitButton.SetActive(false);

        BgMusicToggle.SetActive(true);
        SoundEffectToggle.SetActive(true);
        backButton.SetActive(true);
    }



    

    public void BgMusicToggleOn() 
    {
        InGameSetting.BackgroundMusic = BgMusicToggle.GetComponent<Toggle>().isOn;
        if (BgMusicToggle.GetComponent<Toggle>().isOn) 
        {
            
            GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().UnPause();
        }
            
        else 
        {
            GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Pause();
            
        }
            
    }

    public void SoundEffectToggleOn() 
    {
        InGameSetting.SoundEffect = SoundEffectToggle.GetComponent<Toggle>().isOn;
    }

    public void ClickExitButton() 
    {
        Application.Quit();
    }
}
