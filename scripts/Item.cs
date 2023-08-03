using Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    //public Chara_Ctrl Chara;
    public ItemType type;
    public int damage;
    public Sprite sprite;
    public int count;
    public GameObject Prefab;
    public bool pickable = true;
}

