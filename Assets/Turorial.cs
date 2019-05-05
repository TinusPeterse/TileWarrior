using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Turorial : Warrior
{
    public Sprite Portal;
    protected int TimesRandom = 0;
    int LevelsDone = 0;
    private GameObject RestartButton;

    public void Restart () 
    {
        if (LevelsDone == 5)
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
        PlayerHp = 3;
        PlayerArmor = 0;
        PlayerStepCounter = 0;
        if (PlayerHpObject == null) { return; }
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
        base.Resett();
    }
    public override void RandomField()
    {
        if (LevelsDone == 0)
        {
            if (TimesRandom == 0)
            {
                RessetStats();
                SetHint("Potions Restore Health");
                for (int i = 0; i < 9; i++)
                {
                    if (i == 0) { ForceTile(i, 2); }
                    else if (i == 3) { ForceTile(i, 0); PlayerIndex = i; }
                    else if (i == 5) { ForceTile(i, 1); }
                    else { ForceTile(i, 2); }
                }
                TimesRandom++;
            }
            if (Current[5].sprite != AllTiles[1].sprite)
            {
                GameTile[4].GetComponent<SpriteRenderer>().sprite = Portal;
            }
            if (Current[5].sprite != AllTiles[1].sprite && Current[5].sprite != AllTiles[0].sprite)
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
                SetHint("Monsters deal damage upon contact");
                for (int i = 0; i < 9; i++)
                {
                    if (i == 0) { ForceTile(i, 2); }
                    else if (i == 3) { ForceTile(i, 0); PlayerIndex = i; }
                    else if (i == 5) { ForceTile(i, 3); Current[i].DamHeal = 2; GameTile[i].GetComponentInChildren<TextMeshProUGUI>().text = 2.ToString(); }
                    else { ForceTile(i, 2); }
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
        if (LevelsDone == 2)
        {
            SetHint("Armor blocks damage taken from the first monster you hit");
            if (TimesRandom == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (i == 0) { ForceTile(i, 2); }
                    else if (i == 3) { ForceTile(i, 0); PlayerIndex = i; }
                    else if (i == 4) { ForceTile(i, 4); }
                    else if (i == 5) { ForceTile(i, 3); Current[i].DamHeal = 3; GameTile[i].GetComponentInChildren<TextMeshProUGUI>().text = 4.ToString(); }
                    else { ForceTile(i, 2); }
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
        if (LevelsDone == 3)
        {
            SetHint("Once you are on a killing spree you become invulnerable");
            if (TimesRandom == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (i == 0) { ForceTile(i, 0); PlayerIndex = i; }
                    else if (i == 1) { ForceTile(i, 1); Current[i].DamHeal = 4; GameTile[i].GetComponentInChildren<TextMeshProUGUI>().text = 4.ToString(); }
                    else if (i == 2) { ForceTile(i, 4); }
                    else if (i == 5) { ForceTile(i, 3); }
                    else if (i == 6) { ForceTile(i, 3); }
                    else if (i == 7) { ForceTile(i, 3); }
                    else if (i == 8) { ForceTile(i, 3); }
                    else if (i == 3) { ForceTile(i, 3); }
                    else { ForceTile(i, 2); }
                }
                TimesRandom++;
            }
            if (Current[3] != AllTiles[3] && Current[5] != AllTiles[3] && Current[6] != AllTiles[3] && Current[7] != AllTiles[3] && Current[8] != AllTiles[3])
            {
                GameTile[4].GetComponent<SpriteRenderer>().sprite = Portal;
            }
            if (Current[3] == AllTiles[2] && Current[5] == AllTiles[2] && Current[6] == AllTiles[2] && Current[7] == AllTiles[2] && Current[8] == AllTiles[2] && Current[4].sprite == AllTiles[0].sprite)
            {
                LevelsDone++;
                TimesRandom = 0;
                RessetStats();
            }
        }
        if (LevelsDone == 4)
        {
            SetHint("Your killing spree stops if you hit something else then a foe");
            if (TimesRandom == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (i == 8) { ForceTile(i, 0); PlayerIndex = i; }
                    else if (i == 5) { ForceTile(i, 5); }
                    else if (i == 3) { ForceTile(i, 4); Current[i].DamHeal = 15; GameTile[i].GetComponentInChildren<TextMeshProUGUI>().text = 15.ToString(); }
                    else if (i == 0 || i == 1 || i == 2 || i == 4 || i == 6 || i == 7) { ForceTile(i, 3); }
                    else { ForceTile(i, 2); }
                }
                TimesRandom++;
            }
            int stupidCounter= 0;
            for (int i = 0; i < 9; i++)
            {
                if (Current[i] != AllTiles[3] )
                {
                    stupidCounter++;
                }
            }
            CheckDeath();
            if (stupidCounter == 9)
            {
                LevelsDone++;
                TimesRandom = 0;
                RessetStats();
            }
        }
        if (LevelsDone == 5)
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
    
    public override void CheckDeath()
    {
        if (PlayerHp <= 0)
        {
            PlayerPrefs.SetInt("PlayerHp", 3);
            PlayerPrefs.SetInt("PlayerArmor", 0);
            PlayerPrefs.SetInt("PlayerLevel", 0);
            PlayerPrefs.SetInt("PlayerStepCounter", 0);
            Resett();
            SceneManager.LoadScene(3);
        }
    }

    public override void CheckTile(int index)
    {
        base.CheckTile(index);
        RandomField();
    }

    public override void TriggerRandomTile(int index)
    {
        ForceTile(PlayerIndex, 2);
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
