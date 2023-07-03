
using UnityEngine;

public class TrapInventoryScript : MonoBehaviour
{
    [SerializeField] private KeyCode useKey = KeyCode.Q;

    [Header("Needed data")]
    [SerializeField] private GameObject trapInventory;
    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private GameObject trapButtons;

    private bool isHad;

    public void ScriptUpdate()
    {
        if (Input.GetKeyDown(useKey) && isHad)
        {
            SetATrap();
        }
    }

    public void TakeTrap()
    {
        isHad = true;

        trapInventory.SetActive(true);
        HideButton();
    }

    public void ShowButton()
    {
        trapButtons.SetActive(true);
    }

    public void HideButton()
    {
        trapButtons.SetActive(false);
    }

    public void SetATrap()
    {
        Instantiate(trapPrefab, gameObject.transform.position, Quaternion.identity);
        isHad = false;
        trapInventory.SetActive(false);
    }

    public GameObject TrapButtons
    {
        get { return trapButtons; }
    }
}