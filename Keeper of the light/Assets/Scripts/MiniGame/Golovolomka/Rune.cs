using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] private CellData _rune;
    [SerializeField] private SpriteRenderer _runeSpriteRenderer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().SetRune(_rune);
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _runeSpriteRenderer.sprite = _rune.icon;
    }
}
