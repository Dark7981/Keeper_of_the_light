
using System;
using UnityEngine;

public class EnemyKillZone : MonoBehaviour
{
    public static Action PlayerDead;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().Dead(transform,false);
            PlayerDead.Invoke();
        }
    }
}
