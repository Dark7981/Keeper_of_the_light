
using UnityEngine;

public class EnemyKillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().Dead();
        }
    }
}
