using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoint : MonoBehaviour
{
    public Sprite fullIcon;
    public Sprite emptyIcon;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetFullIcon()
    {
        image.sprite = fullIcon;
    }
    public void SteEmptyIcon()
    {
        image.sprite = emptyIcon;
    }
}
