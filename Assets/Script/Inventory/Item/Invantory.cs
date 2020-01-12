using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Invantory : MonoBehaviour
{
    [SerializeField] private int maxSlotItem;
    public int MaxSlotItem
    {
        get { return maxSlotItem; }
        set {
            maxSlotItem = value;
            UpdateSlotCount();
        }
    }
    [SerializeField] private List<ItemSlot> itemSlots;
    [SerializeField] private ItemSlot[] allItemSlot;
    [SerializeField] private List<Item> items;
    [SerializeField] private GameObject prefItemSlots;
    [SerializeField] private int gold;



    //public int Gold
    //{
    //    get {
    //        return gold;
    //    }
    //    set
    //    {
    //        gold = value;
    //    }
    //}
    public int GetGold()
    {
        return gold;
    }
    public void AddCoins(int gold)
    {

        this.gold += gold;
        if(OnGoldChange != null)
            OnGoldChange(this.gold);
    }
    public static Invantory instence;
    public event Action<Item> OnItemClickEvent;
    public event Action<int> OnGoldChange;

    // Start is called before the first frame update

    private void Awake()
    {
        Debug.Log("Inventory");
        instence = this;
        OnItemClickEvent += UseItem;
        
        
        
        
    }
    private void CreateSlotItems()
    {
        itemSlots.Clear();
        maxSlotItem = LoadSaveSystem.instance.LoadMaxSlotCount();
        items = LoadSaveSystem.instance.LoadInventory(maxSlotItem);
        allItemSlot = GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < allItemSlot.Length; i++)
        {
            //GameObject gameObject = Instantiate(prefItemSlots, transform);
            //gameObject.GetComponent<ItemSlot>().OnClickEvent += OnItemClickEvent;
            //itemSlots.Add(gameObject.GetComponent<ItemSlot>());

            if (i < maxSlotItem)
            {
                itemSlots.Add(allItemSlot[i]);
                itemSlots[i].OnClickEvent += OnItemClickEvent;
            }
            else
                allItemSlot[i].gameObject.SetActive(false);
        }
        RefreshUI();
    }
    private void UpdateSlotCount()
    {
        itemSlots.Clear();
        //items = LoadSaveSystem.instance.LoadInventory(maxSlotItem);
        //allItemSlot = GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < allItemSlot.Length; i++)
        {
            //GameObject gameObject = Instantiate(prefItemSlots, transform);
            //gameObject.GetComponent<ItemSlot>().OnClickEvent += OnItemClickEvent;
            //itemSlots.Add(gameObject.GetComponent<ItemSlot>());

            if (i < maxSlotItem)
            {
                allItemSlot[i].gameObject.SetActive(true);
                itemSlots.Add(allItemSlot[i]);
                itemSlots[i].OnClickEvent += OnItemClickEvent;
            }
            else
                allItemSlot[i].gameObject.SetActive(false);
        }
        RefreshUI();
    }
    private void Start()
    {
        CreateSlotItems();
        AddCoins(LoadSaveSystem.instance.LoadGold());
        //LoadSaveSystem.instance.Defolt();

        //OnItemClickEvent += UseItem;
        //OnGoldChange += SetGold;
    }
    public void UseItem(Item item)
    {
        if (item.UseItem())
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].Item == item)
                    itemSlots[i].Item = null;
            }
            items.Remove(item);
            item = null;
            RefreshUI();
        }
    }

    private void RefreshUI()
    {
        int i = 0;
        for(; i < items.Count && i < itemSlots.Count; i++)
        {
            itemSlots[i].Item = items[i];
        }
        for(; i < itemSlots.Count; i ++)
        {
            itemSlots[i].Item = null;
        }
        Debug.Log("RefreshUi");
    }
    public bool AddItem(Item item)
    {
        if (IsFull())
            return false;

        items.Add(item);
        RefreshUI();
        return true;
    }
    public bool IsFull()
    {
        return items.Count >= itemSlots.Count;
    }
    public Item HasKeyItem()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            
            if (itemSlots[i].Item is Key)
            {
                return itemSlots[i].Item;
            }   
        }
        return null;
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        item = null;
        RefreshUI();
    }
    private void OnDestroy()
    {
        LoadSaveSystem.instance.SaveMaxSlotCount(maxSlotItem);
        LoadSaveSystem.instance.SaveInventory(itemSlots);
        LoadSaveSystem.instance.SaveGold(gold);
    }
}
