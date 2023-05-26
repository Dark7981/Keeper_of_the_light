
using UnityEngine;

public class TrapTriger : MonoBehaviour
{
    
    [SerializeField] private Dead _dead;
    [SerializeField] private DeadEnemy _deadEnemy;

  

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _dead.dead();
        }

        else if (col.CompareTag("Enemy"))
        {
            Debug.Log("ss");
            
            _deadEnemy.deadEnemy();
            
        }
    }
}
