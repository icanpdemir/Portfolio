using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cubeCounter;
    int cubeCount = 0;
    private void Start() {
        cubeCounter.text = "Cube count = "+ cubeCount;
    }
    public void updateCubeCount(){
        cubeCounter.text = "Cube count = " + ++cubeCount;
    }
}
