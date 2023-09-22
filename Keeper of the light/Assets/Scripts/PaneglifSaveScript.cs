using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaneglifSaveScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _notes; 

    public static PaneglifSaveScript instance = null; // Ёкземпл€р объекта

    void Start()
    {
        // “еперь, провер€ем существование экземпл€ра
        if (instance == null)
        { // Ёкземпл€р менеджера был найден
            instance = this; // «адаем ссылку на экземпл€р объекта
        }
        if (instance == this)
        { // Ёкземпл€р объекта уже существует на сцене
            Destroy(gameObject); // ”дал€ем объект
        }

        // “еперь нам нужно указать, чтобы объект не уничтожалс€
        // при переходе на другую сцену игры
        DontDestroyOnLoad(gameObject);

        // » запускаем собственно инициализатор
    }

    public void Init()
    {
        
    }
    public void SetNotes(GameObject _notePrefabs)
    {
        _notes.Add(_notePrefabs);
    }
}
