using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header(" Movement Settings ")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float speed = 8f;

    [Header(" Arrow Settings ")]
    [SerializeField] Transform firePosition;
    GameObject arrowObject;

    [SerializeField] Animator animator;

    private float horizontal = 0;
    private bool isFacingRight = true;
    bool isAlive = true;

    private void Start()
    {
        arrowObject = GameObject.FindWithTag("Arrow");
        arrowObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            horizontal -= 1;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            horizontal += 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            horizontal += 1;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            horizontal -= 1;
        }

        if (horizontal != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
        }

        Flip();
    }

    public void fire()
    {
        if (!arrowObject.activeSelf)
        {
            arrowObject.transform.position = firePosition.transform.position;
            arrowObject.SetActive(true);
        }
    }

    public void changeHorizontalValue(int val)
    {
        horizontal += val;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball" && isAlive)
        {
            Debug.Log("you die!");
            isAlive = false;
            SceneManager.LoadScene(0);
            //activate game over ui
        }
    }
}
