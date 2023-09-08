using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellStones : MonoBehaviour
{
    [SerializeField] private StoneData _requiredStone;

    public Action<CellStones> cellActive;
    public Action<CellStones> cellDisActive;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Stone>())
        {
            if (collision.GetComponent<Stone>().GetData() == _requiredStone)
                cellActive?.Invoke(gameObject.GetComponent<CellStones>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Stone>())
            cellDisActive?.Invoke(gameObject.GetComponent<CellStones>());
    }
}
