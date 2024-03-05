using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlidingV2 : MonoBehaviour
{
     [SerializeField] float touchMoveMultiplier = 0.2f;
    [SerializeField] float autoMoveSpeed = 20f;
    bool isCardMovingToInitialPos = false;
    bool isCardMovingToOutOfScreen = false;
    Vector3 initialPos;
    Touch touch;
    int screenWidth, screenHeight;
    int horVal, verVal;

    CardMaster cardMaster;

    private void Awake() {
        cardMaster = FindObjectOfType<CardMaster>();
    }

    private void Start() {
        initialPos = transform.position;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    private void Update() {
        moveToInitialPos();
        moveToOutOfScreen();
        touchInput();
    }

    void touchInput(){
        if(Input.touchCount > 0) {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved){
                transform.position = new Vector2(
                    transform.position.x + touch.deltaPosition.x * touchMoveMultiplier, 
                    transform.position.y + touch.deltaPosition.y * touchMoveMultiplier);
            }else if(touch.phase == TouchPhase.Ended && Mathf.Abs(transform.position.x / screenWidth - 0.5f) > 0.15f){
                if(transform.position.x / screenWidth - 0.5f > 0){
                    if(transform.position.y / screenHeight - 0.5f > 0){
                        isCardMovingToOutOfScreen = true;
                        horVal = 1;
                        verVal = 1;
                    }else{
                        isCardMovingToOutOfScreen = true;
                        horVal = 1;
                        verVal = -1;
                    }
                }else{
                    if(transform.position.y / screenHeight - 0.5f > 0){
                        isCardMovingToOutOfScreen = true;
                        horVal = -1;
                        verVal = 1;
                    }else{
                        isCardMovingToOutOfScreen = true;
                        horVal = -1;
                        verVal = -1;
                    }
                }
                
            }else if(touch.phase == TouchPhase.Ended){
                isCardMovingToInitialPos = true;
            }
        }
    }

    void moveToInitialPos(){
        if(isCardMovingToInitialPos){
            transform.position = Vector2.MoveTowards(transform.position, initialPos, autoMoveSpeed * Time.deltaTime);
            if(transform.position == initialPos){
                isCardMovingToInitialPos = false;
            }
        }
    }

    void moveToOutOfScreen(){
        if(isCardMovingToOutOfScreen){
            Vector2 outOfScreenPos = new Vector2(screenWidth * 2 * horVal + initialPos.x, transform.position.y);
            Debug.Log(initialPos);
            transform.position = Vector2.MoveTowards(transform.position, outOfScreenPos, autoMoveSpeed * Time.deltaTime);
            if((Vector2)transform.position == outOfScreenPos){
                isCardMovingToOutOfScreen = false;
                cardMaster.nextCard();
                transform.position = initialPos;
            }
        }
        
    }
}
