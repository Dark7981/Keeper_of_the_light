using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapInventoryScript : MonoBehaviour
{
    [SerializeField] private bool isHad;
    [SerializeField] private GameObject trapInventory;
    [SerializeField] private GameObject trapPrefab;
    public GameObject PressButton;
    public GameObject ActiveButton;

    private void Update()
    {
        TrapUpdate();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetATrap();
        }
    }
    public void TrapUpdate()
    {
        if (isHad)
            trapInventory.SetActive(true);
        else
            trapInventory.SetActive(false);
    }
    public void TakeTrap()
    {
        isHad = true;
        HideButton();
    }

    public void ShowButton()
    {
        PressButton.SetActive(true);
        ActiveButton.SetActive(true);
    }
    public void HideButton()
    {
        PressButton.SetActive(false);
        ActiveButton.SetActive(false);
    }
    public void SetATrap()
    {
        if (isHad)
        {
            Instantiate(trapPrefab, gameObject.transform.position, Quaternion.identity);
            isHad = false;
        }

    }
}