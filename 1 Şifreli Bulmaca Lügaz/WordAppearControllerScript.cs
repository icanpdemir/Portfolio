using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class WordAppearControllerScript : MonoBehaviour
{
    [SerializeField] GameObject wordAppearUI;
    [SerializeField] TMP_Text revealTxt;

    SayingManager sayingManager;
    Dictionary<char, int> alphabetCounter, totalLetterOccurences;
    char[] saying;
    char[] alphabet = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ".ToCharArray();
    char hintLetter = '0';
    int hintCipherValue = -1;

    private void Start() {
        isHintActivated = false;
    }

    public static bool isHintActivated = false;

    public void setCounterData(Dictionary<char, int> data)
    {
        alphabetCounter = new Dictionary<char, int>(data);
    }

    public void setSayingData(string currentSaying)
    {
        sayingManager = FindObjectOfType<SayingManager>();
        saying = currentSaying.ToUpper(new CultureInfo("tr-TR", false)).ToCharArray();
        calculateKnownLetters();
        //displayContent();
        getHighestPossibleValue();
    }

    void calculateKnownLetters()
    {
        if (alphabet != null && saying != null)
        {
            totalLetterOccurences = new Dictionary<char, int>();

            for (int i = 0; i < alphabet.Length; i++)
            {
                totalLetterOccurences[alphabet[i]] = 0;
            }
            
            for (int i = 0; i < saying.Length; i++)
            {
                if (saying[i] != ' ' && saying[i] != '\0')
                {
                    totalLetterOccurences[saying[i]]++;
                }
            }
        }
    }

    void getHighestPossibleValue()
    {
        Dictionary<char, int> unknownLetters = new Dictionary<char, int>();
        int counter = 0;
        char maxValKey = '0';
        int maxVal = 0;

        foreach (var item in alphabetCounter)
        {
            if (item.Value == totalLetterOccurences[item.Key])
            {
                unknownLetters[item.Key] = totalLetterOccurences[item.Key];
                counter++;
            }
        }

        if (counter > 0)
        { //we found undiscovered char
            foreach (var item in unknownLetters)
            {
                if (item.Value > maxVal)
                {
                    maxValKey = item.Key;
                    maxVal = item.Value;
                }
            }
        }
        else
        { // there is no undiscovered character get the best one
            int tempRes;
            foreach (var item in alphabetCounter)
            {
                tempRes = totalLetterOccurences[item.Key] - item.Value;
                if (tempRes < maxVal)
                {
                    maxVal = tempRes;
                    maxValKey = item.Key;
                }
            }
        }

        hintLetter = maxValKey;
        hintCipherValue = sayingManager.getCipherValue(maxValKey);
        //Debug.Log("THE KEY <" + maxValKey + ">" + " AND VALUE IS: " + sayingManager.getCipherValue(maxValKey) + "");

    }

    public void revealCipherHint() // use this after rewarded ad is successfully watched 
    {
        
        if (hintLetter != '0' && hintCipherValue != -1)
        {
            revealTxt.text = hintLetter + "=" + hintCipherValue;
            wordAppearUI.SetActive(true);
            isHintActivated = true;
            Debug.Log("Hint printed!");
        }
    }

    
    void displayContent()
    {
        if (alphabetCounter != null)
        {
            string test = "Alphabet counter out: \n";
            foreach (var item in alphabetCounter)
            {
                test += "\n" + item.Key + " is : " + item.Value;
            }
            Debug.Log(test);
        }

        if (totalLetterOccurences != null)
        {
            string test2 = "Known letter counter out: \n";
            foreach (var item in totalLetterOccurences)
            {
                test2 += "\n" + item.Key + " is : " + item.Value;
            }
            Debug.Log(test2);
        }

    }


}
