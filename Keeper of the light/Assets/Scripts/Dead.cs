
using UnityEngine;
using UnityEngine.AI;

public class Dead : MonoBehaviour
{

  [SerializeField] private SpriteRenderer _spriteRenderer;
  [SerializeField] private PlayerMovement _playerMovement;
  [SerializeField] private UpdateController _updateController;
 

  [SerializeField] private Animator _animator;

  private void Start()
  {
    
    _animator.GetComponent<Animator>().enabled = false;


  }

  public void dead()
  {
    _animator.GetComponent<Animator>().enabled = true;
    _updateController.enabled = false;
    _playerMovement.enabled = false;
    _spriteRenderer.color = Color.red;


  }

  
   
  
  
}
