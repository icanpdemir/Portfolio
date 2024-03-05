using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Key keyPrefab;
    [SerializeField] GameObject selectALetterFirstWarningObj;

    [Header(" Settings ")]
    [Range(0f, 1f)]
    [SerializeField] float widthPercent;
    [Range(0f, 1f)]
    [SerializeField] float heightPercent;
    [Range(0f, 5f)]
    [SerializeField] float bottomOffset;
    [Range(1f, 500f)]
    [SerializeField] float spaceBetweenLines;
    [Range(1f, 10f)]
    [SerializeField] float yPosition;

    [Header(" Keyboard Lines ")]
    [SerializeField] KeyboardLine[] lines;

    [Header(" Key Settings")]
    [Range(0f, 1f)]
    [SerializeField] float keyToLineRatio;
    [Range(0f, 1f)]
    [SerializeField] float keyXSpacing;

#nullable enable
    Letter? chosenLetter;
    KeyboardPreferences keyboardData;

    IEnumerator Start()
    {
        fixKeyboardIntoScreen();
        //changeParametersBasedOnResolution();
        createKeys();

        yield return null;

        updateRectTransform();
        placeKeys();
    }

    void Update()
    {
        keyboardDebug();
    }

    void keyboardDebug()
    {
        //changeParametersBasedOnResolution();
        updateRectTransform();
        placeKeys();
    }

    void updateRectTransform()
    {
        //Debug.Log("Screen width is: " + Screen.width);
        //Debug.Log("Keyboard width is: " + (widthPercent * Screen.width).ToString());
        float width = widthPercent * Screen.width;
        float height = heightPercent * Screen.height;

        rectTransform.sizeDelta = new Vector2(width, height);

        Vector2 position;

        position.x = Screen.width / 2;
        position.y = bottomOffset * Screen.height / 2;

        rectTransform.position = position;
    }

    void createKeys()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].keys.Length; j++)
            {
                char key = lines[i].keys[j];

                Key keyInstance = Instantiate(keyPrefab, rectTransform);
                keyInstance.setKey(key);

                keyInstance.getButton().onClick.AddListener(() => keyPressedCallback(key));
            }
        }
    }

    void placeKeys()
    {
        int lineCount = lines.Length;

        float lineHeight = rectTransform.rect.height / lineCount;

        float keyWidth = lineHeight * keyToLineRatio;
        float xSpacing = keyXSpacing * lineHeight - 25;

        int currentKeyIndex = 0;

        for (int i = 0; i < lineCount; i++)
        {
            float halfKeyCount = (float)lines[i].keys.Length / 2;

            float startX = rectTransform.position.x - (keyWidth + xSpacing) * halfKeyCount + (keyWidth + xSpacing) / 2;

            float lineY = rectTransform.position.y + rectTransform.rect.height / yPosition - lineHeight / 2 - i * spaceBetweenLines;

            for (int j = 0; j < lines[i].keys.Length; j++)
            {
                float keyX = startX + j * (keyWidth + xSpacing);

                Vector2 keyPosition = new Vector2(keyX, lineY);

                RectTransform keyRectTransform = rectTransform.GetChild(currentKeyIndex).GetComponent<RectTransform>();
                keyRectTransform.position = keyPosition;

                keyRectTransform.sizeDelta = new Vector2(keyWidth, keyWidth);
                currentKeyIndex++;
            }
        }
    }

    void keyPressedCallback(char key)
    {

        if (SelectLetterController.selectedLetterObject != null)
        {
            chosenLetter = SelectLetterController.selectedLetterObject.GetComponent<Letter>() ?? null;
            if(chosenLetter != null)
                chosenLetter.changeLetter(key);

        }
        else
        {
            selectALetterFirstWarningObj.SetActive(true);
            StartCoroutine(doSomethingAfterDelay());
        }

        //Debug.Log(key);
    }

    private IEnumerator doSomethingAfterDelay()
    {

        yield return new WaitForSeconds(1.0f);

        selectALetterFirstWarningObj.SetActive(false);

    }

    void fixKeyboardIntoScreen()
    {
        float width = Screen.width;
        float height = Screen.height;
        float ratio = height / width;
        Debug.Log("current ratio is: " + ratio);

        keyboardData.widthPercent = 0f; //
        keyboardData.heightPercent = 0f; //0.234  0.193  0.169  0.157  0.157  0.134
        keyboardData.bottomOffset = 0.4f; //
        keyboardData.spaceBetweenLines = 200f; // 45 70  120  130  170  207
        keyboardData.yPosition = 3f; //
        keyboardData.keyToLineRatio = 0.75f; //
        keyboardData.keyXSpacing = 0f; //

        initializeKeyboardData();
    }

    void initializeKeyboardData()
    {
        keyXSpacing = keyboardData.keyXSpacing;
        keyToLineRatio = keyboardData.keyToLineRatio;
        widthPercent = keyboardData.widthPercent;
        heightPercent = keyboardData.heightPercent;
        bottomOffset = keyboardData.bottomOffset;
        yPosition = keyboardData.yPosition;
        spaceBetweenLines = keyboardData.spaceBetweenLines;
    }

}

[System.Serializable]
public struct KeyboardLine
{
    public string keys;
}

public struct KeyboardPreferences
{
    public float keyXSpacing,
                keyToLineRatio,
                widthPercent,
                heightPercent,
                bottomOffset,
                yPosition,
                spaceBetweenLines;

}

