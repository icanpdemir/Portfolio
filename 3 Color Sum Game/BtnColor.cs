using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnColor : MonoBehaviour
{
    ColorCountManager colorCountManager;
    Animator animator;
    Color32[] colors;
    Image btnColor;
    Text btnText;
    int btnIndex;
    int currentColorIndx, btnValue;

    private void Start() {
        animator = GetComponent<Animator>();
        colorCountManager = FindObjectOfType<ColorCountManager>();
        btnIndex = colorCountManager.getButtonIndex();
        colors = colorCountManager.getColors();
        btnValue = colorCountManager.getValue(btnIndex);
        //Debug.Log("my index is: " + btnIndex);
        currentColorIndx = colorCountManager.getColorIndx(btnIndex);

        btnColor = GetComponent<Image>();
        btnText = GetComponentInChildren<Text>();
        btnText.text = btnValue.ToString();
        btnColor.color = colors[currentColorIndx];
    }

    public void changeColor(){
        currentColorIndx = (currentColorIndx + 1) % colors.Length;
        btnColor.color = colors[currentColorIndx];
        colorCountManager.changeColor(btnIndex);
        animator.SetTrigger("click");
    }
}
