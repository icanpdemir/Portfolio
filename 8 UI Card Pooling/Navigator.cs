using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField] GameObject[] menuObjects;

    private void Start() {
        changeMenu(0);
    }

    public void changeMenu(int i){
        foreach (var menu in menuObjects)
        {
            menu.SetActive(false);
        }
        
        if(i <= menuObjects.Length){
            menuObjects[i].SetActive(true);
        }
    }
}
