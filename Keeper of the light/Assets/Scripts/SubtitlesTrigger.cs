using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SubtitlesTrigger : MonoBehaviour
{
    public static Action<List<string>> NeedSubtitles;

    public List<string> subtitles;
    public bool useTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && useTrigger)
        {
            NeedSubtitles.Invoke(subtitles);
            Destroy(gameObject);
        }
    }
    public void CustomInvoke()
    {
        NeedSubtitles.Invoke(subtitles);
    }
}
