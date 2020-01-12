using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] private List<Image> levelImage;
    [SerializeField] private List<int> levels;
    [SerializeField] private Button upButton;
    [SerializeField] private Text gold;
    private int level;
    private enum LevelUpStat
    {
        Health,
        SlotCount,
        Damage,
    }
    [SerializeField] private LevelUpStat levelStat;
    // Start is called before the first frame update
    private void Start()
    {
        Setup(PlayerManager.instance.MaxHeath);
    }
    public void Setup(int level)
    {
        this.level = level;
        upButton.GetComponentInChildren<Text>().text = levels[level].ToString();

        UpdateButtonUp();
        
        for (int i = 0; i < levelImage.Count; i++)
        {
            if (i == level)
                return;

            levelImage[i].color = Color.green;
        }
    }
    public void UpdateButtonUp()
    {
        if (Invantory.instence.GetGold() < levels[level])
        {
            upButton.interactable = false;
        }else
            upButton.interactable = true;
    }
    public void LevelUp()
    {
        Invantory.instence.AddCoins(-levels[level]);
        level++;
        if (levelStat == LevelUpStat.Health)
        {
            PlayerManager.instance.MaxHeath = level;
            PlayerManager.instance.SetHealth(level);
            //LoadSaveSystem.instance.SaveMaxHealth(level);
        }
        if(levelStat == LevelUpStat.SlotCount)
        {
            //LoadSaveSystem.instance.SaveMaxSlotCount(level);
            Invantory.instence.MaxSlotItem = level;
            
        }
        if(levelStat == LevelUpStat.Damage)
        {
            
            //LoadSaveSystem.instance.SaveDamage(level);
            PlayerManager.instance.Damage = level;
        }
        Setup(level);
    }
}
