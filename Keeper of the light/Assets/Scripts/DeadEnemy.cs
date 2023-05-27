
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;


public class DeadEnemy : MonoBehaviour
{
   
   
  
    [SerializeField] private GameObject _deadenemy;
   [SerializeField] private bool _isDead = false;

   public bool IsDead
   {
      set => _isDead = value;
      get => _isDead;
      
   } 
   
 
   public void deadEnemy()
   {
      Debug.Log("asasasasasa");
      _isDead = true;
     
    
      if (_isDead)
      {
         Destroy(gameObject);
         Debug.Log("ssssss");
         Instantiate(_deadenemy, transform.position,quaternion.identity);
      }
     
   }

}
