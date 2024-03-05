using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float countdownTime = 5f;
    [SerializeField] Text timerTxt;
    SFXController sfxController;
    [SerializeField] Animator timerAnimator;
    bool isTimerOn;

    void Awake() {
        timerTxt.text = countdownTime.ToString();
        StartCoroutine(ActiveOnTimer());
        isTimerOn = false;
    }

    private void Start() {
        sfxController = GetComponent<SFXController>();
    }

    IEnumerator ActiveOnTimer(){
        //Debug.Log("ıenumartor activaed");
        while(true){
            //Debug.Log("inside of while");
            yield return new WaitForSeconds(1f);
            if(countdownTime >= 1 && isTimerOn){
                //Debug.Log("inside of if");
                timerAnimator.SetTrigger("count");
                countdownTime--;
                timerTxt.text = countdownTime.ToString();
            }else if(countdownTime == 0 && isTimerOn){
                //game over
                sfxController.playLoseSFX();
                StopCoroutine(ActiveOnTimer());
                isTimerOn = false;
            }

        }
    }

    public void setTimerOn(){
        isTimerOn = true;
        sfxController.playTimerOnSFX();
        //Debug.Log("timer is on!");
    }
}
