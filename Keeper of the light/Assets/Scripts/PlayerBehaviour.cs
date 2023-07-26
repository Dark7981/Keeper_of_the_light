using System;
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
    [SerializeField] private float soundRange;
    [SerializeField] private float sittingSoundRange;
    [SerializeField] private float jumpSoundRange;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip sittingSound;
    [SerializeField] private SoundsDark _soundsDark;
    [SerializeField] private GameObject soundsDarkObject;
    [SerializeField] private AudioSource _audioSource;

    [Header("Needed data")]
    [SerializeField] private SourceOfNoise _sourceOfNoise;

    [Header("PrefabData")]
    [SerializeField] private Sprite sittingSprite;
    [SerializeField] private AudioSource jumpAudioSource;
    [SerializeField] private AudioSource sitAudioSource;
    [SerializeField] private AudioSource stepAudioSource;


    private int numberOfFootstep;
    private float range;
    private float speed;
    private bool isSiting = false;
    private Sprite standartSprite;  
    private Vector2 moveDirection;      
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private PlayerBehaviour _playerBehaviour;
    private Animator playerAnimator;
    private Vector3 _spawnPosition;
    private bool inJump = false;
    private bool readyToSit = true;


    public static Action menuOpened;

    private void Start()
    {
        _soundsDark = soundsDarkObject.GetComponent<SoundsDark>();
        UpdateController updateController = GameObject.FindGameObjectWithTag("UpdateController").GetComponent<UpdateController>();
        updateController._playerBehaviour = GetComponent<PlayerBehaviour>();

        if (_spawnPosition.x != PlayerPrefs.GetFloat("x") && _spawnPosition.y != PlayerPrefs.GetFloat("y"))
        {
            SpawnPoint();
            SpawnPlayer();
        }
        else if(!PlayerPrefs.HasKey("x")&&!PlayerPrefs.HasKey("y"))
        {
            _spawnPosition = new Vector3(0, 3, -10);
            SpawnPlayer();
        }
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
        StartCoroutine(FootstepSound());
    }

    public void ScriptUpdate()   
    {
        Movement();
        StartCoroutine(Siting());
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        if (Input.GetKeyDown(jumpKey) && !inJump && !isSiting)
        {
            inJump = true;
            jumpAudioSource.PlayOneShot(jumpSound);
            speed = moveSpeed / 1.5f;

            yield return new WaitForSeconds(0.5f);

            _sourceOfNoise.MakeNoise(transform.position, jumpSoundRange);
            inJump = false;
            speed = moveSpeed;
        }
    }

    private IEnumerator Siting() 
    {
        if (Input.GetKeyDown(sitKey) && !isSiting && readyToSit) // Присів
        {
            readyToSit = false;
            isSiting = true;
            sitAudioSource.PlayOneShot(sittingSound); 

            range = sittingSoundRange;
            speed = sittingMoveSpeed;
            _spriteRenderer.sprite = sittingSprite;

            yield return new WaitForSeconds(0.25f);

            readyToSit = true;
        }
        else if (Input.GetKeyDown(sitKey) && isSiting && readyToSit) // Cтоїть
        {
            readyToSit = false;
            isSiting = false;
            sitAudioSource.PlayOneShot(sittingSound);

            range = soundRange;
            speed = moveSpeed;
            
            _spriteRenderer.sprite = standartSprite;

            yield return new WaitForSeconds(0.25f);

            readyToSit = true; 
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
            _soundsDark.enabled = true;
            _audioSource.enabled = true;
        }
        else
        {
            _audioSource.enabled = false;
            _soundsDark.enabled = false;
            stepAudioSource.Stop();
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
        if (!stepAudioSource.isPlaying)
        {
            stepAudioSource.PlayOneShot(footstepSounds[numberOfFootstep]);
            if (numberOfFootstep + 1 < footstepSounds.Length)
            {
                numberOfFootstep++;
            }else
            {
                numberOfFootstep = 0;
            }
        }
    }
    
    public void ScriptFixedUpdate() 
    {
        _rigidBody.MovePosition(_rigidBody.position + moveDirection * speed * Time.fixedDeltaTime);  
    }

    public void Dead()
    {
        _playerBehaviour.enabled = false;
        GameObject.Find("UpdateController").GetComponent<UpdateController>().enabled = false;
        _spriteRenderer.color = Color.red;
        menuOpened.Invoke();
        gameObject.GetComponent<AudioSource>().enabled = false;
    }
    public void SpawnPoint()
    {
        _spawnPosition = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), -10);
    }
    public void SpawnPlayer()
    {
        gameObject.transform.position = _spawnPosition;
    }
}
