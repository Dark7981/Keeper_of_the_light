using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    

public class GameManager : MonoBehaviour
{
    public static Action OpenInterface;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenInterface.Invoke();
        }
    }
}
