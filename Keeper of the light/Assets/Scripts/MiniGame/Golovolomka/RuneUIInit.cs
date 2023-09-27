using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneUIInit : MonoBehaviour
{
    [SerializeField] private GameObject runeUIPrefab;
    [SerializeField] private List<CellData> runes;
    [SerializeField] private RuneManager runeManager;
    private void Start()
    {
        runeManager = GameObject.FindWithTag("RuneManager").GetComponent<RuneManager>();
        runeManager._runeUI += SpawnRune;
        foreach (var _rune in runeManager.GetRunes())
        {
            SpawnRune(_rune);
        }
    }
    public IEnumerator Subscribe()
    {
        yield return new WaitForSeconds(0.5f);
        StopCoroutine("Subscribe");
    }
    public void SpawnRune(CellData runeData)
    {
        if (gameObject != null)
        {
            var correctRune = Instantiate(runeUIPrefab, transform);
            correctRune.GetComponent<Image>().sprite = runeData.icon;
            correctRune.name = runeData.name;
            runes.Add(runeData);
        }
    }
    public void Describe()
    {
        runeManager._runeUI -= SpawnRune;;
    }
    //[SerializeField] private List<CellData> spawnedUIRune;


    //private void Start()
    //{
    //    PlayerBehaviour.newRune += RuneInitUI;
    //    runes = GameObject.Find("Player").GetComponent<PlayerBehaviour>().GetRune();
    //    RuneSpawner();
    //}
    //public void RuneInitUI(CellData rune)
    //{
    //    if (runes.Count != 0)
    //    {
    //        Debug.Log("Start UI Init");
    //        bool isNewRune = true;
    //        foreach (CellData cell in runes)
    //        {
    //            if (cell == rune)
    //            {
    //                isNewRune = false;
    //                return;
    //            }
    //        }
    //        if (isNewRune)
    //        {
    //            runes.Add(rune);
    //            RuneSpawner();
    //        }
    //    }else
    //    {
    //        runes.Add(rune);
    //            RuneSpawner();
    //    }
    //    RuneSpawner();
    //}
    //public void RuneSpawner()
    //{
    //    foreach(var runeData in runes)
    //    {
    //        //bool newRune = true;
    //        //if (spawnedUIRune.Count != 0)
    //        //{
    //        //    foreach (CellData cell in spawnedUIRune)
    //        //    {
    //        //        if (cell == runeData)
    //        //        {
    //        //            newRune = false;
    //        //            return;
    //        //        }
    //        //    }
    //        //}
    //        //if (newRune)
    //        //{
    //        var correctRune = Instantiate(runeUIPrefab, transform);
    //        correctRune.GetComponent<Image>().sprite = runeData.icon;
    //        correctRune.name = runeData.name;
    //        spawnedUIRune.Add(runeData);
    //        //}
    //    }
    //}
}
