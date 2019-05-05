using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Warrior : ItemLayOut
{
    int WarriorRage;
    private Sprite BasicSprite;
    public Sprite WarriorRageSprite;
    private int WarriorHighScore;
    public override void Start()
    {
        base.Start();
        WarriorHighScore = PlayerPrefs.GetInt("WarriorHighScore", 0);
    }
    public override void Resett()
    {
        base.Resett();
        BasicSprite = AllTiles[0].sprite;
        WarriorRage = PlayerPrefs.GetInt("WarriorRage", WarriorRage);
        if (WarriorRage >= 3)
        {
            Current[PlayerIndex].sprite = WarriorRageSprite;
        }
        else
        {
            Current[PlayerIndex].sprite = BasicSprite;
        }
    }
    public override void Update()
    {
        base.Update();
    }
    public override void save()
    {
        base.save();
        PlayerPrefs.SetInt("WarriorRage", WarriorRage);
    }
    public override void CheckDeath()
    {
        if (PlayerStepCounter > WarriorHighScore)
        {
            WarriorHighScore = PlayerStepCounter;
            PlayerPrefs.SetInt("WarriorHighScore", WarriorHighScore);
        }
        base.CheckDeath();
    }
    public override void CheckTile(int index)
    {

        if (WarriorRage >= 2)
        {
            Current[PlayerIndex].sprite = WarriorRageSprite;
        }
        else
        {
            Current[PlayerIndex].sprite = BasicSprite;
        }
        PlayerStepCounter++;
        PlayerStepObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerStepCounter.ToString();
        if (Current[index].MonsterType.ToString() == "Potion")
        {
            WarriorRage = 0;
            Current[PlayerIndex].sprite = BasicSprite;
            base.Potion(index);
        }
        else if (Current[index].MonsterType.ToString() == "Monster")
        {
            WarriorRage++;
            if (!(WarriorRage > 3))
            {
                int LastPlayerHp = PlayerHp;
                PlayerHp -= (Current[index].DamHeal - PlayerArmor);
                PlayerArmor = 0;
                if (PlayerHp > LastPlayerHp) { PlayerHp = LastPlayerHp; }
            }
            PlayerArmorObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerArmor.ToString();
            PlayerHpObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerHp.ToString();
            UpdateTile(index, 0); TriggerRandomTile(index);
        }
        else if (Current[index].MonsterType.ToString() == "Armor")
        {
            PlayerArmor += Current[index].DamHeal;
            PlayerArmorObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerArmor.ToString();
            WarriorRage = 0;
            Current[PlayerIndex].sprite = BasicSprite;
            UpdateTile(index, 0); TriggerRandomTile(index);
        }
        SpecialCounter = GameObject.FindGameObjectWithTag("KillingSpreeCounter");
        SpecialCounter.GetComponentInChildren<TextMeshProUGUI>().text = WarriorRage.ToString();
    }
}