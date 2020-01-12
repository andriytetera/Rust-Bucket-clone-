using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public abstract class Item : ScriptableObject
{
    public Sprite itemIcon;
    public AudioClip pickUpSound;
    public string nameItem;

    [SerializeField] protected Invantory inventory;

    public abstract bool PickUp();
    //public abstract void 
    public abstract bool UseItem();
}
