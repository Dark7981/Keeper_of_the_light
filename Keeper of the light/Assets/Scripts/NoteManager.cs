using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private List<string> _notes;
    public static NoteManager instance = null; // ��������� �������


    void Start()
    {
        //������, ��������� ������������� ����������
        if (instance == null)
        { // ��������� ��������� ��� ������
            instance = this; // ������ ������ �� ��������� �������
        }
        else if (instance != this)
        { // ��������� ������� ��� ���������� �� �����
            Destroy(gameObject); // ������� ������
        }

        // ������ ��� ����� �������, ����� ������ �� �����������
        // ��� �������� �� ������ ����� ����
        DontDestroyOnLoad(gameObject);

        // � ��������� ���������� �������������
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
