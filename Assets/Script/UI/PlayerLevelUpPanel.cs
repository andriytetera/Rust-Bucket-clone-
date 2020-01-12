using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUpPanel : MonoBehaviour
{
    [SerializeField] private Text gold;
    private void Start()
    {
        Invantory.instence.OnGoldChange += UpdateGoldPanel;
        UpdateGoldPanel(Invantory.instence.GetGold());
    }

    public void UpdateGoldPanel(int gold)
    {
        this.gold.text = gold.ToString();
    }
}
