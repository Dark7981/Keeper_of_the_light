using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitlesController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    public List<string> _subtitlesText;

    private void OnEnable()
    {
        SubtitlesTrigger.NeedSubtitles += SetSubtitles;
    }
    private void OnDisable()
    {
        SubtitlesTrigger.NeedSubtitles -= SetSubtitles;
    }
    public void SetSubtitles(List<string> subtitles)
    {
        _subtitlesText = subtitles;
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
        StopCoroutine("SubtitleShowCaskad");
    }
    private IEnumerator SubtitleShow(int subtitleNumber)
    {
        _textMesh.enabled = true;
        _textMesh.text = _subtitlesText[subtitleNumber];
        yield return new WaitForSeconds(2f);
        _textMesh.enabled = false;
        StopCoroutine("SubtitleShow");
    }
}
