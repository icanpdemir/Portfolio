using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SayingManager : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] GameObject letterPrefab, spacePrefab;
    [SerializeField] TutorialV3 tutorialController;
    [SerializeField] KeyboardV2 keyboard;
    [SerializeField] int lineLength = 13;

    NumericalValueController numericalValueController;
    ProgressTracker progressTracker;
    WinScreenController winScreenController;

    string originalSaying;
    string authorOfSaying;
    SayingSO currentSayingSO;
    char[] eliminatedSaying;
    char[] alphabet;
    int[] cipherValues, randomIndexes;
    public static bool isLevelCompletedCheck = true;
    public static bool isSayingPrinted = false;

    Dictionary<char, int> cipher = new Dictionary<char, int>();
    Dictionary<char, int> alphabetCounter = new Dictionary<char, int>();
    Dictionary<char, int> keyboardArr = new Dictionary<char, int>();


    void Start()
    {
        StartCoroutine(checkTutorialState());
        isSayingPrinted = false;

        initializeControllers();
        initializeValues();
        if(currentSayingSO != null)
            printSayingToScreen();
    }

    IEnumerator checkTutorialState()
    {
        yield return 0.01f;

        if (PlayerPrefs.GetString("isTutorialLevel", "true") == "true")
        {
            tutorialController.startTutorial();
        }

    }

    void initializeControllers()
    {
        progressTracker = FindObjectOfType<ProgressTracker>();
        numericalValueController = FindObjectOfType<NumericalValueController>();
        winScreenController = FindObjectOfType<WinScreenController>();
    }

    void initializeValues()
    {
        currentSayingSO = progressTracker.getCurrentSaying();
        if (progressTracker.getCurrentSaying() != null)
        {
            originalSaying = currentSayingSO.getSaying();
            authorOfSaying = currentSayingSO.getAuthorName();
            originalSaying = eliminateExtraSpaces(originalSaying);
        }
    }

    void printSayingToScreen()
    {
        Letter currentLetterScript;
        int charCounterInLine = 0;
        int letterIdCounter = 0;
        prepareCipher();
        eliminateLettersForPuzzle();
        progressTracker.updateLevelIndicatorUI();
        SelectLetterController.clearLetterObjectsList();
        displayArrayContents();
        for (int i = 0; i < eliminatedSaying.Length; i++)
        {
            if (eliminatedSaying[i] == '+') // new line till line end
            {
                int numOfRequiredBlanks = lineLength - charCounterInLine;
                charCounterInLine = 0;

                for (int k = 0; k < numOfRequiredBlanks; k++)
                {
                    Instantiate(spacePrefab, gridLayout.transform);
                }
            }
            else if (eliminatedSaying[i] == '0') // empty letter cell
            {
                currentLetterScript = Instantiate(letterPrefab, gridLayout.transform).GetComponentInChildren<Letter>();
                instantiateBlankLetterObject(currentLetterScript, i, letterIdCounter);
                letterIdCounter++;
                alphabetCounter[originalSaying[i]]++;
                numericalValueController.addLetterObject(currentLetterScript);
                charCounterInLine++;
            }
            else if (eliminatedSaying[i] != ' ') // filled letter cell
            {
                currentLetterScript = Instantiate(letterPrefab, gridLayout.transform).GetComponentInChildren<Letter>();
                instantiateFilledLetterObject(currentLetterScript, i, letterIdCounter);
                letterIdCounter++;
                numericalValueController.addLetterObject(currentLetterScript);
                charCounterInLine++;
            }
            else // space
            {
                Instantiate(spacePrefab, gridLayout.transform);
                charCounterInLine++;
            }


        }
        foreach (var item in alphabetCounter)
        {
            if (item.Value == 0)
            {
                numericalValueController.removeNumericalValuesOfLetter(item.Key.ToString());
            }
        }


        isSayingPrinted = true;

        keyboard.createKeys(getKeyboardLetters());
    }

    Dictionary<char, int> getKeyboardLetters()
    {

        keyboardArr = new Dictionary<char, int>();

        foreach (var item in alphabetCounter)
        {
            if (item.Value > 0)
            {
                keyboardArr[item.Key] = item.Value;
            }
        }

        return keyboardArr;
    }

    int[] calculateWordLengths(string str)
    {
        string[] words = str.Split(' ');
        int[] lengths = new int[words.Length];

        for (int j = 0; j < words.Length; j++)
        {
            lengths[j] = words[j].Length;
        }

        return lengths;
    }

    void prepareCipher()
    {
        cipherValues = new int[29];
        alphabet = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ".ToCharArray();

        for (int i = 0; i < 29; i++)
        {
            cipherValues[i] = i;
        }

        for (int i = 0; i < 29; i++)
        {
            alphabetCounter[alphabet[i]] = 0;
        }

        shuffleCipher();

        for (int i = 0; i < 29; i++)
        {
            cipher[alphabet[i]] = cipherValues[i];
        }
    }

    void shuffleCipher()
    {
        int n = cipherValues.Length;
        System.Random rng = new System.Random();

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int temp = cipherValues[k];
            cipherValues[k] = cipherValues[n];
            cipherValues[n] = temp;
        }

    }

    void eliminateLettersForPuzzle()
    {
        //Space check
        if (originalSaying[0] == ' ' || authorOfSaying[0] == ' ')
        {
            originalSaying = originalSaying.TrimStart();
            authorOfSaying = authorOfSaying.TrimStart();
        }

        originalSaying = eliminateExtraSpaces(originalSaying);
        originalSaying = originalSaying.ToUpper(new CultureInfo("tr-TR", false));

        eliminatedSaying = originalSaying.ToCharArray();
        int arraySize = originalSaying.Length;
        int desiredCount = (int)Math.Ceiling(arraySize * 3.0 / 5.0);
        randomIndexes = new int[desiredCount];
        System.Random random = new System.Random();

        List<int> allIndexes = new List<int>(arraySize);

        for (int i = 0; i < arraySize; i++)
        {
            allIndexes.Add(i);
        }

        for (int i = allIndexes.Count - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);
            int temp = allIndexes[i];
            allIndexes[i] = allIndexes[randomIndex];
            allIndexes[randomIndex] = temp;
        }

        for (int i = 0; i < desiredCount; i++)
        {
            randomIndexes[i] = allIndexes[i];
        }

        foreach (int index in randomIndexes)
        {
            if (eliminatedSaying[index] != ' ')
            {
                eliminatedSaying[index] = '0';
            }
        }

        eliminatedSaying = wrapText(new string(eliminatedSaying), lineLength).ToCharArray();

    }

    void displayArrayContents()
    {
        string alphabetOut = "Alphabet \n",
                cipherValuesOut = "Cipher values \n",
                sayingOut = "Saying \n",
                cipherOut = "Cipher \n",
                eliminatedOut = "Eliminated \n";

        for (int i = 0; i < alphabet.Length; i++) // alphabet
        {
            alphabetOut += "At pos " + i + " letter: " + alphabet[i] + "\n";
        }
        for (int i = 0; i < cipherValues.Length; i++) // cipher
        {
            cipherValuesOut += "At pos " + i + " number: " + cipherValues[i] + "\n";
        }

        foreach (var ciph in cipher)
        {
            cipherOut += "Key: " + ciph.Key + " Value: " + ciph.Value + "\n";
        }

        for (int i = 0; i < originalSaying.Length; i++) // saying
        {
            sayingOut += "At pos " + i + " letter: " + originalSaying[i] + "\n";
        }

        foreach (var item in eliminatedSaying)
        {
            if (item == '\n')
            {
                eliminatedOut += "\\n";
            }
            else
            {
                eliminatedOut += item;
            }
        }

        Debug.Log(alphabetOut);
        Debug.Log(cipherValuesOut);
        Debug.Log(sayingOut);
        Debug.Log(cipherOut);
        Debug.Log(eliminatedOut);

    }

    public void isLevelCompleted()
    {
        isLevelCompletedCheck = SelectLetterController.getIsLevelCompleted();

        if (isLevelCompletedCheck)
        {
            //WELL DONE LEVEL IS COMPLETED DO WIN RELATED STUFFS IN HERE
            Debug.Log("WIN");
            progressTracker.getSuccessPercentage();
            progressTracker.prepareNextLevel();
            winScreenController.levelCompleted(originalSaying, authorOfSaying, progressTracker.isGenreCompleted());
            SoundEffectManager.playLevelCompletedSFX();
        }
    }


    public void reduceAlphabetCounterValue(char ch)
    {
        if (alphabetCounter[ch] > 1)
        {
            alphabetCounter[ch]--;
        }
        else if (alphabetCounter[ch] == 1 && isSayingPrinted)
        {
            alphabetCounter[ch]--;
            numericalValueController.removeNumericalValuesOfLetter(ch.ToString());
            Debug.Log(ch + " letter is completed!");
            keyboard.inactivateCompletedLetter(ch);
            //remove the letter ciphers of that letter
        }

    }


    public TutorialLetterData[] getTutorialData()
    {
        TutorialLetterData[] data = new TutorialLetterData[alphabetCounter.Count];
        int i = 0;

        foreach (var item in alphabetCounter)
        {
            if (item.Value > 1)
            {
                data[i].character = item.Key;
                data[i].cipherCode = cipher[item.Key];
                data[i].numberOfOccurence = alphabetCounter[item.Key];
                i++;
            }
        }

        return data;
    }

    string wrapText(string text, int maxWidth)
    {
        string[] words = text.Split(' ');
        string wrappedText = "";
        string currentLine = "";

        foreach (string word in words)
        {
            if ((currentLine + word).Length <= maxWidth)
            {
                currentLine += word + ' ';
            }
            else
            {
                wrappedText += currentLine.Trim() + "+";
                currentLine = word + ' '; //check here may not be required to space
            }
        }

        // Add the last line
        wrappedText += currentLine.Trim();

        return wrappedText;
    }

    string eliminateExtraSpaces(string text)
    {
        int spaceCounter = 0;
        text = text.TrimStart();
        StringBuilder stringBuilder = new StringBuilder(text);

        for (int i = text.Length - 1; i >= 0; i--)
        {
            if (text[i] == ' ')
            {
                spaceCounter++;
                if (spaceCounter >= 2)
                {
                    stringBuilder.Remove(i + 1, 1);
                    //Debug.Log("here we have an extra space at pos" + (i + 1));
                }
            }
            else
            {
                spaceCounter = 0;
            }
        }
        //Debug.Log("here is the trimmed format: " + stringBuilder);
        return stringBuilder.ToString();
    }

    void instantiateBlankLetterObject(Letter letterScript, int i, int letterId)
    {
        letterScript.setLetter(' ');
        letterScript.setIsLetterInteractable(true);
        letterScript.setNumericalValue(cipher[originalSaying[i]], originalSaying[i]);
        letterScript.setLetterId(letterId);
        letterScript.initLetterObject();
    }

    void instantiateFilledLetterObject(Letter letterScript, int i, int letterId)
    {
        letterScript.setLetter(originalSaying[i]);
        letterScript.setIsLetterInteractable(false);
        letterScript.setNumericalValue(cipher[originalSaying[i]], originalSaying[i]);
        letterScript.setLetterId(letterId);
        letterScript.initLetterObject();
    }

    public Dictionary<char, int> getCipherData()
    {
        return cipher;
    }

    public void calculateAndRevealHint()
    {
        WordAppearControllerScript wordAppearControllerScript = FindObjectOfType<WordAppearControllerScript>();
        wordAppearControllerScript.setCounterData(alphabetCounter);
        wordAppearControllerScript.setSayingData(currentSayingSO.getSaying());
        wordAppearControllerScript.revealCipherHint();
    }

    public int getCipherValue(char key)
    {
        return cipher[key];
    }
}
