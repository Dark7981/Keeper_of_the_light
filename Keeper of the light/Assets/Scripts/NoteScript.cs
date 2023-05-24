using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour
{
    [SerializeField] private GameObject noteName; //Нотатка, можно буде зробити лістом
    [SerializeField] private GameObject button; //Кнопка яку треба нажати для відкриття
    [SerializeField] private TextMeshProUGUI exitButton;//напис внизу нотатки для виходу
    [SerializeField] private KeyCode buttonLetter;

    private Image note;
    private TextMeshProUGUI noteText;
    private Image buttonImage;
    private TextMeshProUGUI buttonText;


    private void Start()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = $"{buttonLetter}";
        exitButton.text = $"Натиснiть {buttonLetter}, щоб закрити";
        note = noteName.GetComponent<Image>();
        noteText = noteName.GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = button.GetComponent<Image>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (note.enabled == false)//перевірка чи не показується нотатка
            {
                ShowButton();//показ кнопки для відкриття
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)//Перевірка чи не вийшов гравець за межі нотатки
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CloseButton();
            StartCoroutine(CloseNote());
        }
    }
    public void ScriptUpdate()
    {
        if (Input.GetKeyDown(buttonLetter) && buttonImage.enabled == true)//Відкриття нотатки
            StartCoroutine(OpenNote());
        else if (Input.GetKeyDown(buttonLetter))//Або її закриття
        {
            StartCoroutine(CloseNote());
        }

    }
    public IEnumerator CloseNote()//Закриття нотатки
    {
        note.enabled = false;
        noteText.enabled = false;
        exitButton.enabled = false;
        yield return null;
        StopCoroutine(CloseNote());

    }
    public IEnumerator OpenNote()//Відкриття нотатки
    {
        CloseButton();
        note.enabled = true;
        noteText.enabled = true;
        exitButton.enabled = true;
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(OpenNote());
    }
    private void ShowButton()//Показ кнопки для нажаття
    {
        buttonImage.enabled = true;
        buttonText.enabled = true;
    }
    private void CloseButton()//Закриття кнопки для нажаття
    {
        buttonImage.enabled = false;
        buttonText.enabled = false;
    }
}
