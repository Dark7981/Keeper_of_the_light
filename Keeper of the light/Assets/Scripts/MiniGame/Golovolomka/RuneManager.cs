using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    [SerializeField] private List<CellData> _playerRunes;
    [SerializeField] private PlayerBehaviour _player;
    public static RuneManager instance = null; // Ёкземпл€р объекта

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        PlayerBehaviour.newRune += SetRuneList;
        PlayerBehaviour.sceneSwitch += InitializeManager;
        // “еперь, провер€ем существование экземпл€ра
        if (instance == null)
        { // Ёкземпл€р менеджера был найден
            instance = this; // «адаем ссылку на экземпл€р объекта
        }
        else if (instance == this)
        { // Ёкземпл€р объекта уже существует на сцене
            Destroy(gameObject); // ”дал€ем объект
        }

        // “еперь нам нужно указать, чтобы объект не уничтожалс€
        // при переходе на другую сцену игры
        DontDestroyOnLoad(gameObject);

        // » запускаем собственно инициализатор
    }

    // ћетод инициализации менеджера
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
