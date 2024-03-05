using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolderCardContent : MonoBehaviour
{
    [SerializeField] CardContentSO[] cardContents;
    [SerializeField] CardContentSO endCard, startCard;
    [SerializeField] CardController startCardObject;
    int currentIndex = 0;
    bool isCardsOut = false;

    CardContentSO currentCardContent;

    private void Start()
    {
        startCardObject.initCardData(startCard);
        currentCardContent = cardContents[currentIndex++];
    }

    public CardContentSO getNextCard()
    {
        if (!isCardsOut)
        {
            CardContentSO content = currentCardContent;
            currentCardContent = cardContents[currentIndex++];
            if (currentIndex > cardContents.Length - 1)
                isCardsOut = true;
            return content;
        }
        else
        {
            return endCard;
        }

    }
}
