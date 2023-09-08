using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCell : Interactable
{
    [SerializeField] private CellData _cellData;
    [SerializeField] private SpriteRenderer _cellIcon;

    private Animator _animator;

    public Action<CellData,PuzzleCell> cellActive;
    
    public void Init(CellData _cellDataBase)
    {
        _cellData = _cellDataBase;
        _cellIcon.sprite = _cellData.icon;
        _animator = GetComponent<Animator>();
    }
    public override void Interact()
    {
        Debug.Log("Interact");
        cellActive?.Invoke(_cellData,gameObject.GetComponent<PuzzleCell>());
    }
    public void SetActive(bool state)
    {
        if (state)
        {
            Debug.Log($"Active");
        }
        else
        {
            Debug.Log("Disable");
        }
    }
    public void ReactionChoise(bool correct)
    {
        _animator.SetTrigger(correct ? "Correct" : "Wrong");
    }
}
