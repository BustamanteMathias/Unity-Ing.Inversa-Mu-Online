using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemGenerate
{
    public static Item Generate()
    {
        Item _item = new Item();
        _item.id = Random.Range(1, 6);

        return _item;
    }
}
