using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StonePuzzleBase : MonoBehaviour
{
    [SerializeField] private List<CellStones> _cells;
    [SerializeField] private List<CellStones> _cellsDataList;

    [SerializeField] private Door _exitDoor;

    public bool _isComplete = true;
    private void Start()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            _cells[i].cellActive += AddCells;
            _cells[i].cellDisActive += RemoveCells;
        }
    }

    private void RemoveCells(CellStones cells)
    {
        _cellsDataList.Remove(cells);
    }

    private void AddCells(CellStones cells)
    {
        _cellsDataList.Add(cells);
        if (_cellsDataList.Count == _cells.Count)
        {
            Debug.Log("Complete");
            CompletePuzzle();
        }
    }
    private void CompletePuzzle()
    {
        Debug.Log("CompletePuzzle");
        _exitDoor._locked = false;
    }
}
