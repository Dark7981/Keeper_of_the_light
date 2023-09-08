using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellData", menuName = "ScriptableObjects/StoneData")]
public class StoneData : ScriptableObject
{
    public Sprite icon;
    public string name;
}
