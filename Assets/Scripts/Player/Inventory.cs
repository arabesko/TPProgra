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

    public int EatApple()
    {
        int index = 0;
        int energy = 0;
        index = items.IndexOf("apple");
        
        if (index != -1)
        {
            energy = itemsPower[index];
            items.RemoveAt(index);
            itemsPower.RemoveAt(index);
            return energy;
        }
        else
        {
            return 0;
        }
    }

    public bool HasItemsy(string item)
    {
        return items.Contains(item);
    }
}
