using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockItems : MonoBehaviour
{
    UIController uiController;
    private void Start() {
        uiController = FindObjectOfType<UIController>();
    }
    private void OnCollisionEnter(Collision other) { 
        Destroy(other.gameObject);
        uiController.updateCubeCount();    
    }
}
