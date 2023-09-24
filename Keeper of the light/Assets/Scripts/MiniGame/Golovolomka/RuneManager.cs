using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    [SerializeField] private List<CellData> _playerRunes;
    public PlayerBehaviour _player;
    public static RuneManager instance = null; // ��������� �������
    public RuneManager _runeManager;

    public Action<CellData> _runeUI;
    private void Awake()
    {
        if (instance == null)
        { // ��������� ��������� ��� ������
            instance = this; // ������ ������ �� ��������� �������
        }
        else if (instance != this)
        { // ��������� ������� ��� ���������� �� �����
            Destroy(gameObject); // ������� ������
        }
        DontDestroyOnLoad(gameObject);
        instance._player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        PlayerBehaviour.newRune += SetRuneList;
        PlayerBehaviour.sceneSwitch += InitializeManager;
        _runeManager = instance;
    }
    // ����� ������������� ���������
    private void InitializeManager()
    {
        foreach (var rune in _playerRunes)
        {
            InitNewRune(rune);
        }
    }
    private void InitNewRune(CellData rune)
    {
        instance._player.InitRune(rune);
        _runeUI?.Invoke(rune);
    }
    private void SetRuneList(CellData rune)
    {
        if (rune != null)
        {
            bool isNewRune = true;
            foreach (var runesInList in _playerRunes)
            {
                if (runesInList == rune)
                {
                    isNewRune = false;
                    return;
                }
            }
            if (isNewRune)
            {
                _playerRunes.Add(rune);
                InitNewRune(rune);
            }
        }
        else
            InitializeManager();
    }
    public List<CellData> GetRunes()
    {
        return _playerRunes;
    }
}
