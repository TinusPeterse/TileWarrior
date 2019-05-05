using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MageTutorial : Mage
{
    public Sprite Portal;
    protected int TimesRandom = 0;
    int LevelsDone = 0;
    private GameObject RestartButton;
    public override void Start()
    {
        base.Start();
        SpecialCounter = GameObject.FindGameObjectWithTag("KillingSpreeCounter");
        Restart();
    }
    public void Restart()
    {
        if (LevelsDone == 2)
        {
            SceneManager.LoadScene(0);
        }
        RessetStats();
        TimesRandom = 0;
        RandomField();
    }
    private void RessetStats()
    {
        PlayerPrefs.SetInt("PlayerHp", 3);
        PlayerPrefs.SetInt("PlayerArmor", 0);
        PlayerPrefs.SetInt("PlayerStepCounter", 0);
        PlayerPrefs.SetInt("SpecialCounter", 0);
        PlayerHp = 3;
        PlayerArmor = 0;
        PlayerStepCounter = 0;
        MageCounter = 0;
        if (PlayerHpObject == null) { return; }
        SpecialCounter.GetComponentInChildren<TextMeshProUGUI>().text = MageCounter.ToString();
        PlayerHpObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerHp.ToString();
        PlayerArmorObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerArmor.ToString();
        PlayerStepObject.GetComponentInChildren<TextMeshProUGUI>().text = PlayerStepCounter.ToString();
    }
    public override void Resett()
    {
        PlayerPrefs.DeleteKey("PlayerHp");
        PlayerPrefs.DeleteKey("PlayerArmor");
        PlayerPrefs.DeleteKey("PlayerStepCounter");
        PlayerPrefs.DeleteKey("PlayerLevel");
        PlayerPrefs.DeleteKey("SpecialCounter");
        base.Resett();
    }
    public override void RandomField()
    {
        if (LevelsDone == 0)
        {
            if (TimesRandom == 0)
            {
                SetHint("When your special ability counter hits 10 you activate your magical ability");
                for (int i = 0; i < 9; i++)
                {
                    if (i == 3) { ForceTile(i, 0); PlayerIndex = i;}
                    else if (i == 4) { ForceTile(i, 2); }
                    else if (i == 5) { ForceTile(i, 3); }
                    else if (i == 2) { ForceTile(i, 3); }
                    else if (i == 8) { ForceTile(i, 3); }
                    else { ForceTile(i, 1); }
                }
                TimesRandom++;
            }
            if (Current[5].sprite != AllTiles[3].sprite)
            {
                GameTile[4].GetComponent<SpriteRenderer>().sprite = Portal;
            }
            if (Current[5].sprite != AllTiles[3].sprite && Current[5].sprite != AllTiles[0].sprite)
            {
                LevelsDone++;
                TimesRandom = 0;
                RessetStats();
            }
        }
        if (LevelsDone == 1)
        {
            if (TimesRandom == 0)
            {
                SetHint("Killing Bunnys also gives 1 mana");
                for (int i = 0; i < 9; i++)
                {
                    if (i == 3) { ForceTile(i, 0); PlayerIndex = i; }
                    else if (i == 4) { ForceTile(i, 5); }
                    else if (i == 5) { ForceTile(i, 4); }
                    else if (i == 2) { ForceTile(i, 3); }
                    else if (i == 8) { ForceTile(i, 3); }
                    else { ForceTile(i, 1); }
                }
                TimesRandom++;
            }
            if (Current[5].sprite != AllTiles[4].sprite)
            {
                GameTile[4].GetComponent<SpriteRenderer>().sprite = Portal;
            }
            if (Current[5].sprite != AllTiles[4].sprite && Current[5].sprite != AllTiles[0].sprite)
            {
                LevelsDone++;
                TimesRandom = 0;
                RessetStats();
            }
        }
        if (LevelsDone == 2)
        {
            if (TimesRandom == 0)
            {
                TimesRandom++;
            }
            SetHint("You have completed the tutorial");
            RestartButton = GameObject.FindGameObjectWithTag("Restart");
            RestartButton.GetComponentInChildren<TextMeshProUGUI>().text = "Quit Tutorial";
        }
    }
    public override bool CheckBunnys(int index)
    {
        if (Current[index].sprite == AllTiles[4].sprite){ return true; }
        else return false;
    }

    public override void KillMonsters(int index)
    {
        if (index == PlayerIndex) { return; }
        while (Current[index].MonsterType == Tile.TypeMonster.Monster)
        {
            if (CheckBunnys(index))
            {
                MageCounter++;
            }
            ForceTile(index, 1);
        }
    }
    public override void CheckDeath()
    {
        if (PlayerHp <= 0)
        {
            PlayerPrefs.SetInt("PlayerHp", 3);
            PlayerPrefs.SetInt("PlayerArmor", 0);
            PlayerPrefs.SetInt("PlayerLevel", 0);
            PlayerPrefs.SetInt("PlayerStepCounter", 0);
            PlayerPrefs.SetInt("SpecialCounter", 0);
            Resett();
            SceneManager.LoadScene(5);
        }
    }

    public override void CheckTile(int index)
    {
        base.CheckTile(index);
        RandomField();
    }

    public override void TriggerRandomTile(int index)
    {
        ForceTile(PlayerIndex, 1);
        PlayerIndex = index;
    }
    public void ForceTile(int Index, int TileInt)
    {
        UpdateTile(Index, TileInt);
        if (Current[Index].MonsterType.ToString() == "Monster")
        {
            GameTile[Index].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            GameTile[Index].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }
}
