using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GenreMenuItem : MonoBehaviour
{
    [Header(" Object References ")]
    [SerializeField] TMP_Text genreTitle;
    [SerializeField] TMP_Text genreProcess;
    [SerializeField] TMP_Text genreDifficulty;
    [SerializeField] Image genreImage;
    [SerializeField] Image genreBGImage;
    [SerializeField] Slider genreProcessSlider;
    [SerializeField] Color32[] difficultyColors;
    [SerializeField] GameObject newLevelsAddedObject;

    string genreName;

    public void setGenreData(string currentGenreTitle, string currentGenreProcess,
                            Sprite currentGenreImage, int maxValue,
                            int currentValue, Color32 genreIconColor, string genreDifficultyVal, bool newLevelsAdded)
    {
        genreName = currentGenreTitle;
        if(genreName != "Türk Atasözleri")
            genreTitle.text = genreName;
        else
            genreTitle.text = "Atasözleri ve Deyimler";
        genreProcess.text = currentGenreProcess;
        genreImage.sprite = currentGenreImage;
        genreImage.color = genreIconColor;
        genreBGImage.sprite = currentGenreImage;
        genreDifficulty.text = genreDifficultyVal;
        genreDifficulty.color = setGenreDifficultyColor(genreDifficultyVal);
        setSliderData(maxValue, currentValue);
        newLevelsAddedObject.SetActive(newLevelsAdded);
    }

    void setSliderData(int maxValue, int currentValue)
    {
        genreProcessSlider.minValue = 0;
        genreProcessSlider.maxValue = maxValue;
        genreProcessSlider.value = currentValue;
    }

    Color32 setGenreDifficultyColor(string difficulty)
    {
        if (difficulty == "Başlangıç Seviyesi")
        {
            return difficultyColors[0];
        }
        else if (difficulty == "Orta Seviye")
        {
            return difficultyColors[1];
        }
        else if (difficulty == "Zor Seviye")
        {
            return difficultyColors[2];
        }
        else
        {
            return Color.black;
        }
    }

    public void loadGameWithCurrentGenre()
    {


        PlayerPrefs.SetString("currentGenre", genreName);
        SceneManager.LoadScene(1);

    }
}
