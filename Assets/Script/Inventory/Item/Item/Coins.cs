using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Coins : Item
{
    [SerializeField] private int count;
    public override bool PickUp()
    {
        Invantory.instence.AddCoins(count);
        return true;
    }

    public override bool UseItem()
    {
        throw new System.NotImplementedException();
    }
}
