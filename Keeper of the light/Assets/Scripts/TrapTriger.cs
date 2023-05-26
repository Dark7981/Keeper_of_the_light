using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriger : MonoBehaviour
{
    
    [SerializeField] private Dead _dead;

  

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _dead.dead();
        }
    }
}
