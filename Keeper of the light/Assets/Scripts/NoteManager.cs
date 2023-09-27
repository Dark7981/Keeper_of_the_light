using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private List<string> _notes;
    public static NoteManager instance = null; // Ёкземпл€р объекта


    void Start()
    {
        //“еперь, провер€ем существование экземпл€ра
        if (instance == null)
        { // Ёкземпл€р менеджера был найден
            instance = this; // «адаем ссылку на экземпл€р объекта
        }
        else if (instance != this)
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
        var _interfaceController = GameObject.Find("InterfaceController");
        _interfaceController.GetComponent<InterfaceController>().InitUINotes(_notes, gameObject);
    }
    public void SetInNoteList(string note)
    {
        _notes.Add(note);
    }
}
