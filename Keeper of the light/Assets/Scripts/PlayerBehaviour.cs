using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sitKey = KeyCode.C;

    [Header("Parameters")]
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float sittingMoveSpeed = 2;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float sittingSoundRange;
    [SerializeField] private float soundRange;
    [SerializeField] private float jumpSoundRange;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip jumpSound;

    [Header("Needed data")]
    [SerializeField] private Sprite sittingSprite;
    [SerializeField] private SourceOfNoise _sourceOfNoise;

    private int numberOfFootstep;
    private float range;
    private float speed;
    private bool isSiting = false;
    private AudioSource _audioSource;
    [SerializeField] private AudioSource _jumpSource;
    [SerializeField] private GameObject menuFolder;
    private Sprite standartSprite;  
    private Vector2 moveDirection;      
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private PlayerBehaviour _playerBehaviour;
    private Animator playerAnimator;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerBehaviour = GetComponent<PlayerBehaviour>();
        playerAnimator = GetComponent<Animator>();
        range = soundRange;
        speed = moveSpeed;

        standartSprite = _spriteRenderer.sprite; 

        if (sittingSprite == null) 
        {
            sittingSprite = standartSprite;
        }
        _audioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(FootstepSound());
    }

    public void ScriptUpdate()   
    {
        Movement();
        Siting();
        Jump();
        MenuUpdate();
    }

    private void Jump()
    {
        if (!isSiting && Input.GetKeyDown(jumpKey))
        {
            Debug.Log("111");
            _jumpSource.PlayOneShot(jumpSound);
            _sourceOfNoise.MakeNoise(transform.position, jumpSoundRange);
            Debug.Log("222");
        }
    }

    private void Siting() 
    {
        if (Input.GetKeyDown(sitKey) && isSiting == false) // Присів
        {
            isSiting = true;

            soundRange = sittingSoundRange;
            speed = sittingMoveSpeed;

            _spriteRenderer.sprite = sittingSprite;
        }
        else if (Input.GetKeyDown(sitKey) && isSiting == true) // Cтоїть
        {
            isSiting = false;

            range = soundRange;
            speed = moveSpeed;
            
            _spriteRenderer.sprite = standartSprite;
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

            _sourceOfNoise.MakeNoise(transform.position, range);

            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            _audioSource.Stop();
            playerAnimator.SetBool("isRunning", false);
        }
    }
    private IEnumerator FootstepSound()
    {
        while (true)
        {
            MovementDetection();
            yield return new WaitForSeconds(0.3f);
        }
        
    }
    private void MovementDetection()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(footstepSounds[numberOfFootstep]);
            if (numberOfFootstep + 1 < footstepSounds.Length)
            {
                numberOfFootstep++;
            }else
            {
                numberOfFootstep = 0;
            }
        }
    }
    private void MenuUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuFolder.activeSelf == true)
        {
            Time.timeScale = 1;
            menuFolder.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuFolder.activeSelf == false)
        {
            Time.timeScale = 0;
            menuFolder.SetActive(true);
        }
    }
    public void ScriptFixedUpdate() 
    {
        _rigidBody.MovePosition(_rigidBody.position + moveDirection * speed * Time.fixedDeltaTime);  
    }

    public void Dead()
    {
        Destroy(_playerBehaviour);
        _spriteRenderer.color = Color.red;
    }
}
