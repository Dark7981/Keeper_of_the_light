using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour
{
    [SerializeField] private string noteName; //����� ��� ������� ���������, ����� ���� ������� �����
    [SerializeField] private GameObject button; //������ ��� ����� ������ ��� ��������
    [SerializeField] private TextMeshProUGUI exitButton;//����� ����� ������� ��� ������
    [SerializeField] private KeyCode buttonLetter;

    private void Start()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = $"{buttonLetter}";
        exitButton.text = $"������i�� {buttonLetter}, ��� �������";
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameObject.Find(noteName).GetComponent<Image>().enabled == false)//�������� �� �� ���������� �������
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
        if (Input.GetKeyDown(buttonLetter) && button.GetComponent<Image>().enabled == true)//³������� �������
            StartCoroutine(OpenNote());
        else if (Input.GetKeyDown(buttonLetter))//��� �� ��������
        {
            StartCoroutine(CloseNote());
        }

    }
    public IEnumerator CloseNote()//�������� �������
    {
        GameObject.Find(noteName).GetComponent<Image>().enabled = false;
        GameObject.Find(noteName).GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        exitButton.enabled = false;
        yield return null;
        StopCoroutine(CloseNote());

    }
    public IEnumerator OpenNote()//³������� �������
    {
        CloseButton();
        GameObject.Find(noteName).GetComponent<Image>().enabled = true;
        GameObject.Find(noteName).GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        exitButton.enabled = true;
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(OpenNote());
    }
    private void ShowButton()//����� ������ ��� �������
    {
        button.GetComponent<Image>().enabled = true;
        button.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }
    private void CloseButton()//�������� ������ ��� �������
    {
        button.GetComponent<Image>().enabled = false;
        button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }
}
