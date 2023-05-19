
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;  // швидкість руху

    private Vector2 direction;      // напрям руху
    private Rigidbody2D rigidBody;  

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();  // беру компонент 
    }

    public void ScriptUpdate() // Це метод який я буду використовувати в UpdateController 
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // Напрям руху записується по осям
    }

    public void ScriptFixedUpdate() // Це метод який я буду використовувати в UpdateController 
    {
        rigidBody.MovePosition(rigidBody.position + direction * moveSpeed * Time.fixedDeltaTime);  // Це метод який переміщує об'єкт в указану точку з заданою швидкістю
    }
}
