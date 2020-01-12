using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveSystem : MonoBehaviour
{
    public static LoadSaveSystem instance;

    public Item apple;
     
    private void Awake()
    {
        Debug.Log("LoadSaveSystem");
        instance = this;
    }
    public void SaveGold(int gold)
    {
        PlayerPrefs.SetInt("gold", gold);
    }
    public int LoadGold()
    {
        return PlayerPrefs.GetInt("gold");
    }
    public void SaveInventory(List<ItemSlot> items)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].Item != null && items[i].Item.nameItem == "health")
            {
                //Debug.Log(items[i].nameItem);
                PlayerPrefs.SetString("slot" + i, items[i].Item.nameItem);
            }
            else
                PlayerPrefs.SetString("slot" + i, "");
        }
    }
    public List<Item> LoadInventory(int countSlots)
    {
        List<Item> items = new List<Item>();
        for (int i = 0; i < countSlots; i++)
        {
            string temp =  PlayerPrefs.GetString("slot" + i);
            if (temp == "health")
                items.Add(apple);
        }
        return items;
    }
    public void SaveHealth(int health)
    {
        PlayerPrefs.SetInt("health", health);
    }
    public int LoadHealth()
    {
        return PlayerPrefs.GetInt("health");
    }
    public void SaveMaxSlotCount(int maxSlotCount)
    {
        PlayerPrefs.SetInt("maxSlotCount", maxSlotCount);
    }
    public int LoadMaxSlotCount()
    {
        return PlayerPrefs.GetInt("maxSlotCount");
    }
    public void SaveDamage(int damage)
    {
        PlayerPrefs.SetInt("damage", damage);
    }
    public int LoadDamage()
    {
        return PlayerPrefs.GetInt("damage");
    }
    public void SaveMaxHealth(int maxHealth)
    {
        PlayerPrefs.SetInt("maxHealth", maxHealth);

    }
    public int LoadMaxHealth()
    {
        return PlayerPrefs.GetInt("maxHealth");
    }
    public void Defolt()
    {
        int maxSlotCount = PlayerPrefs.GetInt("maxSlotCount");
        
        for(int i = 0; i < maxSlotCount; i++)
        {
            PlayerPrefs.SetString("slot" + i, "");
        }
        PlayerPrefs.SetInt("maxHealth", 1);
        PlayerPrefs.SetInt("damage", 1);
        PlayerPrefs.SetInt("maxSlotCount", 1);
        PlayerPrefs.SetInt("health", 1);
        PlayerPrefs.SetInt("gold", 0);
    }
}
