using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Key : Item
{
    public override bool PickUp()
    {
        if (Invantory.instence.AddItem(this))
            return true;
        else
            return false;
    }

    public override bool UseItem()
    {
        throw new System.NotImplementedException();
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
