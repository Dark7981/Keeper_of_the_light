
using UnityEngine;

public class StoneMove : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _rigidBody.bodyType = RigidbodyType2D.Kinematic;
            _rigidBody.velocity = Vector2.zero;
        }
    }


}
