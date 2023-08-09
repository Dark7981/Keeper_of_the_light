using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TunnelExit : MonoBehaviour
{
    [SerializeField] private Transform playerExit;
    [SerializeField] private Transform playerTransform;
    private BoxCollider2D _boxCollider;
    private bool inTriger = false;
    private KeyCode _keyCode = KeyCode.E;
    [SerializeField] private Animator blackScreen;
    [SerializeField] private GameObject tunnel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.CompareTag("Player"))
        {
          
            inTriger = true;
            Debug.Log("w");
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTriger = false;
        }    
    }

    private IEnumerator ChangePosition()
    {
        Debug.Log("969696");
        blackScreen.SetTrigger("BlackScreen");
        yield return new WaitForSeconds(1.50f);
        playerTransform.position = new Vector3(playerExit.position.x,
            playerExit.position.y,
            playerExit.position.z
        );
        blackScreen.SetTrigger("BlackScreen");


    }
    private  void Update()
    {
        
        if (Input.GetKeyDown(_keyCode) && inTriger)
        {
            StartCoroutine(ChangePosition());
        }
    }
}
