
using System.Collections.Generic;
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private NoteScript noteScript;

    private void Update()
    {
        if (_playerBehaviour != null)
        {
            _playerBehaviour.ScriptUpdate();
        }
        if (noteScript != null)
        {
            noteScript.ScriptUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (_playerBehaviour != null)
        {
            _playerBehaviour.ScriptFixedUpdate();
        }
    }
}
