
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;  
    [SerializeField] private float rotationSpeed = 10;

    private Vector2 moveDirection;      // напрям руху
    private Rigidbody2D rigidBody;  

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();   // беру компонент 
    }

    public void ScriptUpdate()   // це метод який я буду використовувати в UpdateController 
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // Напрям руху записується по осям

        if (moveDirection != Vector2.zero) //перевірка чи змінюється напрям руху
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg; // визначає кут до напрямку руху

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle); // перетворення кута в градусах, в кут в кватерніоні

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // плавний поворот до напрямку руху
        }
    }

    public void ScriptFixedUpdate() // Це метод який я буду використовувати в UpdateController 
    {
        rigidBody.MovePosition(rigidBody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);  // Це метод який переміщує об'єкт в указану точку з заданою швидкістю
    }
}
