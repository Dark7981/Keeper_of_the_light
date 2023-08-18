using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTextController : MonoBehaviour
{
    public GameObject endText;
    private void Update()
    {
        if (PlayerPrefs.GetInt("End") == 1)
        {
            endText.SetActive(true);
        }else
        {
            endText.SetActive(false);
        }
    }
}
