using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private StoneData _data;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public StoneData GetData()
    {
        return _data;
    }
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        _spriteRenderer.sprite = _data.icon;
    }
}
