using System;
using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Mechanics")]
    [SerializeField] private bool sit = true;
    [SerializeField] private bool jump = true;

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
    public int doorKey;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip sittingSound;
    [SerializeField] private AudioSource _audioSource;
    private float _volume;
    private float _volumePart;

    [Header("Needed data")]
    [SerializeField] private SourceOfNoise _sourceOfNoise;
    [SerializeField] private Transform _startSpawnPoint;
    [SerializeField] private bool _useSpawnPoint;

    [Header("PrefabData")]
    [SerializeField] private Sprite sittingSprite;
    [SerializeField] private AudioSource jumpAudioSource;
    [SerializeField] private AudioSource sitAudioSource;
    [SerializeField] private AudioSource stepAudioSource;
    [SerializeField] private LightScript _ligth;

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
    public bool freezeMovement;
    public bool playerDead = false;

    public static Action menuOpened;

    private void OnEnable()
    {
        SubtitlesController.FreezePlayer += FreezeMovement;
    }
    private void OnDisable()
    {
        SubtitlesController.FreezePlayer -= FreezeMovement;
    }

    private void Start()
    {
        _volume = _audioSource.volume;
        _volumePart = 0.001f;
        UpdateController updateController = GameObject.FindGameObjectWithTag("UpdateController").GetComponent<UpdateController>();
        updateController._playerBehaviour = GetComponent<PlayerBehaviour>();

        if (_spawnPosition.x != PlayerPrefs.GetFloat("x") && _spawnPosition.y != PlayerPrefs.GetFloat("y") && _useSpawnPoint == false)
        {
            SpawnPoint();
            SpawnPlayer();
        }
        else
        {
            _spawnPosition = _startSpawnPoint.position;
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
        if (freezeMovement)
        {
            Movement();
        }
        StartCoroutine(Siting());
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        if (Input.GetKeyDown(jumpKey) && !inJump && !isSiting && jump)
        {
            inJump = true;
            playerAnimator.SetBool("Jump", inJump);
            jumpAudioSource.PlayOneShot(jumpSound);
            speed = moveSpeed / 1.5f;

            yield return new WaitForSeconds(0.5f);

            _sourceOfNoise.MakeNoise(transform.position, jumpSoundRange);
            inJump = false;
            playerAnimator.SetBool("Jump", inJump);
            speed = moveSpeed;
        }
    }

    private IEnumerator Siting() 
    {
        if (Input.GetKeyDown(sitKey) && !isSiting && readyToSit && sit) // Присів
        {
            readyToSit = false;
            isSiting = true;
            playerAnimator.SetBool("Crouch", isSiting);
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
            playerAnimator.SetBool("Crouch", isSiting);
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
            _audioSource.mute = false;
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            _sourceOfNoise.MakeNoise(transform.position, range);

            playerAnimator.SetBool("isRunning", true);
            StopCoroutine(MuteVolume());
            _audioSource.volume = 0.15f;

        }
        else
        {
            stepAudioSource.Stop();
            playerAnimator.SetBool("isRunning", false);
            StartCoroutine(MuteVolume());
        }
    }

    private IEnumerator MuteVolume()
    {
     
       for (int i = 0; i < 8; i++)
       {
           _audioSource.volume -= _volumePart;
           yield return new WaitForSeconds(4f);        
       }
    }
    private IEnumerator FootstepSound()
    {
        while (true)
        {
            if (freezeMovement)
            {
                MovementDetection();
            }
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
    {   if (freezeMovement)
        {
            _rigidBody.MovePosition(_rigidBody.position + moveDirection * speed * Time.fixedDeltaTime);
        }
    }

    public void Dead(Transform positionTrap, bool TeleportToKiller)
    {
        LightDead();
        _playerBehaviour.enabled = false;
        GameObject.Find("UpdateController").GetComponent<UpdateController>().enabled = false;
        if (TeleportToKiller)
        {
            transform.position = positionTrap.position;
        }
        playerAnimator.PlayInFixedTime("Death");
        gameObject.GetComponent<AudioSource>().enabled = false;
        playerDead = true ;
    }
    public void LightDead()
    {
        _ligth.DeadLight();
    }
    public void menuOpenedInvoke()
    {
        menuOpened.Invoke();
    }
    public void SpawnPoint()
    {
        _spawnPosition = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), -10);
    }
    public void SpawnPlayer()
    {
        gameObject.transform.position = _spawnPosition;
    }
    public void FreezeMovement(bool freeze)
    {
        freezeMovement = freeze;
        playerAnimator.SetBool("isRunning", false);
    }
}
