using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardContentSO", fileName = " Card")]
public class CardContentSO : ScriptableObject
{
    [SerializeField] string cardTitle;
    [SerializeField] string cardContent;
    [SerializeField] Sprite cardImage;
    [SerializeField] CardEffect[] leftCardEffects;
    [SerializeField] CardEffect[] rightCardEffects;
    
    public string getCardTitle(){
        return cardTitle;
    }

    public string getCardContent(){
        return cardContent;
    }

    public Sprite getCardImage(){
        return cardImage;
    }

    public CardEffect[] getLeftCardEffects(){
        return leftCardEffects;
    }

    public CardEffect[] getRightCardEffects(){
        return rightCardEffects;
    }
}

[System.Serializable]
public struct CardEffect
{
    public int parameterId;
    public int value;
}

