using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialV3 : MonoBehaviour
{
    [Header(" UI Objects References ")]
    [SerializeField] GameObject tutorialWindowObject;
    [SerializeField] TMP_Text tutorialText;
    [SerializeField] Image tutorialImage;
    [SerializeField] GameObject tutorialImageObject;
    [SerializeField] GameObject tutorialPointerObject;
    [SerializeField] GameObject keyboardObject;
    [SerializeField] GameObject sayingAreaObject;
    [SerializeField] GameObject hintButton;
    [SerializeField] GameObject hintObject;
    [SerializeField] GameObject pauseButtonObject;

    [Header(" Tutorial Images ")]
    [SerializeField] Sprite[] tutorialImageContents;
    [SerializeField] Transform[] tutorialPositionContents;

    Transform tutorialPointer;
    bool[] prevUIStates;

    int tutorialPhase = 0;
    string[] tutorialTextContents = new string[20];

    void Awake()
    {
        setTutorialTextContents();
        tutorialPointer = tutorialPointerObject.GetComponent<Transform>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && tutorialWindowObject.active)
        {
            setTutorialToNextStep();
        }

    }

    void setTutorialTextContents()
    {
        //phase 0
        tutorialTextContents[0] = "Lügaz’a hoşgeldin! Birazdan şifre çözme becerilerini sınayacağız";

        //phase 1
        tutorialTextContents[1] = "Her sayı bir harfi temsil ediyor, örneğin A harfi 14";

        //phase 2
        tutorialTextContents[2] = "İşte! Bu boşlukta da 14 sayısı var, demek ki A harfi gelmeli";

        //phase 3
        tutorialTextContents[3] = "Boşluğu seçmek için klavyedeki okları kullanabilir ya da alanın üzerine dokunabilirsin ";

        //phase 3
        tutorialTextContents[4] = "Yerleştirmek istediğin harfi de klavyeden seçebilirsin";

        //phase4
        tutorialTextContents[5] = "Unutma! Şifrenin içinde tamamı bulunan harflerin sayıları yok olur";

        //phase5
        tutorialTextContents[6] = "Müzikten sıkılırsan menüden değiştirebilirsin\nBol şans! ";
    }

    void setTutorialToNextStep()
    {
        switch (tutorialPhase)
        {
            case 0:
                sayingAreaObject.SetActive(false);
                keyboardObject.SetActive(false);
                hintButton.SetActive(false);
                hintObject.SetActive(false);
                pauseButtonObject.SetActive(false);
                tutorialText.text = tutorialTextContents[tutorialPhase];
                tutorialImageObject.SetActive(false);
                tutorialImage.sprite = tutorialImageContents[tutorialPhase];
                tutorialImageObject.SetActive(true);
                tutorialImage.preserveAspect = true;
                break;

            case 1:
                tutorialText.text = tutorialTextContents[tutorialPhase];
                tutorialImage.sprite = tutorialImageContents[tutorialPhase];
                break;

            case 2:
                tutorialText.text = tutorialTextContents[tutorialPhase];
                tutorialImage.sprite = tutorialImageContents[tutorialPhase];
                break;

            case 3:
                keyboardObject.SetActive(true);
                tutorialText.text = tutorialTextContents[tutorialPhase];
                break;

            case 4:
                tutorialPointer.position = tutorialPositionContents[tutorialPhase].position;
                tutorialText.text = tutorialTextContents[tutorialPhase];
                break;

            case 5:
                tutorialText.text = tutorialTextContents[tutorialPhase];
                tutorialImage.sprite = tutorialImageContents[tutorialPhase];

                break;

            case 6:
                tutorialText.text = tutorialTextContents[tutorialPhase];
                tutorialImage.sprite = tutorialImageContents[tutorialPhase];

                break;

            case 7:
                tutorialWindowObject.SetActive(false);
                sayingAreaObject.SetActive(true);
                hintButton.SetActive(true);
                if (WordAppearControllerScript.isHintActivated)
                {
                    hintObject.SetActive(true);
                }
                pauseButtonObject.SetActive(true);
                PlayerPrefs.SetString("isTutorialLevel", "false");
                tutorialPhase = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;

            default:
                break;
        }
        //Debug.Log(tutorialPhase);
        tutorialPhase++;

    }

    public void startTutorial()
    {
        tutorialPhase = 0;
        setTutorialToNextStep();
        tutorialWindowObject.SetActive(true);
    }

}

