using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuComponentsSizeController : MonoBehaviour
{
    [SerializeField] GameObject mainTitleObject;
    [SerializeField] GameObject contentObject;
    [SerializeField] GameObject canvasObject;

    private void Start()
    {
        centerCardObjects();
    }

    void centerCardObjects()
    {
        HorizontalLayoutGroup horizontalLayoutGroup = contentObject.GetComponent<HorizontalLayoutGroup>();
        float width = canvasObject.GetComponent<RectTransform>().rect.width;
        float objectSize = contentObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().rect.width;
        int genreLeftRightPadding = (int)((width - objectSize) / 2);

        /*Debug.Log("Screen size is: " + width);
        Debug.Log("Size of objet: " + objectSize);
        Debug.Log("So, space should be: " + genreLeftRightPadding);*/

        horizontalLayoutGroup.padding.left = genreLeftRightPadding;
        horizontalLayoutGroup.padding.right = genreLeftRightPadding;
    }

    void scaleGenreCards()
    {
        HorizontalLayoutGroup horizontalLayoutGroup = contentObject.GetComponent<HorizontalLayoutGroup>();
        MenuScrollBarController menuScrollBarController = contentObject.GetComponent<MenuScrollBarController>();
        float width = Screen.width;
        float height = Screen.height;
        float genreCardScale = 1f;
        float genreCardSpacing = 0f;
        int genreLeftRightPadding = (int)((width - contentObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().rect.width) / 2);
        Debug.Log(genreLeftRightPadding);
        if (width <= 480)
        {
            genreCardSpacing = -300f;
            genreCardScale = 0.35f;
            //genreUpPadding = 50;
        }
        else if (width <= 640)
        {
            genreCardSpacing = -250f;
            genreCardScale = 0.5f;

        }
        else if (width <= 720)
        {
            genreCardSpacing = -150;
            genreCardScale = 0.6f;
            //genreUpPadding = 50;
        }
        else if (width <= 750)
        {
            genreCardSpacing = -125f;
            genreCardScale = 0.7f;
        }
        else if (width <= 828)
        {
            genreCardSpacing = -25f;
            genreCardScale = 0.8f;
        }
        else if (width <= 1080)
        {
            genreCardSpacing = 10f;
            genreCardScale = 0.9f;
        }
        else if (width <= 1125)
        {
            genreCardSpacing = 25f;
            genreCardScale = 1f;
        }
        else if (width <= 1170)
        {
            genreCardSpacing = 50f;
            genreCardScale = 1f;
        }
        else if (width <= 1242)
        {
            genreCardSpacing = 100f;
            genreCardScale = 1.1f;
        }
        else if (width <= 1242)
        {
            genreCardSpacing = 125f;
            genreCardScale = 1.2f;
        }
        else if (width <= 1242)
        {
            genreCardSpacing = 125f;
            genreCardScale = 1.2f;
        }

        horizontalLayoutGroup.padding.left = genreLeftRightPadding;
        horizontalLayoutGroup.padding.right = genreLeftRightPadding;
        horizontalLayoutGroup.spacing = genreCardSpacing;
        menuScrollBarController.setScaleRatio(genreCardScale);
    }

    void scaleTitleSize()
    {
        RectTransform rT = mainTitleObject.GetComponent<RectTransform>();
        float height = Screen.height;
        float titleScaleRatio = 1f;
        float yVal = 0f;

        if (height <= 850)
        {
            titleScaleRatio = 0.5f;
        }
        else if (height <= 1500)
        {
            titleScaleRatio = 0.75f;
        }
        else if (height <= 1800)
        {
            yVal = -100;
        }
        else if (height <= 1940)
        {
            titleScaleRatio = 1.5f;
        }
        else if (height <= 2350)
        {
            titleScaleRatio = 1.5f;
            yVal = -100;
        }
        else
        {
            titleScaleRatio = 1.5f;
            yVal = -100;
        }

        rT.localScale = Vector3.one * titleScaleRatio;
        Vector3 dedicatedPosition = new Vector3(0f, yVal, 0f);
        rT.anchoredPosition = dedicatedPosition;

    }
}
