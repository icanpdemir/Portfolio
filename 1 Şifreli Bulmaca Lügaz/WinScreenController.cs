using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenController : MonoBehaviour
{
    [Header(" Object References ")]
    [SerializeField] GameObject winScreenObject;
    [SerializeField] TMP_Text sayingTxt;
    [SerializeField] TMP_Text authorTxt;
    [SerializeField] GameObject sayingObject;
    [SerializeField] GameObject keyboardObject;
    [SerializeField] GameObject helpButtonObject;
    [SerializeField] GameObject pauseButtonObject;
    [SerializeField] GameObject hintObject;

    public void levelCompleted(string saying, string author, bool isGenreCompleted)
    {
        sayingTxt.text = "\"" + saying + "\"";
        authorTxt.text = "- " + author;
        sayingObject.SetActive(false);
        keyboardObject.SetActive(false);
        helpButtonObject.SetActive(false);
        hintObject.SetActive(false);
        pauseButtonObject.SetActive(false);
        if (!isGenreCompleted)
            winScreenObject.SetActive(true);
    }

}
