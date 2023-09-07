using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCell : Interactable
{
    [SerializeField] private CellData _cellData;
    [SerializeField] private SpriteRenderer _cellIcon;
    public Action<CellData> cellActive;
    
    public void Init()
    {
        _cellIcon.sprite = _cellData.icon;
    }

    public override void Interact()
    {
        Debug.Log("Interact");
        cellActive?.Invoke(_cellData);
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
}
