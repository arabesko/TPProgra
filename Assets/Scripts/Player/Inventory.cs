using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public string nameInventory;
    public List<string> items = new List<string>();
    public List<int> itemsPower = new List<int>();

    public void AddItems (string item, int life)
    {
        items.Add(item);
        itemsPower.Add(life);
    }

    public void DeleteItem(string item, int life)
    {

    }

    public bool HasItemsy(string item)
    {
        return items.Contains(item);
    }
}
