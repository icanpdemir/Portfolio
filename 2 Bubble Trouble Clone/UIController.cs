using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text levelTxt;
    [SerializeField] GameObject winUIComponents;

    public static UIController Instance { get; private set; }

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
    }

    private void Start()
    {
        if(levelTxt != null)
            levelTxt.text = "Level: " + PlayerPrefs.GetInt("currentLevel", 1).ToString();
    }

    public void startGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("currentLevel", 1));
    }

    public void activateWinUI()
    {
        winUIComponents?.SetActive(true);
    }


}
