using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject playerLevelPanel;
    [SerializeField] private Text gold;
    [SerializeField] private List<HealthPoint> healthPoints;
    [SerializeField] private LevelUpPanel healthLevel;
    [SerializeField] private LevelUpPanel slotCountLevel;
    [SerializeField] private LevelUpPanel damageLevel;
    [SerializeField] private PlayerManager player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
        gold.text = Invantory.instence.GetGold().ToString();
        Invantory.instence.OnGoldChange += UpdateGold;
        UpdateHealthPoint(player.Health);
        player.OnChangeHealth += UpdateHealthPoint;
    }
    public void UpdateGold(int gold)
    {
        this.gold.text = gold.ToString();
    }

    public void UpdateHealthPoint(int health)
    {
        int maxHealth = player.MaxHeath;
        for (int i = 0; i < healthPoints.Count; i++)
        {
            if (i < maxHealth)
            {
                healthPoints[i].gameObject.SetActive(true);
                if(i+1 > health)
                {
                    healthPoints[i].SteEmptyIcon();
                }else
                {
                    healthPoints[i].SetFullIcon();
                }
            }
            else
                healthPoints[i].gameObject.SetActive(false);
        }
    }
    public void ShowPlayerPanel()
    {
        playerLevelPanel.SetActive(true);
        healthLevel.Setup(player.MaxHeath);
        damageLevel.Setup(player.Damage);
        slotCountLevel.Setup(Invantory.instence.MaxSlotItem);
      
    }
    public void ClosePlayerPanel()
    {
        playerLevelPanel.SetActive(false);
    }
}
