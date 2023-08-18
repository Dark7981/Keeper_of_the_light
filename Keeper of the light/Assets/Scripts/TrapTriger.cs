using UnityEngine;

public class TrapTriger : MonoBehaviour
{
    [SerializeField] private AudioSource AudioActivateTrap;
    [SerializeField] private AudioSource AudioDisactiveTrap;

    [SerializeField] private bool _activatedBool;
    [SerializeField] private bool _destroyBool;

    [Header("Keys")]
    [SerializeField] private KeyCode takeKey = KeyCode.E;
    [SerializeField] private KeyCode activateKey = KeyCode.F;

    [Header("Needed data")]
    [SerializeField] private Sprite _activeTrap;
    [SerializeField] private Sprite _disactiveTrap;
    [SerializeField] private Sprite _destroyTrap;
    public GameObject trapButton;

    private TrapInventoryScript _trapInventoryScript;
    private SpriteRenderer _spritesRenderer;


    private void Start()
    {
        if (_destroyBool&&_activatedBool)
            _activatedBool = false;
        _spritesRenderer = GetComponent<SpriteRenderer>();
        _trapInventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<TrapInventoryScript>();
        _spritesRenderer.sprite = _destroyBool ? _destroyTrap : _activatedBool ? _activeTrap : _disactiveTrap;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_activatedBool)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerBehaviour>().Dead(transform,true);
                DisActiveTrapSprite();
                _destroyBool = true;
            }
            else if (col.CompareTag("Enemy"))
            {
                if (col.GetComponent<RegularEnemy>())
                {
                    col.GetComponent<RegularEnemy>().Dead(transform);
                }
                DisActiveTrapSprite();
                _destroyBool = true;
            }
        }
        else
        {
            if (col.CompareTag("Player"))
            {
                if(_destroyBool == false)
                    _trapInventoryScript.ShowButton(trapButton);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (_trapInventoryScript)
        {
            _trapInventoryScript.HideButton(trapButton);
        }
    }

    private void Update()
    {
        if (_trapInventoryScript)
        {
            if (trapButton.active)
            {
                TrapPicker();
            }
        }


    }
    
    private void TrapPicker()
    {
        if (!_activatedBool && _destroyBool == false)
        {
            if (Input.GetKeyDown(takeKey))
            {
                Destroy(gameObject, 0.1f);
                _trapInventoryScript.TakeTrap(trapButton);
            }
            else if (Input.GetKeyDown(activateKey))
            {
                ActiveTrapSprite();
                AudioActivateTrap.Play();
            }
        }
    }
    private void ActiveTrapSprite()
    {
        _spritesRenderer.sprite = _activeTrap;
        _activatedBool = true;

        _trapInventoryScript.HideButton(trapButton);
    }

    private void DisActiveTrapSprite()
    {
        _spritesRenderer.sprite = _destroyTrap;
        _activatedBool = false;

        AudioDisactiveTrap.Play();
    }
}