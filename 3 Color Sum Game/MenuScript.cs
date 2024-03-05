using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public void startGame(){
        GetComponent<SFXController>().playChangeColorSFX();
        SceneManager.LoadScene(1);
    }
}
