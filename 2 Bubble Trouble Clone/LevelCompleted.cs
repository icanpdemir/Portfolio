using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public static LevelCompleted Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        Time.timeScale = 1f;
    }

    public void levelCompleted(){
        //Win related stuffs here
        Time.timeScale = 0f;
        PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel", 1) + 1);
        UIController.Instance.activateWinUI();
    }
}
