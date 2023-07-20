using UnityEngine;
using System.Collections.Generic;

public class TrapInventoryScript : MonoBehaviour
{
    [SerializeField] private KeyCode useKey = KeyCode.Q;

    [Header("Needed data")]
    [SerializeField] private List<GameObject> trapInventory;
    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private List<GameObject> trapButtons;
    

    [SerializeField] private int _traps;
    public int traps
    {
        get { return _traps; }
        set { if (_traps + value >= 0 && _traps + value <= 3)
                _traps += value;    
            }
    }

    private void Start()
    {
        UpdateController updateController = GameObject.FindGameObjectWithTag("UpdateController").GetComponent<UpdateController>();
        updateController.trapInventoryScript = GetComponent<TrapInventoryScript>();
    }

    public void ScriptUpdate()
    {
        if (Input.GetKeyDown(useKey) && _traps > 0)
        {
            SetATrap();
        }
    }

    public void TakeTrap(GameObject trapButton)
    {
        trapInventory[traps].SetActive(true);
        traps = 1;
        HideButton(trapButton);
    }

    public void ShowButton(GameObject trapButton)
    {
        trapButton.SetActive(true);
    }

    public void HideButton(GameObject trapButton)
    {
        trapButton.SetActive(false);
    }

    public void SetATrap()
    {
        traps = -1;
        var trap = Instantiate(trapPrefab, gameObject.transform.position, Quaternion.identity);
        trap.GetComponent<TrapTriger>().trapButton = trapButtons[traps];
        trapInventory[traps].SetActive(false);
    }
}