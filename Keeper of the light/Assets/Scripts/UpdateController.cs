
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement; // ������ � ����� �������������� Update()

    private void Update()
    {
        _playerMovement.ScriptUpdate();  // ������� ����� ���� ������� �� Update()
    }

    private void FixedUpdate()
    {
        _playerMovement.ScriptFixedUpdate();  // ������� ����� ���� ������� �� FixedUpdate()
    }
}
