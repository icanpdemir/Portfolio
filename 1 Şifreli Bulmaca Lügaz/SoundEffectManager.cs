using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    [Header(" SFX Files ")]
    [SerializeField] AudioClip correctAnswerSFX;
    [SerializeField] AudioClip wrongAnswerSFX;
    [SerializeField] AudioClip levelCompletedSFX;
    [SerializeField] AudioClip[] musicArraySFX;

    [Header(" Object References ")]
    [SerializeField]

    static AudioSource audioSource;

    static AudioClip correctAnswer, wrongAnswer, levelCompleted;
    static AudioClip[] musicArray;
    static int currentMusicIndex;

    private void Awake()
    {
        currentMusicIndex = PlayerPrefs.GetInt("musicIndex", 0);
        audioSource = GetComponent<AudioSource>();

        correctAnswer = correctAnswerSFX;
        wrongAnswer = wrongAnswerSFX;
        levelCompleted = levelCompletedSFX;
        musicArray = new AudioClip[musicArraySFX.Length];
        for (int i = 0; i < musicArraySFX.Length; i++)
        {
            musicArray[i] = musicArraySFX[i];
        }
        audioSource.volume = PlayerPrefs.GetFloat("volume", 0.65f);
        audioSource.clip = musicArray[currentMusicIndex];
        audioSource.Play();
        FindObjectOfType<UIOperationsController>().changeMusicTxt(musicArray[currentMusicIndex].name);

    }


    public static void playCorrectAnswerSFX()
    {
        audioSource.PlayOneShot(correctAnswer);
    }

    public static void playWrongAnswerSFX()
    {
        audioSource.PlayOneShot(wrongAnswer);
    }

    public static void playLevelCompletedSFX()
    {
        audioSource.PlayOneShot(levelCompleted);
    }

    public static float playLetterCompletedSFX()
    {
        audioSource.PlayOneShot(correctAnswer);
        return correctAnswer.length;
    }

    public static void muteVolume(Slider slider)
    {
        slider.value = 0f;
        PlayerPrefs.SetFloat("volume", 0f);
        audioSource.volume = 0f;
    }

    public static void changeVolumeSlider(Slider slider)
    {
        PlayerPrefs.SetFloat("volume", slider.value);
        audioSource.volume = slider.value;
    }

    public static void changeBackgroundMusic()
    {
        currentMusicIndex = (currentMusicIndex + 1) % musicArray.Length;
        PlayerPrefs.SetInt("musicIndex", currentMusicIndex);
        audioSource.clip = musicArray[currentMusicIndex];
        audioSource.Play();

        FindObjectOfType<UIOperationsController>().changeMusicTxt(musicArray[currentMusicIndex].name);

    }



}
