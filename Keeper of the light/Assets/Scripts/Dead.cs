using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{

  [SerializeField] private GameObject Player;
  [SerializeField] private PlayerMovement _playerMovement;
  [SerializeField] private UpdateController _updateController;

  [SerializeField] private Animator _animator;

  private void Start()
  {
    
    _animator.GetComponent<Animator>().enabled = false;


  }

  public void dead()
  {
    _updateController.enabled = false;
    _playerMovement.enabled = false;
    Player.GetComponent<MeshRenderer>().material.color = Color.gray;
    _animator.GetComponent<Animator>().enabled = true;
    
  }

   
  
  
}
