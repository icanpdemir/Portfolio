using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ProgressTracker : MonoBehaviour
{
    [Header(" Object References ")]
    [SerializeField] TMP_Text genreNameTxt;
    [SerializeField] TMP_Text levelNumberTxt;
    [SerializeField] GameObject lifePointTxt;
    [SerializeField] GameObject genreCompletedWindow;
    [SerializeField] TMP_Text genreCompletedText;
    [SerializeField] TMP_Text percentageTxt;
    [SerializeField] UIOperationsController uiOperationsController;
    [SerializeField] Interstitial interstitialAdController;

    SayingSO[] sayings;
    GenreDataHolderScript genreDataHolderScript;
    List<int> successPercentages = new List<int>();
    string currentGenreKey, currentGenreName;

    void Awake()
    {
        genreDataHolderScript = GenreDataHolderScript.getGenreDataHolderScript();
        currentGenreKey = PlayerPrefs.GetString("currentGenre", "Türk Atasözleri");
        sayings = genreDataHolderScript.getSayingData(currentGenreKey);
        currentGenreName = genreDataHolderScript.getGenreName(currentGenreKey);

    }


    public void prepareNextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt(currentGenreKey, 0);

        if (currentLevel + 1 < sayings.Length)
        {
            PlayerPrefs.SetInt(currentGenreKey, currentLevel + 1);
        }
        else
        {
            PlayerPrefs.SetInt(currentGenreKey, currentLevel + 1);
            genreCompletedWindow.SetActive(true);
            genreCompletedText.text = "Tebrikler \"" + currentGenreName + "\" bölümünü tamamladınız, yeni bölümler çok yakında!";
            uiOperationsController.setGenreCompletedSettings();
            uiOperationsController.changePauseButtonActivatedStatus(false);
            Debug.Log("WELL DONE! YOU HAVE COMPLETED ALL LEVELS IN THIS GENRE");
            //end of the levels
        }
    }

    public void loadNextLevel()
    {
        restartLevel();
    }

    public SayingSO getCurrentSaying()
    {
        if (isGenreCompleted()) // this genre is already completed so reset level count and start again
        {
            uiOperationsController.changePauseButtonActivatedStatus(false);
            uiOperationsController.changeKeyboardActivatedStatus(false);
            genreCompletedWindow.SetActive(true);
            return null;
        }
        else
        {
            return sayings[PlayerPrefs.GetInt(currentGenreKey, 0)];
        }

    }

    public void updateLevelIndicatorUI()
    {
        if(currentGenreName != "Türk Atasözleri")
            genreNameTxt.text = currentGenreName;
        else
            genreNameTxt.text = "Atasözleri\nve Deyimler";
        levelNumberTxt.text = " Bölüm " + (PlayerPrefs.GetInt(currentGenreKey, 0) + 1) + "";
        levelNumberTxt.gameObject.SetActive(true);
        lifePointTxt.SetActive(true);
    }

    public void restartLevel()
    {
        if (!isGenreCompleted())
        {
            interstitialAdController.increaseInterstitialAd();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            genreCompletedWindow.SetActive(true);
        }

    }

    public bool isGenreCompleted()
    {
        if (PlayerPrefs.GetInt(currentGenreKey, 0) < sayings.Length)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void calculateSuccessPercentage()
    {
        int numberOfLevels = sayings.Length;
        int percentage;
        string testPerc = "Percentage test: \n";

        for (int i = (numberOfLevels + 1); i >= 1; i--)
        {

            percentage = (i * 100) / numberOfLevels;

            if (i == numberOfLevels + 1 || percentage >= 100)
            {
                percentage = 99;
            }
            else if (i == 1)
            {
                percentage = 1;
            }

            successPercentages.Add(percentage);
            //testPerc += "\nHam base value is: " + baseValue;
            //testPerc += "\nIn iteration " + i + " base value is:" + ((i + 1) * baseValue).ToString() + " and rand val is: " + randomValue;
            testPerc += "\nLevel " + (i + 1) + " percentage is: %" + percentage + "\n";
        }
        Debug.Log(testPerc);

    }

    public void getSuccessPercentage()
    {
        calculateSuccessPercentage();
        int percentage = successPercentages[PlayerPrefs.GetInt(PlayerPrefs.GetString("currentGenre"), 0)];
        string addition = getAdditionByNumber(percentage);

        string finalTxt = "<b><size=52>Tebrikler!</size></b>\nOyuncuların sadece <b><size=52>%" + percentage
                        + "</size></b>" + addition + " bu bölümü başarıyla tamamlayabildi!";
        percentageTxt.text = finalTxt;
    }

    string getAdditionByNumber(int num)
    {
        int lastDigit = num % 10;
        string addition;
        if (lastDigit == 1 || lastDigit == 5 || lastDigit == 8 || num == 70 || num == 80)
        {
            addition = "i";
        }
        else if (lastDigit == 2 || lastDigit == 7 || num == 20 || num == 50)
        {
            addition = "si";
        }
        else if (lastDigit == 3 || lastDigit == 4)
        {
            addition = "ü";
        }
        else if (lastDigit == 6)
        {
            addition = "sı";
        }
        else if (lastDigit == 9 || num == 10 || num == 30)
        {
            addition = "u";
        }
        else
        {
            addition = "ı";
        }

        return "\'" + addition;
    }
}
