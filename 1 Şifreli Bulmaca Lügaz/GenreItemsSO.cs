using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GenreItemSO", fileName = " Genre")]
public class GenreItemsSO : ScriptableObject
{
    [Header(" Genre Properties ")]
    [SerializeField] string genreName; // also used for playerPrefs
    [SerializeField] Sprite genreImage;
    [SerializeField] Color32 iconColor;
    [SerializeField] string genreDifficulty;
    [SerializeField] SayingSO[] sayingsOfGenre;
    [SerializeField] bool isNewLevelsAdded = false;

    public string getGenreName()
    {
        if (genreName != null)
            return genreName;
        else
            return "";
    }

    public SayingSO[] getGenreSayings()
    {
        return sayingsOfGenre;
    }

    public Sprite getGenreLogo()
    {
        return genreImage;
    }

    public int getNumberOfLevels()
    {
        return sayingsOfGenre.Length;
    }

    public string getGenreDifficulty()
    {
        if (genreDifficulty != null)
            return genreDifficulty;
        else
            return "null";
    }

    public int getGenreCurrentProcess()
    {
        return PlayerPrefs.GetInt(genreName, 0);
    }

    public Color32 getGenreIconColor()
    {
        return iconColor;
    }

    public bool getNewLevelsAddedInfo()
    {
        return isNewLevelsAdded;
    }


}
