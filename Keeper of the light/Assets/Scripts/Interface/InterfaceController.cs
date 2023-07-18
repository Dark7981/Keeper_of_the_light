using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;

public class InterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject _interface;
    [SerializeField] private GameObject _bestiariy;
    [SerializeField] private GameObject _note;
    [SerializeField] private Image _noteImageUI;
    [SerializeField] private TextMeshProUGUI _noteText;
    [SerializeField] private List<GameObject> Paneglifs;
    [SerializeField] private GameObject menuFolder;
    private int numberOfItem;
    public int _numberOfItem
    {
        get { return numberOfItem; }
        set
        {
            if (value >= 0 && value <= 1)
            {
                numberOfItem = value;
            }
            else
            {
                numberOfItem = 0;
            }
        }
    }
    public static Action NoteBlock;
    public static Action<string, int> NoteText;
    public static Action _openInterface;
    public static Action _closeInterface;

    private void OnEnable()
    {
        InterfaceItemScript.NotePressed += ShowNote;
        NoteScript.UnlockPaneglif += UnlockPaneglifUI;
        NoteScript.GetTextPaneglif += TextPaneglif;
        PlayerBehaviour.menuOpened += HideInterface;
    }
    private void OnDisable()
    {
        InterfaceItemScript.NotePressed -= ShowNote;
        NoteScript.UnlockPaneglif -= UnlockPaneglifUI;
        NoteScript.GetTextPaneglif -= TextPaneglif;
        PlayerBehaviour.menuOpened -= HideInterface;
    }
    private void Start()
    {
        _numberOfItem = 0;
        InterfaceUpdate();
    }
    private void MenuUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuFolder.activeSelf == true)
        {
            MenuScript(1, false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuFolder.activeSelf == false)
        {
            HideInterface();
            MenuScript(0, true);
        }
    }
    public void MenuScript(int timeScale, bool menuActive)
    {
        Time.timeScale = timeScale;
        menuFolder.SetActive(menuActive);
    }
    public void InterfaceUpdate()
    {
        if (_numberOfItem == 0)
        {
            _bestiariy.SetActive(true);
            _note.SetActive(false);
        }
        else
        {
            _bestiariy.SetActive(false);
            _note.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _noteImageUI.enabled && _interface.activeSelf)
        {
            HideNote();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenInterface();
        }
        MenuUpdate();
    }
    public void BestiariyButton()
    {
        _numberOfItem = 0;
        InterfaceUpdate();
    }
    public void NoteButton()
    {
        _numberOfItem = 1;
        InterfaceUpdate();
    }
    public void ShowNote(string Text)
    {
        _noteImageUI.enabled = true;
        _noteText.text = Text;
        _noteText.enabled = true;
    }
    public void HideNote()
    {
        _noteImageUI.enabled = false;
        _noteText.text = string.Empty;
        _noteText.enabled = false;
        NoteBlock.Invoke();
    }
    public void OpenInterface()
    {
        if (_interface.activeSelf == false && menuFolder.activeSelf == false)
        {
            _interface.SetActive(true);
            _openInterface.Invoke();
        }
        else
        {
            HideInterface();
        }
    }
    public void HideInterface()
    {
        if (_interface.activeSelf == true)
        {
            _closeInterface.Invoke();
            _interface.SetActive(false);
        }
    }
    public void UnlockPaneglifUI(bool isNew, int id)
    {
        if (isNew)
        {
            Paneglifs[id].SetActive(true);
        }
    }
    public void TextPaneglif(int id)
    {
        var paneglifScript = Paneglifs[id].GetComponent<InterfaceItemScript>();
        paneglifScript.idPaneglif = id;
        NoteText.Invoke(paneglifScript._textNote, id);
    }
}