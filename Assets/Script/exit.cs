using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player")
        {
            LoadScene.instance.LoadScene_btn(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
