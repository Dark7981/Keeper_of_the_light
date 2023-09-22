using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaneglifSaveScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _notes; 

    public static PaneglifSaveScript instance = null; // ��������� �������

    void Start()
    {
        // ������, ��������� ������������� ����������
        if (instance == null)
        { // ��������� ��������� ��� ������
            instance = this; // ������ ������ �� ��������� �������
        }
        if (instance == this)
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
        
    }
    public void SetNotes(GameObject _notePrefabs)
    {
        _notes.Add(_notePrefabs);
    }
}
