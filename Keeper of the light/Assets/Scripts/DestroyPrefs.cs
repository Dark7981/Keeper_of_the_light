using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefs : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteKey("x");
        PlayerPrefs.DeleteKey("y");
        PlayerPrefs.DeleteKey("End");
        if (PlayerPrefs.HasKey("_lastScene"))
        {
            if (PlayerPrefs.GetInt("_lastScene") <= 2)
            {
                PlayerPrefs.DeleteKey("_lastScene");
            }  
        }
        
    }
}
