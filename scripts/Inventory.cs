using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int maxInventorySize = 20; // ������������
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
    // �򱳰��������Ʒ
    public bool AddItem(Item item)
    {
        if (items.Count < maxInventorySize)
        {
            items.Add(item);
            return true;
        }
        else
        {
            Debug.Log("�����������޷������Ʒ��");
            return false;
        }
    }

    // �ӱ������Ƴ���Ʒ
    public bool RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            return true;
        }
        else
        {
            Debug.Log("�����в����ڸ���Ʒ��");
            return false;
        }
    }
    //�ı�itemλ��
    public bool ChangePos(Item item1, Item item2)
    {
        return false;
    }
}
