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

    [Header("Needed data")]
    [SerializeField] private SourceOfNoise _sourceOfNoise;
    [SerializeField] private GameObject menuFolder;

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

    private void Start()
    {
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
        Siting();
        StartCoroutine(Jump());
        MenuUpdate();
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

    private void Siting() 
    {
        if (Input.GetKeyDown(sitKey) && isSiting == false) // Присів
        {
            isSiting = true;
            sitAudioSource.PlayOneShot(sittingSound); 

            range = sittingSoundRange;
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
    private void MenuUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuFolder.activeSelf == true)
        {
            MenuScript(1, false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuFolder.activeSelf == false)
        {
            MenuScript(0, true);
        }
    }
    public void MenuScript(int timeScale, bool menuActive)
    {
        Time.timeScale = timeScale;
        menuFolder.SetActive(menuActive);
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
        MenuScript(1, true);
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
