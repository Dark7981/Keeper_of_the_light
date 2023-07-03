
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    public PlayerBehaviour _playerBehaviour;
    public NoteScript noteScript;
    public TrapInventoryScript trapInventoryScript;

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
        if (trapInventoryScript != null)
        {
            trapInventoryScript.ScriptUpdate();
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
