using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private KeyCode sitKey = KeyCode.C;
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float sittingMoveSpeed = 2;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private Sprite sittingSprite;//������ ��������� ��� �����
    [SerializeField] private AudioClip[] footsteps;//������ �����

    private bool isMoving = false;
    private AudioSource playerAudio;
    private Sprite standartSprite;  // ������ ���� ��� �� ������ �� �������������
    private float speed = 4;
    private bool isSiting = false;
    private Vector2 moveDirection;      // ������ ����
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        standartSprite = spriteRenderer.sprite;

        if (sittingSprite == null) // ��� ��� ��� �� ��� ����� ����� �� ���� �������
        {
            sittingSprite = standartSprite;
        }
    }

    public void ScriptUpdate()   // �� ����� ���� � ���� ��������������� � UpdateController 
    {
        Movement();
        Siting();

    }

    private void Siting() // ������� ���������
    {
        if (Input.GetKeyDown(sitKey) && isSiting == false) // �������� �� ����� ���� ��� ��������
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

    private void Movement() // ������� �� ��� ������
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // ������ ���� ���������� �� ����

        if (moveDirection != Vector2.zero) //�������� �� ��������� ������ ����
        {
            isMoving = true;

            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg; // ������� ��� �� �������� ����

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle); // ������������ ���� � ��������, � ��� � ���������

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // ������� ������� �� �������� ����
            
            StartCoroutine(Footstep());
        }else
        {
            playerAudio.Stop();
        }
    }
    public IEnumerator Footstep()
    {
        yield return new WaitForSeconds(0.1f); 
        MovementDetection();
        yield return new WaitForSeconds(0.4f);
        StopCoroutine(Footstep());
    }
    private void MovementDetection()
    {
        if (!playerAudio.isPlaying && isMoving)
        {
            int rnd = Random.Range(0, footsteps.Length);
            playerAudio.PlayOneShot(footsteps[rnd]);
        }
    }

    public void ScriptFixedUpdate() // �� ����� ���� � ���� ��������������� � UpdateController 
    {
        rigidBody.MovePosition(rigidBody.position + moveDirection * speed * Time.fixedDeltaTime);  // �� ����� ���� ������� ��'��� � ������� ����� � ������� ��������
    }
}
