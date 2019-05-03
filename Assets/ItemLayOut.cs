using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ItemLayOut : MonoBehaviour
{
    private GameObject PlayerHintObject;
    public Level[] Levels;
    protected Tile[] AllTiles;
    public Tile[] Current = new Tile[9];
    protected Tile PlayerTile; protected int PlayerIndex;
    public GameObject[] GameTile = new GameObject [9];
    protected GameObject PlayerHpObject, PlayerArmorObject, PlayerStepObject, SpecialCounter;
    protected int PlayerArmor;
    public int PlayerHp;
    public int PlayerStepCounter = 0;
    public int PlayerLevel = 0;
    private int HighScore;
    public void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    public void Start()
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
        PlayerHp = PlayerPrefs.GetInt("PlayerHp", 3);
        PlayerArmor = PlayerPrefs.GetInt("PlayerArmor", 0);
        PlayerIndex = 4;
        PlayerLevel = PlayerPrefs.GetInt("PlayerLevel", 0);
        PlayerStepCounter = PlayerPrefs.GetInt("PlayerStepCounter", PlayerStepCounter);
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        AllTiles = Levels[PlayerLevel].Tiles;
        PlayerTile = AllTiles[0];
        RandomField();
        PlayerHpObject = GameObject.FindGameObjectWithTag("PlayerHp");
        PlayerArmorObject = GameObject.FindGameObjectWithTag("PlayerArmor");
        PlayerStepObject = GameObject.FindGameObjectWithTag("PlayerStep");
        PlayerHpObject.GetComponentInChildren<Text>().text = PlayerHp.ToString();
        PlayerArmorObject.GetComponentInChildren<Text>().text = PlayerArmor.ToString();
        PlayerStepObject.GetComponentInChildren<Text>().text = PlayerStepCounter.ToString();
    }
    public virtual void RandomField()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i == 4) { UpdateTile(i, 0); }
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
            PlayerLevel++;
            Mathf.Clamp(PlayerLevel, 0, Levels.Length);
            AllTiles = Levels[PlayerLevel].Tiles;
        }
        save();
    }

    public virtual void CheckDeath()
    {
        if (PlayerStepCounter > HighScore)
        {
            HighScore = PlayerStepCounter;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
        if (PlayerHp <= 0)
        {
            PlayerPrefs.SetInt("PlayerHp", 3);
            PlayerPrefs.SetInt("PlayerArmor", 0);
            PlayerPrefs.SetInt("PlayerLevel", 0);
            PlayerPrefs.SetInt("PlayerStepCounter", 0);
            Resett();
            SceneManager.LoadScene(0);
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
        PlayerStepObject.GetComponentInChildren<Text>().text = PlayerStepCounter.ToString();
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
    }
        public void Potion(int index)
        {
            PlayerHp += Current[index].DamHeal;
            PlayerHpObject.GetComponentInChildren<Text>().text = PlayerHp.ToString();
            UpdateTile(index, 0);
            TriggerRandomTile(index);
        }

        public void Monster(int index)
        {
            int LastPlayerHp = PlayerHp;
            PlayerHp -= (Current[index].DamHeal - PlayerArmor);
            PlayerArmor = 0;
            if (PlayerHp > LastPlayerHp) { PlayerHp = LastPlayerHp; }
            PlayerArmorObject.GetComponentInChildren<Text>().text = PlayerArmor.ToString();
            PlayerHpObject.GetComponentInChildren<Text>().text = PlayerHp.ToString();
            UpdateTile(index, 0);
            TriggerRandomTile(index);
        }

        public void Armor(int index)
        {
            PlayerArmor += Current[index].DamHeal;
            PlayerArmorObject.GetComponentInChildren<Text>().text = PlayerArmor.ToString();
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
            GameTile[CurrentInt].GetComponentInChildren<Text>().text = AllTiles[AllTileInt].DamHeal.ToString();
        }
        else
        {
            GameTile[CurrentInt].GetComponentInChildren<Text>().text = "";
        }
    }
    public void RandomTile(int Index)
    {
        UpdateTile(Index, Random.Range(1, AllTiles.Length));
        if (Current[Index].MonsterType.ToString() == "Monster")
        {
            GameTile[Index].GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            GameTile[Index].GetComponentInChildren<Text>().color = Color.white;
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
public class Level
{
    public Tile[] Tiles;
}

[System.Serializable]
public class Tile
{
    public Sprite sprite;
    public enum TypeMonster { Potion, Monster, Weapon, Armor}
    public TypeMonster MonsterType;
    public int DamHeal;
}