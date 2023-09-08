using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellStones : MonoBehaviour
{
    [SerializeField] private StoneData _requiredStone;
    private Animator _animator;

    public Action<CellStones> cellActive;
    public Action<CellStones> cellDisActive;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Stone>())
        {
            if (collision.GetComponent<Stone>().GetData() == _requiredStone)
            {
                cellActive?.Invoke(gameObject.GetComponent<CellStones>());
                _animator.SetTrigger("Correct");
            }else
                _animator.SetTrigger("Wrong");
                

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Stone>())
            cellDisActive?.Invoke(gameObject.GetComponent<CellStones>());
    }
}
