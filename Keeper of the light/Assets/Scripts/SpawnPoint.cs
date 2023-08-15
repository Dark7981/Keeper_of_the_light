using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("x",gameObject.transform.position.x);
            PlayerPrefs.SetFloat("y",gameObject.transform.position.y);
            collision.GetComponent<PlayerBehaviour>().SpawnPoint();
            Debug.Log("SpawnPointEnter");
        }
    }
}
