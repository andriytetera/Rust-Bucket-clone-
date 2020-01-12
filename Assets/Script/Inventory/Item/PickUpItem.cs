using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player")
        {
            bool temp = item.PickUp();
            if (temp)
            {
                SoundManager.instance.PlaySoundSingle(item.pickUpSound);
                gameObject.SetActive(false);
            }
            //Destroy(gameObject);

        }
    }
}
