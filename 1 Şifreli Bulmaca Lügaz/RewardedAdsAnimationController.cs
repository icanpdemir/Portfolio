using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedAdsAnimationController : MonoBehaviour
{
    [SerializeField] Animator rewardedAdsButtonAnimator;
    [SerializeField] Animator rewardedAdsTxtAnimator;
    [SerializeField] float secondsToWaitBeforeAnimation;

    static float currentTime;
    static float initialTime;

    private void Awake()
    {
        initialTime = secondsToWaitBeforeAnimation;
        currentTime = initialTime;
    }

    private void Start()
    {
        if (SayingManager.isLevelCompletedCheck)
        {
            StartCoroutine(countDown());
        }
    }

    IEnumerator countDown()
    {

        yield return new WaitForSeconds(1f);
        currentTime--;

        if (currentTime <= 0f)
        {
            Debug.Log("TIME IS OUT ITS ANIMATION TIME");
            rewardedAdsButtonAnimator.SetTrigger("watchMe");
            rewardedAdsTxtAnimator.SetTrigger("watchMe");
            currentTime = initialTime;
        }

        StartCoroutine(countDown());
    }

    public static void resetTime()
    {
        currentTime = initialTime;
    }


}
