using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLimitController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Arrow")
        { 
            other.gameObject.SetActive(false);
        }
    }
}
