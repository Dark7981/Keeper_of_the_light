
using Unity.Mathematics;
using UnityEngine;


public class DeadEnemy : MonoBehaviour
{
   
   
  
   [SerializeField] private GameObject deadenemy;
   [SerializeField] private bool isDead = false;
   
 
   public void deadEnemy()
   {
  
      isDead = true;
    
      if (isDead)
      {
         Destroy(gameObject);
         Debug.Log("ssssss");
         Instantiate(deadenemy, transform.position,quaternion.identity);
      }
     
   }

}
