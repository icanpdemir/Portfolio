using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float arrowSpeed = 100f;

    void FixedUpdate() {
        rb.velocity = new Vector2(rb.velocity.x, arrowSpeed * Time.deltaTime);
    }
}
