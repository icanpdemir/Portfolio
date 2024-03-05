using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardMaster : MonoBehaviour
{
    [SerializeField] List<CardSO> cards;
    [SerializeField] Color32[] cardTypeColors;
    [SerializeField] TextMeshProUGUI cardNameTxt, cardTextTxt, cardCountTxt;
    [SerializeField] CardSO passCard;
    [SerializeField] TextMeshProUGUI playerNameTxt;
    int randIndx;
    int currentCardNum, deckSize;
    bool isPassCard;

    CardSO currentCard;
    //PlayerMaster playerMaster;
    string currentCardName, currentCardType, currentCardText;

    private void Awake()
    {
        //playerMaster = FindObjectOfType<PlayerMaster>();
        currentCardNum = 0;
        deckSize = cards.Count;
        isPassCard = true;

    }

    private void Start()
    {
        currentCard = passCard;
        //updateCardData();
        currentCardName = playerNameTxt.text;
        cardCountTxt.enabled = false;
        isPassCard = false;
    }

    void getNewCard()
    {
        randIndx = Random.Range(0, cards.Count);
        currentCard = cards[randIndx];
        cards.Remove(cards[randIndx]);
        isPassCard = true;
    }

    void getPassCard()
    {
        currentCard = passCard;
        isPassCard = false;
    }

    /*void updateCardData()
    {
        if (isPassCard)
        {
            string[] cardArray = currentCard.getCardTxt().Split(char.Parse("_"));
            currentCardName = cardArray[0];
            currentCardType = cardArray[1];
            currentCardText = cardArray[2];

            cardNameTxt.text = currentCardName;
            cardTextTxt.text = currentCardText;
        }
        else
        {
            string[] cardArray = currentCard.getCardTxt().Split(char.Parse("_"));
            currentCardName = cardArray[0];
            currentCardType = cardArray[1];
            currentCardText = cardArray[2];

            cardNameTxt.text = currentCardName;
            cardTextTxt.text = currentCardText;
        }

    }*/

    public void nextCard()
    {
        //Debug.Log("next card");
        if (cards.Count > 0)
        {
            if (!isPassCard)
            {
                getNewCard();
                //updateCardData();
                cardCountTxt.enabled = true;
                updateCardNumberData();
            }
            else
            {
                cardCountTxt.enabled = false;
                //playerMaster.nextPlayer();
                getPassCard();
                //updateCardData();
            }
        }
        else
        {
            // game over
        }

    }

    void updateCardNumberData()
    {
        currentCardNum++;
        cardCountTxt.text = currentCardNum.ToString() + "/" + deckSize.ToString();
    }
}
