using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] int ballSizeValue = 0;
    [SerializeField] string ballType;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool isActive;
    [SerializeField] float startForce = 20f;
    [SerializeField] float jumpForce = 100f;
    bool isFirstTime = true;
    float xAxisDirection, yTempValue;

    public static event Action onFruitSplitted;

    private void Awake()
    {
        gameObject.SetActive(isActive);
        xAxisDirection = Mathf.Sign(startForce) * 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Arrow" && isFirstTime)
        {
            isFirstTime = false;
            BallSplit.Instance.createSplittedBalls(transform, ballType, ballSizeValue);
            other.gameObject.SetActive(false);
            onFruitSplitted?.Invoke();
            Destroy(gameObject);
        }
        else if (other.tag == "Wallcheck")
        {
            xAxisDirection *= -1;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(startForce * xAxisDirection, jumpForce));
        }
        else if (other.gameObject.tag == "Wall")// wall
        {
            yTempValue = rb.velocity.y;
            rb.velocity = new Vector2(0f, yTempValue);
            xAxisDirection *= -1;
            rb.AddForce(new Vector2(startForce * xAxisDirection, 0f));
        }
    }

    public void addForceInSplit(int direction)
    {
        rb.AddForce(new Vector2(direction * startForce, jumpForce / 2));
    }


}
