using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] TMP_Text cardTitle;
    [SerializeField] Image cardImage;
    [SerializeField] TMP_Text cardContent;
    CardContentSO cardContentSO;
    CardEffect[]? leftCardEffects;
    CardEffect[]? rightCardEffects;

    public void initCardData(CardContentSO card)
    {
        cardContentSO = card;
        cardTitle.text = cardContentSO.getCardTitle();
        cardImage.sprite = cardContentSO.getCardImage();
        cardContent.text = cardContentSO.getCardContent();
    }

    public void applyCardEffect(bool isSwipedLeft)
    {
        if (isSwipedLeft && cardContentSO.getLeftCardEffects() != null)
        {
            foreach (var item in cardContentSO.getLeftCardEffects())
            {
                ParameterManager.changeParameterValue(item.parameterId, item.value);
            }
        }
        else if (cardContentSO.getRightCardEffects() != null)
        {
            foreach (var item in cardContentSO.getRightCardEffects())
            {
                ParameterManager.changeParameterValue(item.parameterId, item.value);
            }
        }
    }

}
