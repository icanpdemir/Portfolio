using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SupportTurkishDeveloperPopUp : MonoBehaviour
{
    [SerializeField] GameObject commentPleasePopUp;

    void Start()
    {
        if (PlayerPrefs.GetInt(PlayerPrefs.GetString("currentGenre")) == 7 
            && PlayerPrefs.GetInt("isCommentSuggested", 0) == 0)
        {
            commentPleasePopUp.SetActive(true);
            PlayerPrefs.SetInt("isCommentSuggested", 1);
        }
    }

    public void commentToApp()
    {
        string deviceType;

#if UNITY_IOS
        deviceType = "ios";
        commentPleasePopUp.SetActive(false);
#elif UNITY_ANDROID
        deviceType = "android";
        commentPleasePopUp.SetActive(false);
#endif
        commentPleasePopUp.SetActive(false);

        if (deviceType == "ios")
        {
            Application.OpenURL("https://apps.apple.com/us/app/%C5%9Fifreli-bulmaca-l%C3%BCgaz/id6475111435");
        }
        else if (deviceType == "android")
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.icpgame.sifrelibulmacalugaz");
        }
    }
}
