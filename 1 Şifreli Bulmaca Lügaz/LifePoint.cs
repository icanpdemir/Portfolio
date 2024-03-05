using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifePoint : MonoBehaviour
{
    [Header(" Object References ")]
    [SerializeField] Animator lifePointAnimator;
    [SerializeField] TMP_Text lifePointTxt;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject[] uiObjectsToDisableAfterLose;

    [Header(" Settings ")]
    [SerializeField] int maxLifePoint = 3;
    
    Interstitial InterstitialAd;
    int currentLifePoint;

    private void Start() {
        currentLifePoint = maxLifePoint;
        InterstitialAd = FindObjectOfType<Interstitial>();
        updateLifePointUI();
    }

    public void decreaseLifePoint(){
        if(currentLifePoint > 1){
            currentLifePoint--;
            SoundEffectManager.playWrongAnswerSFX();
            lifePointAnimator.SetTrigger("lifeDecrease");
        }else{
            //game over screen appears and then restart button...
            InterstitialAd.ShowAd();
            currentLifePoint--;
            gameOverUI.SetActive(true);
            foreach (var item in uiObjectsToDisableAfterLose)
            {
                item.SetActive(false);
            }

        }
        
        updateLifePointUI();
    }

    void updateLifePointUI(){
        lifePointTxt.text = "X" + currentLifePoint;
    }
}
