using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/New shop item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string Description;
    public int baseCost;
}
