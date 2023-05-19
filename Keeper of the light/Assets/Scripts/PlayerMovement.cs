
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;  // �������� ����

    private Vector2 direction;      // ������ ����
    private Rigidbody2D rigidBody;  

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();  // ���� ��������� 
    }

    public void ScriptUpdate() // �� ����� ���� � ���� ��������������� � UpdateController 
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // ������ ���� ���������� �� ����
    }

    public void ScriptFixedUpdate() // �� ����� ���� � ���� ��������������� � UpdateController 
    {
        rigidBody.MovePosition(rigidBody.position + direction * moveSpeed * Time.fixedDeltaTime);  // �� ����� ���� ������� ��'��� � ������� ����� � ������� ��������
    }
}
