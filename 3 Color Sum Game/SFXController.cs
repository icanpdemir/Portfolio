using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField] AudioClip changeColorsSound, wonSound, loseSound, timerOnSound;
    AudioSource audioSource;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void playChangeColorSFX(){
        audioSource.PlayOneShot(changeColorsSound);
    }

    public void playWonSFX(){
        audioSource.PlayOneShot(wonSound);
    }

    public void playLoseSFX(){
        audioSource.PlayOneShot(loseSound);
    }

    public void playTimerOnSFX(){
        audioSource.PlayOneShot(timerOnSound);
    }
}
