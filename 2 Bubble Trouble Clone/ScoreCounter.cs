using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TMP_Text scoreTxt;
    int score = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("currentLevel", SceneManager.GetActiveScene().buildIndex);
        score = 0;
    }

    public void increaseScore()
    {
        score++;
        scoreTxt.text = score.ToString();
    }

    private void OnEnable()
    {
        BallController.onFruitSplitted += increaseScore;
    }

    private void OnDisable()
    {
        BallController.onFruitSplitted -= increaseScore;
    }
}
