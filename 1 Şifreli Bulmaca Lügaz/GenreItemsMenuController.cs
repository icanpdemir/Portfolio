using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenreItemsMenuController : MonoBehaviour
{
    [Header(" Object References ")]
    [SerializeField] GameObject contentParentObject;
    [SerializeField] GameObject genreItemPrefab;

    GenreItemsSO[] genreItems;
    GenreMenuItem currentMenuItem;
    GenreDataHolderScript genreDataHolderScript;

    private void Start()
    {
        genreDataHolderScript = FindObjectOfType<GenreDataHolderScript>();
        genreItems = genreDataHolderScript.getGenreItems();

        PlayerPrefs.SetInt("menuExposeCounter", PlayerPrefs.GetInt("menuExposeCounter", 1) + 1);

        foreach (var genreItem in genreItems)
        {
            string genreName, processContent, genreDifficulty;
            int currentProcess, numberOfLevels, processPercentage;
            Color32 genreIconColor;
            bool levelsAdded;
            currentMenuItem = Instantiate(genreItemPrefab, contentParentObject.transform).GetComponentInChildren<GenreMenuItem>();
            currentProcess = genreItem.getGenreCurrentProcess();
            numberOfLevels = genreItem.getNumberOfLevels();
            genreDifficulty = genreItem.getGenreDifficulty();
            genreName = genreItem.getGenreName();
            genreIconColor = genreItem.getGenreIconColor();
            if (PlayerPrefs.GetInt("menuExposeCounter", 1) < 6)
            {
                levelsAdded = genreItem.getNewLevelsAddedInfo();
            }
            else
            {
                levelsAdded = false;
            }

            if (currentProcess == numberOfLevels)
            {
                processContent = "Tamamlandı";
            }
            else
            {
                processPercentage = (currentProcess * 100) / numberOfLevels;
                processContent = "%" + processPercentage.ToString();
            }

            currentMenuItem.setGenreData(genreName, processContent, genreItem.getGenreLogo(),
                                        numberOfLevels, currentProcess, genreIconColor,
                                        genreDifficulty, levelsAdded);
        }
    }


}
