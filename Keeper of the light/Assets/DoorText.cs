using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorText : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    private void OnEnable()
    {
        Door.doorText += UpdateText;
    }
    private void OnDisable()
    {
        Door.doorText += UpdateText;
    }
    private void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.enabled = false;
    }
    public void UpdateText(int key, int doorLocks)
    {
        StartCoroutine(ShowText(key,doorLocks));
    }
    private IEnumerator ShowText(int key,int doorLocks)
    {
        _textMesh.enabled = true;
        _textMesh.text = $"You need the keys {key}/{doorLocks}";
        yield return new WaitForSeconds(2f);
        _textMesh.enabled = false;
        StopCoroutine("ShowText");
    }

}
