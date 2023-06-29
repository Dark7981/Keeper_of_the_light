using TMPro;
using UnityEngine;

public class TrapTriger : MonoBehaviour
{
    [SerializeField] private bool Activated;
    private TrapInventoryScript _trapInventoryScript;
    [SerializeField] private Sprite activeTrap;
    [SerializeField] private Sprite disactiveTrap;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<TrapInventoryScript>())
        {
            _trapInventoryScript = col.GetComponent<TrapInventoryScript>();
        }
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
            if (col.GetComponent<TrapInventoryScript>())
            {
                _trapInventoryScript.ShowButton();
            }
        }
    }
    private void Update()
    {
        if (_trapInventoryScript)
        {
            if (_trapInventoryScript.PressButton.active)
            {
                TrapPicker();
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
    private void TrapPicker()
    {
        if (!Activated)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(gameObject, 0.1f);
                _trapInventoryScript.TakeTrap();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                ActiveTrapSprite();

            }
        }
    }
    private void ActiveTrapSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = activeTrap;
        Activated = true;
        _trapInventoryScript.HideButton();
    }
    private void DisActiveTrapSprite()
    {
        Activated = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = disactiveTrap;
    }
}