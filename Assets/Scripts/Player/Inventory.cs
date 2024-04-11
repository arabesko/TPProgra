using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public string nameInventory;
    public List<int> keys = new List<int>();

    public void AddKeys (int code)
    {
        keys.Add(code);
    }

    public bool HasKey(int code)
    {
        return keys.Contains(code);
    }
}
