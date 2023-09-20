using System;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceItemScript : MonoBehaviour
{
    private Button _buttonComponent;
    public static Action<string> NotePressed;
    public string _textNote;
    private void OnEnable()
    {
        InterfaceController.NoteBlock += NoteUnBlock;
        _buttonComponent = gameObject.GetComponent<Button>();
    }
    private void OnDisable()
    {
        InterfaceController.NoteBlock -= NoteUnBlock;
    }
    public void NotePressedScript()
    {
        NotePressed.Invoke(_textNote);
        SwitchBlock(false);
    }
    public void NoteUnBlock()
    {
        SwitchBlock(true);
    }
    public void SwitchBlock(bool interactable)
    {
        if (_buttonComponent.enabled)
        {
            _buttonComponent.interactable = interactable;
        }
    }
}