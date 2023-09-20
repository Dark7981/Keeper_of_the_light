using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RuneText : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    private void OnEnable()
    {
        Door.runeText += UpdateText;
    }
    private void OnDisable()
    {
        Door.runeText += UpdateText;
    }
    private void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.enabled = false;
    }
    public void UpdateText(int key, int doorLocks)
    {
        StartCoroutine(ShowText(key, doorLocks));
    }
    private IEnumerator ShowText(int key, int doorLocks)
    {
        _textMesh.enabled = true;
        _textMesh.text = $"You need the runes {key}/{doorLocks}";
        yield return new WaitForSeconds(2f);
        _textMesh.enabled = false;
        StopCoroutine("ShowText");
    }
}
