using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCardInstantiator : MonoBehaviour
{
    [Header(" Game Object References ")]
    [SerializeField] GameObject shopCardPrefab;
    [SerializeField] GameObject shopCardParentObject;
    [SerializeField] GameObject workCardPrefab;
    [SerializeField] GameObject workCardParentObject;
    [SerializeField] GameObject horizontalLayoutObject;
    [SerializeField] GameObject leftArrowButton;
    [SerializeField] GameObject rightArrowButton;

    ShopCard[] shopCardPool;
    WorkCard[] workCardPool;

    int initialPoolSize = 10;
    int activeCardSize;
    int currentIndex = 0;
    string cardType;

    public static HorizontalCardInstantiator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        shopCardPool = new ShopCard[initialPoolSize];
        workCardPool = new WorkCard[initialPoolSize];
        for (int i = 0; i < initialPoolSize; i++)
        {
            shopCardPool[i] = Instantiate(shopCardPrefab, shopCardParentObject.transform).GetComponent<ShopCard>();
            shopCardPool[i].gameObject.SetActive(false);

            workCardPool[i] = Instantiate(workCardPrefab, workCardParentObject.transform).GetComponent<WorkCard>();
            workCardPool[i].gameObject.SetActive(false);
        }

    }

    public void updateHorizontalLayoutContent(MenuCardSO[] cards)
    {
        int i = 0;
        cardType = cards[0].getCardType();
        
        switch (cardType)
        {
            case "shopCard":
                workCardParentObject.SetActive(false);
                shopCardPool[currentIndex].gameObject.SetActive(false); // disable the card from previous cycle

                foreach (var card in cards)
                {
                    shopCardPool[i].initCardValues(card);
                    i++;
                }

                activeCardSize = i;
                shopCardPool[0].gameObject.SetActive(true);
                currentIndex = 0;
                horizontalLayoutObject.SetActive(true);
                leftArrowButton.SetActive(false);
                rightArrowButton.SetActive(true);
                break;

            case "workCard":
                shopCardParentObject.SetActive(false);
                workCardPool[currentIndex].gameObject.SetActive(false); // disable the card from previous cycle

                foreach (var card in cards)
                {
                    workCardPool[i].initCardValues(card);
                    i++;
                }

                activeCardSize = i;
                workCardPool[0].gameObject.SetActive(true);
                currentIndex = 0;
                horizontalLayoutObject.SetActive(true);
                leftArrowButton.SetActive(false);
                rightArrowButton.SetActive(true);
                break;
        }




    }

    public void moveToNextHorizontalObject()
    {
        switch (cardType)
        {
            case "shopCard":
                if (currentIndex + 1 < activeCardSize)
                {
                    shopCardPool[++currentIndex].gameObject.SetActive(true);
                    shopCardPool[currentIndex - 1].gameObject.SetActive(false);
                    changeArrowButtonsState(currentIndex);
                }
                break;

            case "workCard":
                if (currentIndex + 1 < activeCardSize)
                {
                    workCardPool[++currentIndex].gameObject.SetActive(true);
                    workCardPool[currentIndex - 1].gameObject.SetActive(false);
                    changeArrowButtonsState(currentIndex);
                }
                break;
        }

    }

    public void moveToPreviousHorizontalObject()
    {
        switch (cardType)
        {
            case "shopCard":
                if (currentIndex - 1 >= 0)
                {
                    shopCardPool[--currentIndex].gameObject.SetActive(true);
                    shopCardPool[currentIndex + 1].gameObject.SetActive(false);
                    changeArrowButtonsState(currentIndex);
                }
                break;

            case "workCard":
                if (currentIndex - 1 >= 0)
                {
                    workCardPool[--currentIndex].gameObject.SetActive(true);
                    workCardPool[currentIndex + 1].gameObject.SetActive(false);
                    changeArrowButtonsState(currentIndex);
                }
                break;
        }
    }

    public void changeArrowButtonsState(int state)
    {
        if (state == 0)
        {
            leftArrowButton.SetActive(false);
        }
        else if (state == 1)
        {
            leftArrowButton.SetActive(true);
        }
        else if (state == activeCardSize - 1)
        {
            rightArrowButton.SetActive(false);
        }
        else if (state == activeCardSize - 2)
        {
            rightArrowButton.SetActive(true);
        }

    }
}
