using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderControlScript : MonoBehaviour
{
    [SerializeField] float countdownTime = 3f; 
    int activatedAreas;
    bool isPlayerOnDeadZone;

    void Awake()
    {
        isPlayerOnDeadZone = false;
        activatedAreas = 0;
    }

    public void setDeadZoneStatus(bool status){
        isPlayerOnDeadZone = status;
        if(isPlayerOnDeadZone){
            activatedAreas--;
            //Debug.Log("Activated zone numbers: " + activatedAreas);
            StartCoroutine(StartCountdown());
        }else{
            activatedAreas++;
            //Debug.Log("Activated zone numbers: " + activatedAreas);
            StopCoroutine(StartCountdown());
        }
        
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(countdownTime);
        if(isPlayerOnDeadZone && activatedAreas < 1){
            Debug.Log("dead");
            restartLevel();
        }
    }

    void restartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
