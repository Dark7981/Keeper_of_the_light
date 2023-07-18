using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestiariySetter : MonoBehaviour
{
    public string descriptrion;
    public string _name;
    public Sprite sprite;
    public bool takeSprite;

    public static Action<string, string, Sprite> createBestiariyItem;
    private void Start()
    {
        if (gameObject.GetComponent<SpriteRenderer>() && takeSprite == true)
        {
            sprite = GetComponent<SpriteRenderer>().sprite;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvokeCreate(collision);  
    }
    public void InvokeCreate(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            createBestiariyItem.Invoke(_name, descriptrion, sprite);
        }
    }
}
