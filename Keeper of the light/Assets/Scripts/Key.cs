using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().doorKey += 1;
            PlayerPrefs.SetInt("Key", collision.GetComponent<PlayerBehaviour>().doorKey);
            Destroy(gameObject);
        }
    }
}
