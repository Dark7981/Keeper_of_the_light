using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEngine;

public class SubtitlesController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    public List<string> _subtitlesText;

    public static Action<bool> FreezePlayer;

    private bool freezePerson;

    private void OnEnable()
    {
        SubtitlesTrigger.NeedSubtitles += SetSubtitles;
    }
    private void OnDisable()
    {
        SubtitlesTrigger.NeedSubtitles -= SetSubtitles;
    }
    public void SetSubtitles(List<string> subtitles, bool freeze)
    {
        freezePerson = freeze;
        if (freezePerson)
        {
            FreezePlayer.Invoke(false);
        }
        _subtitlesText = subtitles;
        StopCoroutine("SubtitleShowCaskad");
        StartCoroutine("SubtitleShowCaskad");
    }
    private IEnumerator SubtitleShowCaskad()
    {
        if (_subtitlesText.Count > 1)
        {
            for (int i = 0; i < _subtitlesText.Count; i++)
            {
                StartCoroutine(SubtitleShow(i));
                yield return new WaitForSeconds(2f);
            }
        }
        else
        {
            StartCoroutine(SubtitleShow(0));
        }
        if (freezePerson)
        {
            FreezePlayer.Invoke(true);
        }
        StopCoroutine("SubtitleShowCaskad");
    }
    private IEnumerator SubtitleShow(int subtitleNumber)
    {
        _textMesh.enabled = true;
        _textMesh.text = _subtitlesText[subtitleNumber];
        yield return new WaitForSeconds(2f);
        _textMesh.enabled = false;
        if (freezePerson)
        {
            FreezePlayer.Invoke(true);
        }
        StopCoroutine("SubtitleShow");

    }
}
