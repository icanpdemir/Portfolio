using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorCountManager : MonoBehaviour
{
    [SerializeField] Color32[] colors;
    [SerializeField] string[] colorNames;
    [SerializeField] Text countTxtUI, targetTxtUI;
    [SerializeField] GameObject wonUI;

    int[] colorSums;
    int[] targetColorSums;
    int[] buttonColors;
    int[] buttonValues;
    int size, colorsLength;
    int currIndx = 0;

    SFXController sfxController;
    Timer timer;

    bool isFirstClick = true;

    private void Awake() {
        size = FindObjectsOfType<BtnColor>().Length;
        colorsLength = colors.Length;
        assignValues();
        assignTargetColorValues();
        printColorCount();

        sfxController = GetComponent<SFXController>();
        timer = GetComponent<Timer>();
    }

    void assignValues(){
        buttonColors = new int[size];
        buttonValues = new int[size];
        colorSums = new int[colorsLength];

        for (int i = 0; i < colorSums.Length; i++)
        {
            colorSums[i] = 0;
        }
        
        for (int i = 0; i < size; i++)
        {
            buttonColors[i] = Random.Range(0, colorsLength - 1);
            buttonValues[i] = Random.Range(1,9);
            
            colorSums[buttonColors[i]] += buttonValues[i];
        } 
    }

    public void changeColor(int index){
        if(isFirstClick){
            isFirstClick = false;
            timer.setTimerOn();
        }
        colorSums[buttonColors[index]] -= buttonValues[index];
        buttonColors[index] = (buttonColors[index] + 1) % colorsLength;
        colorSums[buttonColors[index]] += buttonValues[index];
        sfxController.playChangeColorSFX();
        checkWinStatus();
        printColorCount();
    }

    public int getButtonIndex(){
        return currIndx++;
    }

    public int getValue(int index){
        return buttonValues[index];
    }

    public int getColorIndx(int index){
        return buttonColors[index];
    }

    public Color32[] getColors(){
        return colors;
    }

    void assignTargetColorValues(){
        int randColorIndex;
        targetColorSums = new int[colorsLength];
        for (int i = 0; i < colorsLength; i++)
        {
            targetColorSums[i] = 0;
        }
        for (int i = 0; i < size; i++)
        {
            randColorIndex = Random.Range(0, colorsLength);
            targetColorSums[randColorIndex] += buttonValues[i];
        }
    }

    void printColorCount(){
        string msg = "";
        string targetMsg = "";

        for (int i = 0; i < colorsLength; i++)
        {
            msg += colorNames[i] + ": " + colorSums[i] + "\n"; 
            targetMsg +=  colorNames[i] + ": " + targetColorSums[i] + "\n"; 
        }
        //Debug.Log(msg);
        countTxtUI.text = msg;
        targetTxtUI.text = targetMsg;
        
    }

    bool checkWinStatus(){
        for (int i = 0; i < colorsLength; i++)
        {
            if(colorSums[i] != targetColorSums[i]){
                return false;
            }
        }
        //Debug.Log("YOU WON!!");
        sfxController.playWonSFX();
        wonUI.SetActive(true);
        
        StartCoroutine(restartLevel());
        return true;
    }

    IEnumerator restartLevel()
    {
    
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    }

}
