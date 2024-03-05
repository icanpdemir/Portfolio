using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Letter : MonoBehaviour
{
    [Header(" Color Settings ")]
    [SerializeField] Color32 trueAnswerColor;
    [SerializeField] Color32 falseAnswerColor;
    [SerializeField] Color32 defaultColor;
    [SerializeField] Color32 selectedLetterColor;

    [Header(" Text Objects ")]
    [SerializeField] TMP_Text letterTxt;
    [SerializeField] TMP_Text letterCipherTxt;

    LifePoint lifePointManager;
    SayingManager sayingManager;
    Image imageComponent;
    char letterAnswer;
    bool isLetterInteractable = true;
    bool wrongAnswerCheck = true;
    int letterId;

    private void Start()
    {
        lifePointManager = FindObjectOfType<LifePoint>();
        sayingManager = FindObjectOfType<SayingManager>();
        imageComponent = GetComponent<Image>();
    }

    public void setLetter(char ch)
    {
        letterTxt.text = ch.ToString();
    }

    public string getLetter()
    {
        return letterAnswer.ToString();
    }

    public void changeLetter(char ch)
    {
        if (isLetterInteractable && wrongAnswerCheck)
        {
            if (ch == letterAnswer)
            {
                isLetterInteractable = false; // right answer lockes the letter interaction
                changeLetterColor("true");
                SoundEffectManager.playCorrectAnswerSFX();
                letterTxt.text = ch.ToString();
                sayingManager.isLevelCompleted();
                sayingManager.reduceAlphabetCounterValue(ch);
                RewardedAdsAnimationController.resetTime();
                Invoke("resetLetterColorTrue", 0.4f);
                //StartCoroutine(resetLetterColor(true));
                if (!SelectLetterController.getIsLevelCompleted())
                    SelectLetterController.moveSelectionToNextLetter();
                //Debug.Log("TRUE");
            }
            else
            {
                changeLetterColor("false");
                SoundEffectManager.playWrongAnswerSFX();
                letterTxt.text = ch.ToString();
                lifePointManager.decreaseLifePoint();
                wrongAnswerCheck = false;
                Invoke("resetLetterColorFalse", 0.4f);
                //StartCoroutine(resetLetterColor(false));
            }
        }
    }

    public void setNumericalValue(int number, char answer)
    {
        letterCipherTxt.text = number.ToString();
        letterAnswer = answer;
    }

    void resetLetterColorFalse()
    {
        wrongAnswerCheck = true;

        if (SelectLetterController.selectedLetterObject != null)
        {
            SelectLetterController.checkSelection();
        }

        letterTxt.text = " ";
    }

    void resetLetterColorTrue()
    {
        //Debug.Log("testol");
        imageComponent.color = defaultColor;
    }

    private IEnumerator resetLetterColor(bool state)
    {
        yield return null;

        yield return new WaitForSeconds(0.4f);

        if (state)
        { // unchoose it
            imageComponent.color = defaultColor;
        }
        else
        { // still chosen
            if (SelectLetterController.selectedLetterObject != null)
            {
                SelectLetterController.checkSelection();
            }
            letterTxt.text = " ";
        }

    }

    public void changeLetterColor(string type)
    {
        if (imageComponent != null)
        {
            if (type == "true")
            {
                imageComponent.color = trueAnswerColor;
            }
            else if (type == "false")
            {
                imageComponent.color = falseAnswerColor;
            }
            else if (type == "selected")
            {
                imageComponent.color = selectedLetterColor;
            }
            else
            {
                imageComponent.color = defaultColor;
            }
        }
    }

    private IEnumerator letterCompleted()
    {
        if (imageComponent != null)
        {
            imageComponent.color = trueAnswerColor;

            yield return new WaitForSeconds(0.4f);

            imageComponent.color = defaultColor;
        }

    }

    public void letterCompletedEffect()
    {
        if (!SayingManager.isLevelCompletedCheck)
            StartCoroutine(letterCompleted());
    }

    public void initLetterObject()
    {
        SelectLetterController.initLetterObjects(letterId, this);
    }

    public void setLetterId(int id)
    {
        letterId = id;
    }

    public int getLetterId()
    {
        return letterId;
    }

    public bool getIsLetterInteractable()
    {
        return isLetterInteractable;
    }

    public void setIsLetterInteractable(bool val)
    {
        isLetterInteractable = val;
    }

    public void hideNumericalValue()
    {
        letterCipherTxt.text = " ";
    }

    public void selectThisLetter()
    {
        SelectLetterController.changeSelectionV2(letterId, this);
    }
}
