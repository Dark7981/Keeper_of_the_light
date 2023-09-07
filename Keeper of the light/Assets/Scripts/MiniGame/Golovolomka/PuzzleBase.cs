using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBase : MonoBehaviour
{
    [SerializeField] private List<PuzzleCell> _cells;
    [SerializeField] private List<CellData> _correctSequence;
    [SerializeField] private List<CellData> _�urrentSequence;


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
        _�urrentSequence.Add(data);
        for (int i = 0; i< _�urrentSequence.Count;i++)
        {
            if (_�urrentSequence[i] != _correctSequence[i])
            {
                PuzzleReset();
                return;
            }   
        }
        if (_�urrentSequence.Count == _correctSequence.Count)
        {
            CompletePuzzle();
        }
    }
    private void PuzzleReset()
    {
        Debug.Log("Puzzle Failed");
        _�urrentSequence.Clear();
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
