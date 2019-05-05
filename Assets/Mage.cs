using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Mage : ItemLayOut
{
    protected int MageCounter;
    public Sprite MageRage;
    protected Sprite StartSprite;
    private int MageHighScore;
    public override void Start()
    {
        base.Start();
        StartSprite = PlayerTile.sprite;
        MageHighScore = PlayerPrefs.GetInt("MageHighScore", 0);
    }
    public override void CheckTile(int index)
    {
        base.CheckTile(index);
        SpecialCounter = GameObject.FindGameObjectWithTag("KillingSpreeCounter");
        SpecialCounter.GetComponentInChildren<TextMeshProUGUI>().text = MageCounter.ToString();
    }
    public override void Mana(int index)
    {
        MageCounter += Current[index].DamHeal;
        if (MageCounter >= 9)
        {
            PlayerTile.sprite = MageRage;
        }
        else
        {
            PlayerTile.sprite = StartSprite;
        }
        UpdateTile(index, 0); TriggerRandomTile(index);
    }
    public override void CheckDeath()
    {
        if (PlayerStepCounter > MageHighScore)
        {
            MageHighScore = PlayerStepCounter;
            PlayerPrefs.SetInt("MageHighScore", MageHighScore);
        }
        base.CheckDeath();
    }

    public override void Monster(int index)
    {
        if (CheckBunnys(index))
        {
            MageCounter++;
        }
        if (MageCounter >= 10)
        {
            PlayerTile.sprite = MageRage;
        }
        else
        {
            PlayerTile.sprite = StartSprite;
        }
        if (MageCounter >= 10)
        {
            UpdateTile(index, 0); TriggerRandomTile(index);
            if (PlayerIndex % 3 != 0) { KillMonsters(PlayerIndex - 1); }
            if ((PlayerIndex - 2) % 3 != 0) { KillMonsters(PlayerIndex + 1); }
            if (PlayerIndex + 3 < 9) { KillMonsters(PlayerIndex + 3); }
            if (PlayerIndex - 3 >= 0) { KillMonsters(PlayerIndex -3); }
            MageCounter -= 10;
        }
        else
        {
            base.Monster(index);
        }
        if (MageCounter >= 10)
        {
            PlayerTile.sprite = MageRage;
        }
        else
        {
            PlayerTile.sprite = StartSprite;
        }
    }
    public virtual void KillMonsters(int index)
    {
        if (index == PlayerIndex) { return; }
        while (Current[index].MonsterType == Tile.TypeMonster.Monster)
        {
            if (CheckBunnys(index))
            {
                MageCounter++;
            }
            RandomTile(index);
        }
    }
    public virtual bool CheckBunnys(int index)
    {
        if (Current[index].sprite == AllTiles[2].sprite || Current[index].sprite == AllTiles[3].sprite)
            return true;
        else return false;
    }
}
