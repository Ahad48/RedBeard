using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// An Item class which stored basic details, Inheriting the Scriptable object class 
/// </summary>
[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    //Attributes that each item has

    //Name of the item
    public string objectName;

    //Item Sprite
    public Sprite sprite;

    //Item Quantity
    public int quantity;

    //Can it be stacked, eg. Coins can be stacked
    public bool stackable;

    //various types of items
    public enum ItemType
    {
        Coin,
        Health
    }

    //Item type so that it can be selected in the inspector
    public ItemType itemType;
}
