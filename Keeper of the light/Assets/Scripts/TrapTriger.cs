
using UnityEngine;

public class TrapTriger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerMovement>().Dead();
        }
        else if (col.CompareTag("Enemy"))
        {   
            col.GetComponent<RegularEnemy>().Dead();   
        }
    }
}
