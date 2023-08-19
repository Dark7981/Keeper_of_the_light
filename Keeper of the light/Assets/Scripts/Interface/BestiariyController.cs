using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestiariyController : MonoBehaviour
{
    [SerializeField] private GameObject _bestiariyItemPrefab;
    [SerializeField] private Transform _bestiariyItemParent;
   
    public List<string> _bestiariyNameList;
    private void OnEnable()
    {
        BestiariySetter.createBestiariyItem += CreateItem; 
        
    }
    private void OnDisable()
    {
        BestiariySetter.createBestiariyItem -= CreateItem;
    }

    public void CreateItem(string name, string description, Sprite sprite)
    {
        bool exist = true;
        foreach (string bestiariyName in _bestiariyNameList)
        {
            if (bestiariyName == name)
            {
                exist = false;
            }
            else
            {
                exist = true;
            }
        }
        Debug.Log(exist);
        if (exist)
        {
            Debug.Log("Exist");
            var newBestiariyObject = Instantiate(_bestiariyItemPrefab, _bestiariyItemParent);
            var BestiariyItem = newBestiariyObject.GetComponent<BestiariyItem>();
            BestiariyItem.FillingItem(name,description,sprite);
            _bestiariyNameList.Add(name);
        }
    }

}
