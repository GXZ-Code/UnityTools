using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItem 
{
    const int equipeCount = 4;
    public GameObject[] item=new GameObject[equipeCount];

    public void Clear()
    {
        for (int i = 0; i < item.Length; i++)
        {
            item[i] = null;
        }
    }
    public void Equip(GameObject gameObject,int i)
    {
        if (i<equipeCount)
        {
            item[i] = gameObject;
        }
       
    }

    public void UnEquip(int i)
    {
        item[i] = null;
    }
    public void ChangePos(int i, int j)
    {
        if (i<equipeCount&&j<equipeCount)
        {

        }
    }
}
