using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteLight"))
        {
            collision.GetComponent<Animator>().SetBool("End", false);
            collision.GetComponent<Animator>().SetBool("Start", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteLight"))
        {
            collision.GetComponent<Animator>().SetBool("End",true);
            collision.GetComponent<Animator>().SetBool("Start", false);
        }
    }
}
