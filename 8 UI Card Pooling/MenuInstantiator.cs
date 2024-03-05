using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInstantiator : MonoBehaviour
{
    [Header(" Content Object Reference ")]
    [SerializeField] GameObject contentObject;
    [Header(" Content ")]
    [SerializeField] MenuCardSO[] cards;
    [Header(" Card Prefabs ")]
    [SerializeField] GameObject categoryItemPrefab;
    //[SerializeField] GameObject mapItemPrefab;
    [SerializeField] GameObject profileItemPrefab;
    

    private void Start()
    {
        foreach (var cardData in cards)
        {
            string cardType = cardData.getCardType();
            MenuCard newCard;
            switch (cardType)
            {
                case "categoryCard":
                    newCard = Instantiate(categoryItemPrefab, contentObject.transform).GetComponent<CategoryCard>();
                    newCard.initCardValues(cardData);
                    break;

                case "profileCard":
                    newCard = Instantiate(profileItemPrefab, contentObject.transform).GetComponent<MenuCard>();
                    newCard.initCardValues(cardData);
                    break;

                default:

                    break;
            }

        }

    }

}
