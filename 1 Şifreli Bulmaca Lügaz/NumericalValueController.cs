using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericalValueController : MonoBehaviour
{
    List<Letter> letterObjectsOnScreen = new List<Letter>();

    public void removeNumericalValuesOfLetter(string ch)
    {
        if(SayingManager.isSayingPrinted){
            WaitForSeconds wait = new WaitForSeconds(SoundEffectManager.playLetterCompletedSFX());
            StartCoroutine(PlaySoundDelayed(wait));
        }
        
        foreach (Letter letter in letterObjectsOnScreen)
        {   //Debug.Log(letter.getLetter() );
            if (letter.getLetter() == ch)
            {
                //Debug.Log("found");
                letter.letterCompletedEffect();
                letter.hideNumericalValue();
            }
        }
    }

    public void addLetterObject(Letter obj)
    {
        letterObjectsOnScreen.Add(obj);
    }

    IEnumerator PlaySoundDelayed(WaitForSeconds wait)
    {
        yield return wait;

        SoundEffectManager.playLetterCompletedSFX();
    }

}
