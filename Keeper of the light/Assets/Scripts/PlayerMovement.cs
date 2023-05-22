
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;  
    [SerializeField] private float rotationSpeed = 10;

    private Vector2 moveDirection;      // ������ ����
    private Rigidbody2D rigidBody;  

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();   // ���� ��������� 
    }

    public void ScriptUpdate()   // �� ����� ���� � ���� ��������������� � UpdateController 
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // ������ ���� ���������� �� ����

        if (moveDirection != Vector2.zero) //�������� �� ��������� ������ ����
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg; // ������� ��� �� �������� ����

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle); // ������������ ���� � ��������, � ��� � ���������

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // ������� ������� �� �������� ����
        }
    }

    public void ScriptFixedUpdate() // �� ����� ���� � ���� ��������������� � UpdateController 
    {
        rigidBody.MovePosition(rigidBody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);  // �� ����� ���� ������� ��'��� � ������� ����� � ������� ��������
    }
}
