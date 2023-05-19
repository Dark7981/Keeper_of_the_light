
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement; // скрипт в якому вкористовується Update()

    private void Update()
    {
        _playerMovement.ScriptUpdate();  // визиваю метод який відповідає за Update()
    }

    private void FixedUpdate()
    {
        _playerMovement.ScriptFixedUpdate();  // визиваю метод який відповідає за FixedUpdate()
    }
}
