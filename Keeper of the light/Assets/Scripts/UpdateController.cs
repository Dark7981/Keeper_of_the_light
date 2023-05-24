
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private NoteScript _noteScript;

    private void Update()
    {
        _playerMovement.ScriptUpdate(); 
        _noteScript.ScriptUpdate();
    }

    private void FixedUpdate()
    {
        _playerMovement.ScriptFixedUpdate();  
    }
}
