using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] TMP_Text keyText;
    [SerializeField] Button keyButton;
    char key;

    public void setKey(char key){
        this.key = key;
        keyText.text = key.ToString();
    }  

    public char getKey(){
        return key;
    } 

    public void deactivateKeyObject(){
        keyButton.interactable = false;
    }

    public void activateKeyObject(){
        keyButton.interactable = true;
    }

    public Button getButton(){
        return GetComponent<Button>();
    } 
}
