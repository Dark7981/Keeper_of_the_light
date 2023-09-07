using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBase : MonoBehaviour
{
    [SerializeField] private List<PuzzleCell> _cells;
    [SerializeField] private List<CellData> _correctSequence;
    [SerializeField] private List<CellData> _ñurrentSequence;


    void Start()
    {
        foreach (var cell in _cells) 
        {
            cell.Init();
            cell.cellActive += CheckCell;
        }
    }

    private void CheckCell(CellData data)
    {
        Debug.Log("CheckCell");
        _ñurrentSequence.Add(data);
        for (int i = 0; i< _ñurrentSequence.Count;i++)
        {
            if (_ñurrentSequence[i] != _correctSequence[i])
            {
                PuzzleReset();
                return;
            }   
        }
        if (_ñurrentSequence.Count == _correctSequence.Count)
        {
            CompletePuzzle();
        }
    }
    private void PuzzleReset()
    {
        Debug.Log("Puzzle Failed");
        _ñurrentSequence.Clear();
        foreach(var cell in _cells)
        {
            cell.SetActive(false);
        }
    }
    private void CompletePuzzle()
    {
        Debug.Log("Complete");
    }
}
