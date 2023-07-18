using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestiariyItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameObject;
    [SerializeField] private TextMeshProUGUI _descriptionObject;
    [SerializeField] private Image Image;
    public bool SetNativeSizeImage;

    public void FillingItem(string name, string description, Sprite sprite)
    {
        _nameObject.text = name;
        _descriptionObject.text = description;
        Image.sprite = sprite;
        if (SetNativeSizeImage)
        {
            Image.SetNativeSize();
        }
        
    }
}
