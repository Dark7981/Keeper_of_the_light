using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneUIInit : MonoBehaviour
{
    [SerializeField] private GameObject runeUIPrefab;
    [SerializeField] private List<CellData> runes;
    [SerializeField] private List<CellData> spawnedUIRune;


    private void Start()
    {
        PlayerBehaviour.newRune += RuneInitUI;
        runes = GameObject.Find("Player").GetComponent<PlayerBehaviour>().GetRune();
        RuneSpawner();
    }
    public void RuneInitUI(CellData rune)
    {
        if (runes.Count != 0)
        {
            Debug.Log("Start UI Init");
            bool isNewRune = true;
            foreach (CellData cell in runes)
            {
                if (cell == rune)
                {
                    isNewRune = false;
                    return;
                }
            }
            if (isNewRune)
            {
                runes.Add(rune);
                RuneSpawner();
            }
        }else
        {
            runes.Add(rune);
                RuneSpawner();
        }
    }
    public void RuneSpawner()
    {
        foreach(var runeData in runes)
        {
            bool newRune = true;
            foreach (CellData cell in spawnedUIRune)
            {
                if (cell == runeData)
                {
                    newRune = false;
                    return;
                }
            }
            if (newRune)
            {
                var correctRune = Instantiate(runeUIPrefab,transform);
                correctRune.GetComponent<Image>().sprite = runeData.icon;
                correctRune.name = runeData.name;
            }
        }
    }
}
