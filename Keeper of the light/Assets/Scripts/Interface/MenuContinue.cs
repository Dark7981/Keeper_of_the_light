using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuContinue : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _continueButton;
    void Update()
    {
        if (PlayerPrefs.HasKey("_lastScene"))
        {
            _image.color = Color.white;
        }
    }
}
