using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Item[] items;

    private Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    private ObtainItemController obtainItemController;

    public bool TRUE_CONDITION()
    {
        return true;
    }

    public bool hasItem(Item item)
    {
        return inventory[item] > 0;
    }

    public bool hasItems(Item[] items)
    {
        foreach (Item item in items) if (doesNotHaveItem(item)) return false;
        return true;
    }

    public bool doesNotHaveItems(Item[] items)
    {
        foreach (Item item in items) if (hasItem(item)) return false;
        return true;
    }

    public bool itemConditions(Item[] _hasItems, Item[] _doesNotHaveItems)
    {
        return hasItems(_hasItems) && doesNotHaveItems(_doesNotHaveItems);
    }

    public bool doesNotHaveItem(Item item)
    {
        return !hasItem(item);
    }

    public void addItemIfNotPresent(Item item)
    {
        if (!hasItem(item)) addItem(item);
    }

    public void addItem(Item item)
    {
        obtainItemController.showObtainedItem(item);
        inventory[item]++;
        PlayerPrefs.SetInt(item.name, inventory[item]);
        PlayerPrefs.Save();
    }

    public void removeItem(Item item)
    {
        if (inventory[item] <= 0) return;
        inventory[item]--;
        PlayerPrefs.SetInt(item.name, inventory[item]);
        PlayerPrefs.Save();
    }

    public void removeAllOfItem(Item item)
    {
        inventory[item] = 0;
        PlayerPrefs.SetInt(item.name, 0);
        PlayerPrefs.Save();
    }

    public void clearInventory()
    {
        foreach (Item item in items) removeAllOfItem(item);
    }

    void Start()
    {
        obtainItemController = GameObject.Find("ItemUI").GetComponent<ObtainItemController>();
        foreach (Item item in items)
        {
            if (PlayerPrefs.HasKey(item.name)) inventory.Add(item, PlayerPrefs.GetInt(item.name));
            else
            {
                inventory.Add(item, 0);
                PlayerPrefs.SetInt(item.name, 0);
            }
        }
        PlayerPrefs.Save();
        clearInventory();
    }
}
