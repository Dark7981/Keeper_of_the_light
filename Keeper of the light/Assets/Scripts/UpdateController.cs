
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private NoteScript _noteScript;

    private void Update()
    {
        if (_playerBehaviour != null)
        {
            _playerBehaviour.ScriptUpdate();
        }
        if (_noteScript != null)
        {
            _noteScript.ScriptUpdate();
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
