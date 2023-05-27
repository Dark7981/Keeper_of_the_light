
using UnityEngine;
using UnityEngine.AI;

public class Dead : MonoBehaviour
{

  [SerializeField] private SpriteRenderer _spriteRenderer;
  [SerializeField] private PlayerMovement _playerMovement;
  [SerializeField] private UpdateController _updateController;
  [SerializeField] private Animator _animator;
 



 
  public void dead()
  {
   _animator.SetBool("Transition",true);
    _updateController.enabled = false;
    _playerMovement.enabled = false;
    _spriteRenderer.color = Color.red;


  }

  
   
  
  
}
