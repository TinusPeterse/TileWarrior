using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Warrior : ItemLayOut
{
    int WarriorRage;
    private Sprite BasicSprite;
    public Sprite WarriorRageSprite;
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
        PlayerPrefs.SetInt("WarriorRage", 0);
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
        PlayerStepObject.GetComponentInChildren<Text>().text = PlayerStepCounter.ToString();
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
            PlayerArmorObject.GetComponentInChildren<Text>().text = PlayerArmor.ToString();
            PlayerHpObject.GetComponentInChildren<Text>().text = PlayerHp.ToString();
            UpdateTile(index, 0); TriggerRandomTile(index);
        }
        else if (Current[index].MonsterType.ToString() == "Armor")
        {
            PlayerArmor += Current[index].DamHeal;
            PlayerArmorObject.GetComponentInChildren<Text>().text = PlayerArmor.ToString();
            WarriorRage = 0;
            Current[PlayerIndex].sprite = BasicSprite;
            UpdateTile(index, 0); TriggerRandomTile(index);
        }
        SpecialCounter = GameObject.FindGameObjectWithTag("KillingSpreeCounter");
        SpecialCounter.GetComponentInChildren<Text>().text = WarriorRage.ToString();
    }
}
