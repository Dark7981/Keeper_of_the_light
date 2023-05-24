
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private KeyCode sitKey = KeyCode.C;
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float sittingMoveSpeed = 2;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private Sprite sittingSprite;  //спрайт персонажу при сидінні

    private Sprite standartSprite;  // спрайт який був на гравці по замовчуванням
    private float speed = 4;
    private bool isSiting = false;
    private Vector2 moveDirection;      // напрям руху
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        standartSprite = spriteRenderer.sprite;

        if (sittingSprite == null) // цей код щоб під час ваших тестів не було помилок
        {
            sittingSprite = standartSprite;
        }
    }

    public void ScriptUpdate()   // це метод який я буду використовувати в UpdateController 
    {
        Movement();
        Siting();
    }

    private void Siting() // механіка присідання
    {
        if (Input.GetKeyDown(sitKey) && isSiting == false) // перевірка чи хочеш сісти або піднятись
        {
            isSiting = true;

            speed = sittingMoveSpeed;
            spriteRenderer.sprite = sittingSprite;
        

        }
        else if (Input.GetKeyDown(sitKey) && isSiting == true)  
        {
            isSiting = false;

            speed = moveSpeed; 
            spriteRenderer.sprite = standartSprite;
        }
    }

    private void Movement() // відровідає за рух гравця
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
        rigidBody.MovePosition(rigidBody.position + moveDirection * speed * Time.fixedDeltaTime);  // Це метод який переміщує об'єкт в указану точку з заданою швидкістю
    }
}
