using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] bool deleteAllPlayerPrefs;

    private void Awake()
    {
        if (deleteAllPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
