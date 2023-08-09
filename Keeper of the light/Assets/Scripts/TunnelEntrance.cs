using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TunnelEntrance : MonoBehaviour
{
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform playerTransform;
    private BoxCollider2D _boxCollider;
    public bool inTriger = false;
    private KeyCode _keyCode = KeyCode.E;
    [SerializeField] private Animator blackScreen;
    [SerializeField] private GameObject tunnel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.CompareTag("Player"))
        {
          
            inTriger = true;
            Debug.Log("d");
            
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
        playerTransform.position = new Vector3(playerSpawn.position.x,
            playerSpawn.position.y,
            playerSpawn.position.z
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
