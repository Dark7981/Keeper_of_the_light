using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour
{
    [SerializeField] private GameObject noteName; //�������, ����� ���� ������� �����
    [SerializeField] private GameObject button; //������ ��� ����� ������ ��� ��������
    [SerializeField] private TextMeshProUGUI exitButton;//����� ����� ������� ��� ������
    [SerializeField] private KeyCode buttonLetter;
    

    private Image note;
    private TextMeshProUGUI noteText;
    private Image buttonImage;
    private TextMeshProUGUI buttonText;


    private void Start()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = $"{buttonLetter}";
        exitButton.text = $"������i�� {buttonLetter}, ��� �������";
        note = noteName.GetComponent<Image>();
        noteText = noteName.GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = button.GetComponent<Image>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (note.enabled == false)//�������� �� �� ���������� �������
            {
                ShowButton();//����� ������ ��� ��������
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)//�������� �� �� ������ ������� �� ��� �������
    {
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
        note.enabled = false;
        noteText.enabled = false;
        exitButton.enabled = false;
        yield return null;
        StopCoroutine(CloseNote());

    }
    public IEnumerator OpenNote()//³������� �������
    {
        CloseButton();
        note.enabled = true;
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
