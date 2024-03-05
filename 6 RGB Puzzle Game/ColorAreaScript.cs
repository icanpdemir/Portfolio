using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAreaScript : MonoBehaviour
{
    [SerializeField] Collider2D colliderColorArea;
    [SerializeField] SpriteRenderer spriteRendererColorArea;
    ColliderControlScript colliderController;
    Color32 activeColor, nonActiveColor;
    bool isActivated;

    private void Awake() {
        colliderController = FindObjectOfType<ColliderControlScript>();
        isActivated = false;
    }

    private void Start() {
        colliderColorArea.enabled = isActivated;
        if(isActivated){
            setActiveColor();
        }else{
            setInactiveColor();
        }
        //spriteRendererColorArea.enabled = isActivated;
    }

    public void changeColorAreaStatus(bool val){
        isActivated = val;
        colliderColorArea.enabled = isActivated;
        if(isActivated){
            setActiveColor();
        }else{
            setInactiveColor();
        }
        //spriteRendererColorArea.enabled = isActivated;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            colliderController.setDeadZoneStatus(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            colliderController.setDeadZoneStatus(true);
        }
    }

    void setInactiveColor(){
        spriteRendererColorArea.color = nonActiveColor;
    }

    void setActiveColor(){
        spriteRendererColorArea.color = activeColor;
    }

    public void initNonActiveColor(Color32 color){
        nonActiveColor = color;
    }

    public void initActiveColor(Color32 color){
        activeColor = color;
    }

    
}
