using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class NoteUISpawner : MonoBehaviour
{
    [SerializeField] private GameObject _notePrefabs;
    [SerializeField] private Transform _noteContainer;

    public void SpawnNotesUI(string _text, bool saveInList)
    {
        var noteUI = Instantiate(_notePrefabs,_noteContainer);
        var noteUIComponent = noteUI.GetComponent<InterfaceItemScript>();
        noteUIComponent._textNote = _text;

        var _interfaceController = GameObject.Find("InterfaceController").GetComponent<InterfaceController>();
        if (saveInList)
        {
            _interfaceController.SetNote(_text);
        }
    }
}
