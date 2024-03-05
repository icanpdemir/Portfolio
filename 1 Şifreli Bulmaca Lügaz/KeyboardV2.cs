using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardV2 : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] RectTransform[] rectTransform;
    [SerializeField] Key keyPrefab;
    [SerializeField] GameObject selectALetterFirstWarningObj;
    [SerializeField] int numberOfKeysInKeyboard = 32;

    [Header(" Keyboard Lines ")]
    [SerializeField] KeyboardLine[] lines;

    Key[] keyObjects;

#nullable enable
    Letter? chosenLetter;
    KeyboardPreferences keyboardData;

    IEnumerator Start()
    {
        //createKeys();

        yield return null;

    }

    void keyboardDebug()
    {
        //changeParametersBasedOnResolution();
    }

    public void createKeys(Dictionary<char, int> keyboardArr)
    {
        Dictionary<char, int> updatedKeyboardArr = new Dictionary<char, int>();
        Dictionary<char, bool> mostFrequentWords = new Dictionary<char, bool>
        {
            { 'A', true },
            { 'E', true },
            { 'K', true },
            { 'İ', true },
            { 'I', true },
            { 'M', true },
            { 'R', true },
            { 'N', true },
            { 'T', true },
            { 'U', true },
            { 'Z', true },
            { 'C', true },
            { 'P', true },
            { 'O', true }
        };

        foreach (var item in mostFrequentWords)
        {
            if (!keyboardArr.Keys.Contains(item.Key))
            {
                keyboardArr[item.Key] = 1;
            }
        }

        updatedKeyboardArr = keyboardArr.OrderBy(pair => pair.Key)
                                        .ToDictionary(pair => pair.Key, pair => pair.Value);


        keyObjects = new Key[numberOfKeysInKeyboard];

        int m = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].keys.Length; j++)
            {
                char key = lines[i].keys[j];

                Key keyInstance = Instantiate(keyPrefab, rectTransform[i]);
                keyInstance.setKey(key);
                if (!updatedKeyboardArr.Keys.Contains(key))
                {
                    keyInstance.deactivateKeyObject();
                }
                keyInstance.getButton().onClick.AddListener(() => keyPressedCallback(key));
                keyObjects[m] = keyInstance;
                m++;
            }
        }

    }

    void setActivatedKeysData()
    {

    }

    public void inactivateCompletedLetter(char chosenKey)
    {
        foreach (var item in keyObjects)
        {
            if (item.getKey() == chosenKey)
            {
                item.deactivateKeyObject();
            }
        }
    }

    void keyPressedCallback(char key)
    {

        if (SelectLetterController.selectedLetterObject != null)
        {
            chosenLetter = SelectLetterController.selectedLetterObject.GetComponent<Letter>() ?? null;
            if (chosenLetter != null && !SelectLetterController.getIsLevelCompleted())
                chosenLetter.changeLetter(key);

        }
        else
        {
            selectALetterFirstWarningObj.SetActive(true);
            SoundEffectManager.playWrongAnswerSFX();
            StartCoroutine(doSomethingAfterDelay());
        }

        //Debug.Log(key);
    }

    private IEnumerator doSomethingAfterDelay()
    {

        yield return new WaitForSeconds(0.25f);

        selectALetterFirstWarningObj.SetActive(false);

    }

    public int calculateLineLength(GridLayoutGroup gridLayout)
    {
        float cellXValue = gridLayout.cellSize.x;
        float xSpacingValue = gridLayout.spacing.x;
        float gridLayoutWidth = gridLayout.GetComponent<RectTransform>().rect.width;
        int lineLengthTemp;

        lineLengthTemp = (int)((gridLayoutWidth + xSpacingValue) / (cellXValue + xSpacingValue));

        /*Debug.Log("Cell x value: " + cellXValue);
        Debug.Log("Cell x spacing value: " + xSpacingValue);
        Debug.Log("Rect width : " + gridLayoutWidth);
        Debug.Log("Length: " + lineLengthTemp);*/

        return lineLengthTemp;

    }
}

/*[System.Serializable]
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

}*/

