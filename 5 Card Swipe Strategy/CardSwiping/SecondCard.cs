using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCard : MonoBehaviour
{
    SwipeEffect _swipeEffect;

    GameObject _firstCard;

    private void Start()
    {
        _swipeEffect = FindObjectOfType<SwipeEffect>();
        _swipeEffect.cardMoved += CardMovedFront;
        _firstCard = _swipeEffect.gameObject;

        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    private void Update()
    {
        float _distanceMoved = _firstCard.transform.localPosition.x;
        if (Mathf.Abs(_distanceMoved) > 0)
        {
            float step = Mathf.SmoothStep(0.8f, 1, Mathf.Abs(_distanceMoved) / (Screen.width / 2));
            transform.localScale = new Vector3(step, step, step);
        }
    }

    void CardMovedFront()
    {
        gameObject.AddComponent<SwipeEffect>();
        Destroy(this);
    }
}
