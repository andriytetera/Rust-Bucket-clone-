using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image image;
    public event Action<Item> OnClickEvent;

    [SerializeField] private Item item;
    public Item Item
    {
        get { return item; }
        set
        {
            item = value;
            if(item == null)
            {
                image.enabled = false;
            }else
            {
                image.sprite = item.itemIcon;
                image.enabled = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Item != null && OnClickEvent != null)
        {
            
            OnClickEvent(item);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
