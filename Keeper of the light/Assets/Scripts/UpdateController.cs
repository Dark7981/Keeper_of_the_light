
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private NoteScript _noteScript;

    private void Update()
    {
        if (_playerMovement != null)
        {
            _playerMovement.ScriptUpdate();
        }
        if (_noteScript != null)
        {
            _playerMovement.ScriptUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (_playerMovement != null)
        {
            _playerMovement.ScriptFixedUpdate();
        }
    }
}
