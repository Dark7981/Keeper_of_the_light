
using UnityEngine;

public class TrapTriger : MonoBehaviour
{
    [SerializeField] private bool Activated;

    [Header("Keys")]
    [SerializeField] private KeyCode takeKey = KeyCode.E;
    [SerializeField] private KeyCode activateKey = KeyCode.F;

    [Header("Needed data")]
    [SerializeField] private Sprite activeTrap;
    [SerializeField] private Sprite disactiveTrap;

    private TrapInventoryScript _trapInventoryScript;
    private SpriteRenderer _springsRenderer;


    private void Start()
    {
        _springsRenderer = GetComponent<SpriteRenderer>();
        _trapInventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<TrapInventoryScript>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (Activated)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerBehaviour>().Dead();
                DisActiveTrapSprite();
            }
            else if (col.CompareTag("Enemy"))
            {
                col.GetComponent<RegularEnemy>().Dead();
                DisActiveTrapSprite();
            }
        }
        else
        {
            if (col.CompareTag("Player"))
            {
                _trapInventoryScript.ShowButton();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (_trapInventoryScript)
        {
            _trapInventoryScript.HideButton();
        }
    }

    private void Update()
    {
        if (_trapInventoryScript)
        {
            if (_trapInventoryScript.TrapButtons.active)
            {
                TrapPicker();
            }
        }


    }
    
    private void TrapPicker()
    {
        if (!Activated)
        {
            if (Input.GetKeyDown(takeKey))
            {
                Destroy(gameObject, 0.1f);
                _trapInventoryScript.TakeTrap();
            }
            else if (Input.GetKeyDown(activateKey))
            {
                ActiveTrapSprite();
            }
        }
    }
    private void ActiveTrapSprite()
    {
        _springsRenderer.sprite = activeTrap;
        Activated = true;

        _trapInventoryScript.HideButton();
    }

    private void DisActiveTrapSprite()
    {
        _springsRenderer.sprite = disactiveTrap;
        Activated = false;
    }
}