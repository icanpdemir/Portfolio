using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class ColorChangeScript : MonoBehaviour
{
    enum colors{red , green , blue}

    [SerializeField] GameObject redSet, blueSet, greenSet;
    [SerializeField] colors initialColor = colors.red;
    ColorPaletteSettings colorPaletteSettings;
    ColorAreaScript[] redAreas, greenAreas, blueAreas;
    colors currentColor;

    private void Awake() {
        redAreas = redSet.GetComponentsInChildren<ColorAreaScript>();
        blueAreas = blueSet.GetComponentsInChildren<ColorAreaScript>();
        greenAreas = greenSet.GetComponentsInChildren<ColorAreaScript>();
        colorPaletteSettings = FindObjectOfType<ColorPaletteSettings>();
        initColors();
    }

    private void Start() {
        currentColor = initialColor;
        changeCurrentColor(currentColor);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            currentColor = colors.red; 
            changeCurrentColor(currentColor); 
        }else if (Input.GetKeyDown(KeyCode.Alpha2)){
            currentColor = colors.green; 
            changeCurrentColor(currentColor);
        }else if (Input.GetKeyDown(KeyCode.Alpha3)){
            currentColor = colors.blue; 
            changeCurrentColor(currentColor);
        }
    }

    void changeCurrentColor(colors color){
        foreach (ColorAreaScript area in greenAreas){
            area.changeColorAreaStatus(false);
        }
        foreach (ColorAreaScript area in blueAreas){
            area.changeColorAreaStatus(false);
        }
        foreach (ColorAreaScript area in redAreas){
            area.changeColorAreaStatus(false);
        }
        switch (color)
        {
            case colors.red:
                foreach (ColorAreaScript area in redAreas)
                {
                    area.changeColorAreaStatus(true);
                }
                // Debug.Log("red");
                break;
            case colors.green:
                foreach (ColorAreaScript area in greenAreas)
                {
                    area.changeColorAreaStatus(true);
                }
                // Debug.Log("green");
                break;
            case colors.blue:
                foreach (ColorAreaScript area in blueAreas)
                {
                    area.changeColorAreaStatus(true);
                }
                // Debug.Log("blue");
                break;
        }
    }

    void initColors(){
        Color32 nonActiveColor = colorPaletteSettings.getNonActiveColor();
        Color32 activeColorOne = colorPaletteSettings.getActiveColorOne();
        Color32 activeColorTwo = colorPaletteSettings.getActiveColorTwo();
        Color32 activeColorThree = colorPaletteSettings.getActiveColorThree();
        foreach (ColorAreaScript area in redAreas){
            area.initActiveColor(activeColorOne);
            area.initNonActiveColor(nonActiveColor);
        }
        foreach (ColorAreaScript area in greenAreas){
            area.initActiveColor(activeColorTwo);
            area.initNonActiveColor(nonActiveColor);
        }
        foreach (ColorAreaScript area in blueAreas){
            area.initActiveColor(activeColorThree);
            area.initNonActiveColor(nonActiveColor);
        }
    }
}
