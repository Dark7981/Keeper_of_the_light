using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour
{
    [SerializeField] private Image noteName; //�������, ����� ���� ������� �����
    [SerializeField] private GameObject button; //������ ��� ����� ������ ��� ��������
    [SerializeField] private TextMeshProUGUI exitButton;//����� ����� ������� ��� ������
    [SerializeField] private KeyCode buttonLetter;
    [SerializeField] private TextMeshProUGUI noteText;
    private UpdateController _updateController;
    private Image buttonImage;
    private TextMeshProUGUI buttonText;
    private ShadowCaster2D _shadowCaster2d;


    private void Start()
    {
        UpdateController updateController = GameObject.FindGameObjectWithTag("UpdateController").GetComponent<UpdateController>();
        updateController.noteScript = GetComponent<NoteScript>();

        _updateController = GameObject.Find("UpdateController").GetComponent<UpdateController>();
        _shadowCaster2d = GetComponent<ShadowCaster2D>();
        button.GetComponentInChildren<TextMeshProUGUI>().text = $"{buttonLetter}";
        exitButton.text = $"������i�� {buttonLetter}, ��� �������";
        buttonImage = button.GetComponent<Image>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _updateController.noteScript = gameObject.GetComponent<NoteScript>()    ;
        _shadowCaster2d.enabled = false;
        if (collision.gameObject.CompareTag("Player"))
        {
            if (noteName.IsActive() == false)//�������� �� �� ���������� �������
            {
                ShowButton();//����� ������ ��� ��������
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)//�������� �� �� ������ ������� �� ��� �������
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
        if (Input.GetKeyDown(buttonLetter) && buttonImage.enabled == true)
        {
            StartCoroutine(OpenNote());
            Time.timeScale = 0;
        } //³������� �������
            
        else if (Input.GetKeyDown(buttonLetter))//��� �� ��������
        {
            StartCoroutine(CloseNote());
            Time.timeScale = 1;
        }

    }
    public IEnumerator CloseNote()//�������� �������
    {
        noteName.enabled = false;
        noteText.enabled = false;
        exitButton.enabled = false;
        yield return null;
        StopCoroutine(CloseNote());

    }
    public IEnumerator OpenNote()//³������� �������
    {
        CloseButton();
        noteName.enabled = true;
        noteText.enabled = true;
        exitButton.enabled = true;
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(OpenNote());
        

    }
    private void ShowButton()//����� ������ ��� �������
    {
        buttonImage.enabled = true;
        buttonText.enabled = true;
    }
    private void CloseButton()//�������� ������ ��� �������
    {
        buttonImage.enabled = false;
        buttonText.enabled = false;
    }
}
