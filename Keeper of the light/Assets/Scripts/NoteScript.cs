using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour
{
    [SerializeField] private AudioSource _noteSound;
    [SerializeField] private Image noteName; //�������, ����� ���� ������� �����
    [SerializeField] private GameObject button; //������ ��� ����� ������ ��� ��������
    [SerializeField] private TextMeshProUGUI exitButton;//����� ����� ������� ��� ������
    [SerializeField] private KeyCode buttonLetter;
    [SerializeField] private TextMeshProUGUI noteTextMesh;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private bool showInUIStart;
    public string noteText;
    private UpdateController _updateController;
    private Image buttonImage;
    private TextMeshProUGUI buttonText;
    public ShadowCaster2D _shadowCaster2d;
    private bool isInterfaceOpen = true;

    public Action<string> throwPaneglif;

    private void OnEnable()
    {
        InterfaceController._openInterface += CloseAllForInterface;
        InterfaceController._closeInterface += OpenAllForInterface;
    }
    private void OnDisable()
    {
        InterfaceController._openInterface -= CloseAllForInterface;
        InterfaceController._closeInterface -= OpenAllForInterface;
    }
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("Paneglif"));
        UpdateController updateController = GameObject.FindGameObjectWithTag("UpdateController").GetComponent<UpdateController>();
        updateController.noteScript = GetComponent<NoteScript>();
        _updateController = GameObject.Find("UpdateController").GetComponent<UpdateController>();
        button.GetComponentInChildren<TextMeshProUGUI>().text = $"{buttonLetter}";
        exitButton.text = $"{buttonLetter}";
        buttonImage = button.GetComponent<Image>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _updateController.noteScript = gameObject.GetComponent<NoteScript>();
        _shadowCaster2d.enabled = false;
        if (collision.gameObject.CompareTag("Player"))
        {
            if (noteName.IsActive() == false && isInterfaceOpen)
            {
                ShowButton();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _shadowCaster2d.enabled = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            CloseButton();
            StartCoroutine(CloseNote());
        }
    }
    public void ScriptUpdate()
    {
        if (isInterfaceOpen)
        {
            if (Input.GetKeyDown(buttonLetter) && buttonImage.enabled == true)
            {
                StartCoroutine(OpenNote());
                Time.timeScale = 0;
            }
            else if (Input.GetKeyDown(buttonLetter))
            {
                StartCoroutine(CloseNote());
                Time.timeScale = 1;
            }
        }
    }
    public IEnumerator CloseNote()
    {
        noteName.enabled = false;
        noteTextMesh.enabled = false;
        noteTextMesh.text = string.Empty;
        exitButton.enabled = false;
        yield return null;
        StopCoroutine(CloseNote());
    }
    public IEnumerator OpenNote()
    {
        Invoke("OpenNote",0.15f);
        _noteSound.Play();
        CloseButton();
        noteName.enabled = true;
        noteTextMesh.text = noteText;
        noteTextMesh.enabled = true;
        exitButton.enabled = true;
        throwPaneglif?.Invoke(noteText);
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(OpenNote());
    }
    private void ShowButton()
    {
        if (isInterfaceOpen)
        {
            buttonImage.enabled = true;
            buttonText.enabled = true;
        }
    }
    private void CloseButton()
    {
        buttonImage.enabled = false;
        buttonText.enabled = false;
    }
    private void CloseAllForInterface()
    {
        CloseButton();
        StartCoroutine(CloseNote());
        isInterfaceOpen = false;
    }
    private void OpenAllForInterface()
    {
        isInterfaceOpen = true;
    }
}