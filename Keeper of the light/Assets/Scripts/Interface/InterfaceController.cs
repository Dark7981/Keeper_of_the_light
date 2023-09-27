using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;

public class InterfaceController : MonoBehaviour
{
    [SerializeField] private AudioSource _noteSound;
    [SerializeField] private GameObject _interface;
    [SerializeField] private GameObject _note;
    [SerializeField] private Image _noteImageUI;
    [SerializeField] private TextMeshProUGUI _noteText;
    [SerializeField] private GameObject menuFolder;
    [SerializeField] private GameObject RespawnButton;
    [SerializeField] private NoteManager _noteManager;
    [SerializeField] private NoteUISpawner _noteUISpawner;
    [SerializeField] private List<string> _notesList;
    private PlayerBehaviour playerBehaviour;
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
    [SerializeField] private List<NoteScript> _paneglifs;

    private void OnEnable()
    {
        InterfaceItemScript.NotePressed += ShowNote;
        PlayerBehaviour.menuOpened += HideInterface;
    }
    private void OnDisable()
    {
        InterfaceItemScript.NotePressed -= ShowNote;
        PlayerBehaviour.menuOpened -= HideInterface;
    }
    private void Start()
    {
        foreach (var _panegfifsObject in _paneglifs) { _panegfifsObject.throwPaneglif += GetPaneglifSubscribe; }
        var player = GameObject.Find("Player");
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        Time.timeScale = 1;
        _numberOfItem = 0;
    }
    private void GetPaneglifSubscribe(string text)
    {
        foreach (var note in _notesList)
        {   
            if (note == text)
            {
                return;
            }
        }
        _noteUISpawner.SpawnNotesUI(text, true);

    }
    private void MenuUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuFolder.activeSelf == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            MenuScript(1, false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuFolder.activeSelf == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
           
            HideInterface();
            MenuScript(0, true);
            if (_noteImageUI.enabled == true)
                HideNote();
        }
    }
    public void MenuScript(int timeScale, bool menuActive)
    {
        Time.timeScale = timeScale;
        menuFolder.SetActive(menuActive);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _noteImageUI.enabled && _interface.activeSelf)
        {
            HideNote();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            OpenInterface();
        }
        MenuUpdate();
    }
    public void NoteButton()
    {
        _numberOfItem = 1;
        _noteSound.Play();
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
        //NoteBlock.Invoke();
    }
    public void OpenInterface()
    {

        if (_interface.activeSelf == false && menuFolder.activeSelf == false)
        {
            if (_noteImageUI.enabled == true)
                HideNote();
            _interface.SetActive(true);
            MenuScript(1,false);
            //_openInterface.Invoke();
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            HideInterface();
            if (_noteImageUI.enabled == true)
                HideNote();
        }
    }
    public void HideInterface()
    {
       
        if (_interface.activeSelf == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //_closeInterface.Invoke();
            _interface.SetActive(false);
        }
        if (playerBehaviour.playerDead)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            RespawnButton.SetActive(true);
        }
        MenuScript(1,false);
    }
    public void InitUINotes(List<string> _savedNotesList,GameObject Manager)
    {
        _noteManager = Manager.GetComponent<NoteManager>();
        _notesList = _savedNotesList;
        foreach (var note in _notesList) { _noteUISpawner.SpawnNotesUI(note,false); }
    }
    public void SetNote(string note)
    {
        _noteManager.SetInNoteList(note);
    }
}