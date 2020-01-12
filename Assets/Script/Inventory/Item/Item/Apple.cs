using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Apple : Item
{
    [SerializeField] private AudioClip appleCrunch;
    public override bool PickUp()
    {
        if (Invantory.instence.AddItem(this))
            return true;
        else return false;
    }

    public override bool UseItem()
    {
        if (!PlayerManager.instance.IsHealthFull())
        {
            PlayerManager.instance.SetHealth(1);
            SoundManager.instance.PlaySoundSingle(appleCrunch);
            return true;
        }
        else
            return false;
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
