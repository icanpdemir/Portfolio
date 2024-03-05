using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoorScript : MonoBehaviour
{
    [SerializeField] float timeToCountDownCompleteLevel = 0.5f;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
            StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(timeToCountDownCompleteLevel);
        Debug.Log("CONGRATS!");
    }
}
