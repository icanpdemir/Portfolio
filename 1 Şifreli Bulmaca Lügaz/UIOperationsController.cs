using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class UIOperationsController : MonoBehaviour
{
    [Header(" UI Objects References ")]
    [SerializeField] GameObject pauseMenuObject;
    [SerializeField] GameObject pauseButtonObject;
    [SerializeField] GameObject keyboardObject;
    [SerializeField] TutorialV3 tutorialControllerObject;
    [SerializeField] GameObject areYouSureScreen;
    [SerializeField] GameObject hintWindow;
    [SerializeField] GameObject sayingObject;
    [SerializeField] GameObject watchAdButton;
    [SerializeField] GameObject areYouSureRewardedAdsWindow;
    [SerializeField] GameObject supportDeveloperWindow;

    [SerializeField] TMP_Text musicNameTxt;

    private void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {

            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                openPauseMenuObject();
            }
        }
    }

    public void openTutorialWindow()
    {
        tutorialControllerObject.startTutorial();
    }

    public void openAreYouSureWindow()
    {
        pauseMenuObject.SetActive(false);
        areYouSureScreen.SetActive(true);
    }

    public void closePauseMenuObject()
    {
        pauseMenuObject.SetActive(false);
        areYouSureScreen.SetActive(false);
    }

    public void changePauseButtonActivatedStatus(bool val)
    {
        pauseButtonObject.SetActive(val);
    }

    public void changeKeyboardActivatedStatus(bool val)
    {
        keyboardObject.SetActive(val);
    }

    public void openPauseMenuObject()
    {
        keyboardObject.SetActive(false);
        sayingObject.SetActive(false);
        pauseButtonObject.SetActive(false);
        watchAdButton.SetActive(false);
        hintWindow.SetActive(false);

        pauseMenuObject.SetActive(true);
    }

    public void setGenreCompletedSettings()
    {
        keyboardObject.SetActive(false);
        sayingObject.SetActive(false);
        watchAdButton.SetActive(false);
        pauseButtonObject.SetActive(false);
    }

    public void reActivateDisabledObjects()
    {
        keyboardObject.SetActive(true);
        sayingObject.SetActive(true);
        pauseButtonObject.SetActive(true);
        watchAdButton.SetActive(true);
        if (WordAppearControllerScript.isHintActivated)
        {
            hintWindow.SetActive(true);
        }
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void restartGenreData()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt(PlayerPrefs.GetString("currentGenre"), 0);
    }

    public void closeChosenWindows(GameObject[] uiObjects)
    {
        foreach (var uiObject in uiObjects)
        {
            uiObject.SetActive(false);
        }
    }

    public void openChosenWindows(GameObject[] uiObjects)
    {
        foreach (var uiObject in uiObjects)
        {
            uiObject.SetActive(true);
        }
    }

    public void openAreYouSureRewardedAdsWindows()
    {
        watchAdButton.SetActive(false);
        hintWindow.SetActive(false);
        pauseButtonObject.SetActive(false);
        keyboardObject.SetActive(false);
        sayingObject.SetActive(false);
        areYouSureRewardedAdsWindow.SetActive(true);
    }

    public void closeAreYouSureRewardedAdsWindows()
    {
        watchAdButton.SetActive(true);
        pauseButtonObject.SetActive(true);
        keyboardObject.SetActive(true);
        sayingObject.SetActive(true);
        areYouSureRewardedAdsWindow.SetActive(false);
        if (WordAppearControllerScript.isHintActivated)
        {
            hintWindow.SetActive(true);
        }

    }

    public void changeMusicTxt(string txt)
    {
        musicNameTxt.text = "Çalan: <b>" + txt + "</b>";
    }

    public void closeSupportDeveloperWindow(){
        supportDeveloperWindow.SetActive(false);
    }



}
