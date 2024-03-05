using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] GameObject creditsWindow;
    [SerializeField] GameObject creditsButton;
    [SerializeField] GameObject genreCardsObject;

    public void openCreditWindow()
    {
        creditsWindow.SetActive(true);
        genreCardsObject.SetActive(false);
        creditsButton.SetActive(false);
    }

    public void closeCreditWindow()
    {
        creditsWindow.SetActive(false);
        genreCardsObject.SetActive(true);
        creditsButton.SetActive(true);
    }
}
