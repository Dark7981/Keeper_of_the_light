using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAdder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerLight"))
        {
            collision.GetComponent<LightScript>().StartTimer();
        }
    }
}
