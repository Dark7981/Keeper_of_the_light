using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineOffObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> ActiveGameObjects; 
    void Start()
    {
        for (int i = 0; i < ActiveGameObjects.Count; i++)
        {
            ActiveGameObjects[i].SetActive(false);
        }
    }

}
