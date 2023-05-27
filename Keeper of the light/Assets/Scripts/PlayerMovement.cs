using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Data settings")]
    [SerializeField] private KeyCode sitKey = KeyCode.C;
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float sittingMoveSpeed = 2;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float sittingSoundRange;
    [SerializeField] private float soundRange;

    [Header("Needed data")]
    [SerializeField] private Sprite sittingSprite;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private SourceOfNoise _sourceOfNoise;

    private float range;
    private float speed;
    private bool isSiting = false;
    private AudioSource playerAudio;
    private Sprite standartSprite;  
    private Vector2 moveDirection;      
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        range = soundRange;
        speed = moveSpeed;

        standartSprite = spriteRenderer.sprite; 

        if (sittingSprite == null) 
        {
            sittingSprite = standartSprite;
        }
    }

    public void ScriptUpdate()   
    {
        Movement();
        Siting();

    }

    private void Siting() 
    {
        if (Input.GetKeyDown(sitKey) && isSiting == false) // Присів
        {
            isSiting = true;

            soundRange = sittingSoundRange;
            speed = sittingMoveSpeed;

            spriteRenderer.sprite = sittingSprite;
        }
        else if (Input.GetKeyDown(sitKey) && isSiting == true) // Cтоїть
        {
            isSiting = false;

            range = soundRange;
            speed = moveSpeed;
            
            spriteRenderer.sprite = standartSprite;
        }
    }

    private void Movement() 
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 

        if (moveDirection != Vector2.zero) 
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg; 

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle); 

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 

            StartCoroutine(FootstepSound());

            _sourceOfNoise.MakeNoise(transform.position,soundRange);
        }else
        {
            playerAudio.Stop();
        }
    }
    private IEnumerator FootstepSound()
    {
        MovementDetection();
        yield return new WaitForSeconds(0.4f);
        StopCoroutine(FootstepSound());
    }
    private void MovementDetection()
    {
        if (!playerAudio.isPlaying)
        {
            int number = Random.Range(0, footstepSounds.Length);
            playerAudio.PlayOneShot(footstepSounds[number]);
        }
    }

    public void ScriptFixedUpdate() 
    {
        rigidBody.MovePosition(rigidBody.position + moveDirection * speed * Time.fixedDeltaTime);  
    }
}
