using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _player;
    [SerializeField] private GameObject _respawnButton;
    void FixedUpdate()
    {
        if (_player.enabled == false)
        {
            _respawnButton.SetActive(true);
        }
        else
        {
            _respawnButton.SetActive(false);
        }
    }
}
