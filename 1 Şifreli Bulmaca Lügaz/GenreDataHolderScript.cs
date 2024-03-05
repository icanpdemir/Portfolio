using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenreDataHolderScript : MonoBehaviour
{
    [Header(" Current Genre Items ")]
    [SerializeField] GenreItemsSO[] items;
    Dictionary<string, GenreItemsSO> genreItems = new Dictionary<string, GenreItemsSO>();

    private static GenreDataHolderScript instance;

    private void Awake()
    {
        //for test
        //PlayerPrefs.SetInt("Para Birimleri", 23);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (var item in items)
        {
            genreItems[item.getGenreName()] = item;
        }

    }

    public static GenreDataHolderScript getGenreDataHolderScript()
    {
        return instance;
    }

    public GenreItemsSO[] getGenreItems()
    {
        return items;
    }

    public SayingSO[] getSayingData(string requestedGenre)
    {
        return genreItems[requestedGenre].getGenreSayings();
    }

    public string getGenreName(string requestedGenre)
    {
        Debug.Log("Chosen genre is: " + genreItems[requestedGenre].getGenreName());
        return genreItems[requestedGenre].getGenreName();
    }
}
