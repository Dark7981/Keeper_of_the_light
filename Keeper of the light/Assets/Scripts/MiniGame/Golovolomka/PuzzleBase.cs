using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBase : MonoBehaviour
{
    [SerializeField] private List<PuzzleCell> _cells;
    [SerializeField] private List<CellData> _cellsDataList;
    [SerializeField] private List<CellData> _correctSequence;
    [SerializeField] private List<CellData> _ñurrentSequence;
    [SerializeField] private Door _exitDoor;
    [SerializeField] private bool needRune;
    [SerializeField] private List<int> _puzzles;
    private bool _isComplete = true;


    void Start()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            _puzzles.Add(i);
        }
        foreach (var cell in _cells)
        {
            int cellDataRandom = UnityEngine.Random.Range(0, _puzzles.Count);
            Debug.Log($"{cellDataRandom} RandomCell");
            var cellData = _cellsDataList[_puzzles[cellDataRandom]];
                _puzzles.RemoveAt(cellDataRandom);
                cell.Init(cellData);
                cell.cellActive += CheckCell;
        }
    }

    private void CheckCell(CellData data, PuzzleCell cell, PlayerBehaviour _playerBehaviour)
    {
        if (_isComplete && needRune == true ? _playerBehaviour.CompareRune(data) : _isComplete) 
        {
            Debug.Log("CheckCell");
            _ñurrentSequence.Add(data);
            for (int i = 0; i < _ñurrentSequence.Count; i++)
            {
                if (_ñurrentSequence[i] != _correctSequence[i])
                {
                    PuzzleReset();
                    cell.ReactionChoise(false);
                    return;
                }
            }
            cell.ReactionChoise(true);
            if (_ñurrentSequence.Count == _correctSequence.Count)
            {
                CompletePuzzle();
            }
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
        _isComplete = false;
        _exitDoor._locked = false;
        Debug.Log("Complete");
    }
}
