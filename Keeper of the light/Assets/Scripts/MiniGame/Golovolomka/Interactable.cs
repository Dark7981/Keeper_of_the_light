using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchButtonVisible(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchButtonVisible(false);
        }
    }

    private void SwitchButtonVisible(bool state)
    {
        button.SetActive(state);
    }
    public virtual void Interact()
    {

    }
}
