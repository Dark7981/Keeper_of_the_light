using UnityEngine;
using UnityEngine.Playables;

public class TimeLineStartTrigger : MonoBehaviour
{
    private bool active = true;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active)
        { 
            GetComponent<PlayableDirector>().Play();
            active = false;
        }
    }
}
