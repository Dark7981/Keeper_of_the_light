
using System;
using UnityEngine;

public class EnemyKillZone : MonoBehaviour
{
    public static Action PlayerDead;
    public bool IsBoss = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!IsBoss)
            {
                collision.GetComponent<PlayerBehaviour>().Dead(transform, false);
                PlayerDead.Invoke();
            }
        }
    }
}
