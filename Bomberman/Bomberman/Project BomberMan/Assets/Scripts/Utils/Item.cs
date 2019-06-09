using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int value;
    public Sprite sprite;

    public Item(int val, Sprite tempSprite)
    {
        value = val;
        sprite = tempSprite;
    }
    ~Item()
    {
    }
}
