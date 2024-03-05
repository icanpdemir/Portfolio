using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSliding : MonoBehaviour
{
    [SerializeField] Transform cardTransform;
    Vector2 initPos;
    Touch touch;
    [SerializeField] float speedModifier = 0.1f, outToScreenSpeedModifier = 0.1f;
    int screenWidth, screenHeight;

    bool moveFlag = false;

    private void Start() {
        initPos = gameObject.transform.position;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }
    private void Update() {
        if(Input.touchCount > 0){
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved){
                transform.position = new Vector2(
                    cardTransform.position.x + touch.deltaPosition.x * speedModifier,
                    cardTransform.position.y + touch.deltaPosition.y * speedModifier
                );
                //Debug.Log("x pos is: " + (cardTransform.position.x - initPos.x));
            }else if(touch.phase == TouchPhase.Ended){
                 if(cardTransform.position.x - initPos.x > 250){
                    //moveCardOutOfScreen(false, 1);
                    Debug.Log("Right");
                    moveFlag = true;
                    
                }else if(cardTransform.position.x - initPos.x < -250){
                    //moveCardOutOfScreen(true, -1);
                    Debug.Log("Left");
                }
            }
        }
        if(moveFlag){
            cardTransform.position = Vector2.MoveTowards(cardTransform.position, 
                                                                    new Vector2(3000, 0), outToScreenSpeedModifier * 
                                                                    Time.deltaTime );
        }
    }

    void moveCardOutOfScreen(bool isLeft, int val){
        Vector2 outOfScreenPos = new Vector2(screenWidth * 2 * val + initPos.x, transform.position.y);
        if(isLeft){
            transform.position = Vector2.MoveTowards(transform.position, outOfScreenPos, outToScreenSpeedModifier * Time.deltaTime);
        }else{
            transform.position = Vector2.MoveTowards(transform.position, outOfScreenPos, outToScreenSpeedModifier * Time.deltaTime);
        }
 
        if((Vector2)transform.position == outOfScreenPos){
            //isCardMovingToOutOfScreen = false;
            //cardMaster.nextCard();
            //transform.position = initialPos;
            Debug.Log("out of screen reached");
        }
        
    }
    
}
