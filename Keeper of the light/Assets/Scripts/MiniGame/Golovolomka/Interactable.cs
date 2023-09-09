using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject button;
    public GameObject _player;
    public bool _buttonActive;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = collision.gameObject;
            _buttonActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _buttonActive = false;
            SwitchButtonVisible(false);
        }
    }
    private void CheckActive()
    {
        if (_buttonActive)
        {
            SwitchButtonVisible(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
               
        } 
    }
    private void Update()
    {
        CheckActive();
    }
    private void SwitchButtonVisible(bool state)
    {
        button.SetActive(state);
    }
    public virtual void Interact()
    {

    }
}
