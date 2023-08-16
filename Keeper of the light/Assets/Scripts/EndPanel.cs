
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.SetActive(true);
        }
    }
}
