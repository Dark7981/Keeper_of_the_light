using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour
{
    [SerializeField] private string noteName; //Пошук яка нотатка відкриється, можно буде зробити лістом
    [SerializeField] private GameObject button; //Кнопка яку треба нажати для відкриття
    [SerializeField] private TextMeshProUGUI exitButton;//напис внизу нотатки для виходу
    [SerializeField] private KeyCode buttonLetter;

    private void Start()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = $"{buttonLetter}";
        exitButton.text = $"Натиснiть {buttonLetter}, щоб закрити";
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameObject.Find(noteName).GetComponent<Image>().enabled == false)//перевірка чи не показується нотатка
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
        if (Input.GetKeyDown(buttonLetter) && button.GetComponent<Image>().enabled == true)//Відкриття нотатки
            StartCoroutine(OpenNote());
        else if (Input.GetKeyDown(buttonLetter))//Або її закриття
        {
            StartCoroutine(CloseNote());
        }

    }
    public IEnumerator CloseNote()//Закриття нотатки
    {
        GameObject.Find(noteName).GetComponent<Image>().enabled = false;
        GameObject.Find(noteName).GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        exitButton.enabled = false;
        yield return null;
        StopCoroutine(CloseNote());

    }
    public IEnumerator OpenNote()//Відкриття нотатки
    {
        CloseButton();
        GameObject.Find(noteName).GetComponent<Image>().enabled = true;
        GameObject.Find(noteName).GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        exitButton.enabled = true;
        yield return new WaitForSeconds(0.1f);
        StopCoroutine(OpenNote());
    }
    private void ShowButton()//Показ кнопки для нажаття
    {
        button.GetComponent<Image>().enabled = true;
        button.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }
    private void CloseButton()//Закриття кнопки для нажаття
    {
        button.GetComponent<Image>().enabled = false;
        button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }
}
