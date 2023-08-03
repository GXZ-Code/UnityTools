using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int maxInventorySize = 20; // 背包容量上限
    public List<Item> items;
    public Inventory()
    {
        items = new List<Item>();
        items.Clear();
    }
    public void SaveInventory()
    {

    }
    public void LoadInventory()
    {

    }
    // 向背包中添加物品
    public bool AddItem(Item item)
    {
        if (items.Count < maxInventorySize)
        {
            items.Add(item);
            return true;
        }
        else
        {
            Debug.Log("背包已满，无法添加物品！");
            return false;
        }
    }

    // 从背包中移除物品
    public bool RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            return true;
        }
        else
        {
            Debug.Log("背包中不存在该物品！");
            return false;
        }
    }
    //改变item位置
    public bool ChangePos(Item item1, Item item2)
    {
        return false;
    }
}
