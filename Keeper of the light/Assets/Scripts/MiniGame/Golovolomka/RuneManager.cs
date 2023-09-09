using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    [SerializeField] private List<CellData> _playerRunes;
    [SerializeField] private PlayerBehaviour _player;
    public static RuneManager instance = null; // ��������� �������

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        PlayerBehaviour.newRune += SetRuneList;
        PlayerBehaviour.sceneSwitch += InitializeManager;
        // ������, ��������� ������������� ����������
        if (instance == null)
        { // ��������� ��������� ��� ������
            instance = this; // ������ ������ �� ��������� �������
        }
        else if (instance == this)
        { // ��������� ������� ��� ���������� �� �����
            Destroy(gameObject); // ������� ������
        }

        // ������ ��� ����� �������, ����� ������ �� �����������
        // ��� �������� �� ������ ����� ����
        DontDestroyOnLoad(gameObject);

        // � ��������� ���������� �������������
    }

    // ����� ������������� ���������
    private void InitializeManager()
    {
        foreach (var rune in _playerRunes)
        {
            _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
            _player.InitRune(rune);
        }
    }
    private void InitNewRune(CellData rune)
    {
        _player.InitRune(rune);
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
}
