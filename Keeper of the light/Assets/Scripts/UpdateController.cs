
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement; 

    private void Update()
    {
        _playerMovement.ScriptUpdate(); 
    }

    private void FixedUpdate()
    {
        _playerMovement.ScriptFixedUpdate();  
    }
}
