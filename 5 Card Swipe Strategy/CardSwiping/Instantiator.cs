using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField] DataHolderCardContent dataHolderCardContent;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] CardController firstCard;
    CardContentSO cardContent;

    private void Start() {
        firstCard.initCardData(dataHolderCardContent.getNextCard());
    }


    void Update()
    {
        if (transform.childCount < 2)
        {
            InstantiateCard();
        }
    }

    void InstantiateCard()
    {
        GameObject newCard = Instantiate(cardPrefab, transform, false);
        cardContent = dataHolderCardContent.getNextCard();
        if(cardContent != null)
            newCard.GetComponent<CardController>().initCardData(cardContent);
        else
            Debug.Log("Card content is null!");
        newCard.transform.SetAsFirstSibling();
    }
}
