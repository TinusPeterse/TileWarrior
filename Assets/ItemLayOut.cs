﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ItemLayOut : MonoBehaviour
{
    public bool Generate;
    private GameObject PlayerHintObject;
    protected Tile[] AllTiles;
    public Level[] Levels;
    public Tile[] Current = new Tile[9];
    public Tile PlayerTile; protected int PlayerIndex;
    public GameObject[] GameTile = new GameObject [9];
    protected GameObject PlayerHpObject, PlayerArmorObject, PlayerStepObject, SpecialCounter;
    public Tile[] Monsters;
    protected List<Tile> LevelMonsters;
    private Tile CurrentMonster;
    protected int PlayerArmor;
    public int PlayerHp;
    public int PlayerStepCounter = 0;
    public int PlayerLevel = 0;
    public void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    public virtual void Start()
    {
        Resett();
    }
    public void SetHint(string HintText)
    {
        PlayerHintObject = GameObject.FindGameObjectWithTag("Hint");
        PlayerHintObject.GetComponentInChildren<TextMeshProUGUI>().text = HintText;
    }
    public virtual void Resett()
    {
        PlayerHp = PlayerPrefs.GetInt("PlayerHp", 10);
        PlayerArmor = PlayerPrefs.GetInt("PlayerArmor", 0);
        PlayerIndex = 4;
        PlayerLevel = PlayerPrefs.GetInt("PlayerLevel", 0);
        PlayerStepCounter = PlayerPrefs.GetInt("PlayerStepCounter", PlayerStepCounter);
        if (Generate) { GenerateNext();AllTiles[0] = PlayerTile;}
        else { AllTiles = Levels[PlayerLevel].Tiles; }
        RandomField();
        PlayerHpObject = GameObject.FindGameObjectWithTag("PlayerHp");
        PlayerArmorObject = GameObject.FindGameObjectWithTag("PlayerArmor");
        PlayerStepObject = GameObject.FindGameObjectWithTag("PlayerStep");
        PlayerHpObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerHp.ToString();
        PlayerArmorObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerArmor.ToString();
        PlayerStepObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerStepCounter.ToString();
    }
    #region Yeet
    bool AllTypes(List<Tile> LevelMo)
    {
        bool Monster = false;
        bool Potion = false;
        bool Armor = false;
        for (int i = 0; i < LevelMo.Count; i++)
        {
            if (LevelMo[i].MonsterType == Tile.TypeMonster.Armor) { Armor = true; }
            if (LevelMo[i].MonsterType == Tile.TypeMonster.Monster) { Monster = true; }
            if (LevelMo[i].MonsterType == Tile.TypeMonster.Potion) { Potion = true; }
        }
        if (Armor && Potion && Monster) { return true; }
        else { return false; }
    }
    int Difficulty = 0;
    public void GenerateNext()
    {
        
        PlayerLevel++;
        LevelMonsters = new List<Tile>();
        LevelMonsters.Add(PlayerTile);
        Difficulty = 0;
        while ((Difficulty < PlayerLevel || LevelMonsters.Count < 4 + PlayerLevel/3) || Difficulty > (PlayerLevel) * 1.2 || (!AllTypes(LevelMonsters)) )
        {
            CurrentMonster = Monsters[Random.Range(0, Monsters.Length - 1)];
            if (CurrentMonster. RequiredLevel >= PlayerLevel){ continue; }
            else if (CurrentMonster.MaxLevel < PlayerLevel) { continue; }
            else
            {
                LevelMonsters.Add(CurrentMonster);
                Difficulty += CalculateDifficulty(CurrentMonster);
                if (LevelMonsters.Count > 6 + PlayerLevel/3)
                {
                    Difficulty -= CalculateDifficulty(LevelMonsters[1]);
                    LevelMonsters.RemoveAt(1);
                }
            }
        }
        AllTiles = LevelMonsters.ToArray();
    }
    public int CalculateDifficulty(Tile YEET)
    {
        int Mult = 0;
        int Difficulty = 0;
        if (YEET.MonsterType == Tile.TypeMonster.Potion)
        {
            Mult = -5;
        }
        else if (YEET.MonsterType == Tile.TypeMonster.Armor)
        {
            Mult = -1;
        }
        else if (YEET.MonsterType == Tile.TypeMonster.Monster)
        {
            Mult = 2;
        }
        else if (YEET.MonsterType == Tile.TypeMonster.ManaPots)
        {
            Mult = -1;
        }
        Difficulty = (int)(Mult * YEET.DamHeal);
        return Difficulty;

    }
    #endregion
    public virtual void RandomField()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i == 4) { UpdateTile(i, 0); PlayerIndex = i;  }
            else
            {
                RandomTile(i);
            }
        }
    }
    public virtual void Update()
    {
        CheckDeath();
        HandleInput();
        if (PlayerStepCounter >= 50 * (PlayerLevel + 1))
        {
            if (Generate) { GenerateNext(); }
            else
            {
                PlayerLevel++;
                Mathf.Clamp(PlayerLevel, 0, Levels.Length);
                AllTiles = Levels[PlayerLevel].Tiles;
            }
        }
        save();
    }
    public void HpDropped0()
    {
        PlayerPrefs.SetInt("PlayerHp", 3);
        PlayerPrefs.SetInt("PlayerArmor", 0);
        PlayerPrefs.SetInt("PlayerLevel", 0);
        PlayerPrefs.SetInt("PlayerStepCounter", 0);
        Resett();
        SceneManager.LoadScene(0);
    }
    public virtual void CheckDeath()
    {
        if (PlayerHp <= 0)
        {
            HpDropped0();
        }
    }
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Up();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Left();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Right();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Down();
        }
    }
    public void Left()
    {
        if (PlayerIndex % 3 == 0) { return; } else { CheckTile(PlayerIndex - 1); }
    }
    public void Right()
    {
        if ((PlayerIndex - 2) % 3 == 0) { return; } else { CheckTile(PlayerIndex + 1); }
    }
    public void Up()
    {
        if (PlayerIndex < 3) { return; } else { CheckTile(PlayerIndex - 3); }
    }
    public void Down()
    {
        if (PlayerIndex > 5) { return; } else { CheckTile(PlayerIndex + 3); }
    }
    public virtual void CheckTile(int index)
    {
        PlayerStepCounter++;
        PlayerStepObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerStepCounter.ToString();
        if (Current[index].MonsterType.ToString() == "Potion")
        {
            Potion(index);
        }
        else if (Current[index].MonsterType.ToString() == "Monster")
        {
            Monster(index);
        }
        else if (Current[index].MonsterType.ToString() == "Armor")
        {
            Armor(index);
        }
        else if (Current[index].MonsterType.ToString() == "ManaPots")
        {
            Mana(index);
        }
    }
        public virtual void Mana(int index)
        {
            
        }
        public void Potion(int index)
        {
            PlayerHp += Current[index].DamHeal;
            PlayerHpObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerHp.ToString();
            UpdateTile(index, 0);
            TriggerRandomTile(index);
        }

        public virtual void Monster(int index)
        {
            int LastPlayerHp = PlayerHp;
            PlayerHp -= (Current[index].DamHeal - PlayerArmor);
            PlayerArmor = 0;
            if (PlayerHp > LastPlayerHp) { PlayerHp = LastPlayerHp; }
            PlayerArmorObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerArmor.ToString();
            PlayerHpObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerHp.ToString();
            UpdateTile(index, 0);
            TriggerRandomTile(index);
        }

        public void Armor(int index)
        {
            PlayerArmor += Current[index].DamHeal;
            PlayerArmorObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerArmor.ToString();
            UpdateTile(index, 0);
            TriggerRandomTile(index);
        }

        public virtual void TriggerRandomTile(int index)
        {
            RandomTile(PlayerIndex); PlayerIndex = index;
        }

    public void UpdateTile(int CurrentInt, int AllTileInt)
    {
        Current[CurrentInt] = AllTiles[AllTileInt];
        GameTile[CurrentInt].GetComponent<SpriteRenderer>().sprite = Current[CurrentInt].sprite;
        if (AllTiles[AllTileInt].DamHeal != 0)
        {
            GameTile[CurrentInt].GetComponentInChildren<TextMeshProUGUI>().text = AllTiles[AllTileInt].DamHeal.ToString();
        }
        else
        {
            GameTile[CurrentInt].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
    public void RandomTile(int Index)
    {
        UpdateTile(Index, Random.Range(1, AllTiles.Length));
        
        if (Current[Index].MonsterType.ToString() == "Monster")
        {
            GameTile[Index].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            GameTile[Index].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }
    public virtual void save()
    {
        PlayerPrefs.SetInt("PlayerHp", PlayerHp);
        PlayerPrefs.SetInt("PlayerArmor", PlayerArmor);
        PlayerPrefs.SetInt("PlayerLevel", PlayerLevel);
        PlayerPrefs.SetInt("PlayerStepCounter", PlayerStepCounter);
    }
}

[System.Serializable]
public class Tile
{
    public Sprite sprite;
    public enum TypeMonster { Potion, Monster, Weapon, Armor, Menu, ManaPots}
    public TypeMonster MonsterType;
    public int DamHeal;
    public int RequiredLevel;
    public int MaxLevel;
}
[System.Serializable]
public class Level
{
    public Tile[] Tiles;
}