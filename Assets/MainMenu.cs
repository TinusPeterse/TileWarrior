using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : Turorial
{    
    public Character[] characters;
    public Character currentCharacter;
    public int SelectedCharacter = 0;
    #region MainMenu

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayTutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void NextCharacter()
    {
        if (SelectedCharacter + 1 > characters.Length - 1) { SelectedCharacter = 0; }
        else { SelectedCharacter++; }   
        SetCharacter(SelectedCharacter);
    }

    public void SetCharacter(int characterIndex)
    {
        GameTile[6].GetComponent<SpriteRenderer>().sprite = characters[characterIndex].sprite;
        AllTiles[0].sprite = characters[characterIndex].sprite;
        //ExpText.text = characters[characterIndex].Hint;
        //ExplanationButton.GetComponentInChildren<TextMeshProUGUI>().text = characters[characterIndex].name;
    }
    #endregion
    public override void CheckDeath()
    {
        //Empty
    }
    public override void Resett()
    {
        TimesRandom = 0;
        AllTiles = Levels[PlayerLevel].Tiles;
        PlayerPrefs.DeleteAll();
        RandomField();
    }
    public override void RandomField()
    {
        if (TimesRandom == 0)
        {
            TimesRandom++;
            for (int i = 0; i < 9; i++)
            {
                switch (i)
                {
                    case 0: ForceTile(i, 0); break;
                    case 1: ForceTile(i, 1); break;
                    case 2: ForceTile(i, 2); break;
                    case 3: ForceTile(i, 3); break;
                    case 4: ForceTile(i, 1); break;
                    case 5: ForceTile(i, 4); break;
                    case 6: ForceTile(i, 5); break;
                    case 7: ForceTile(i, 1); break;
                    case 8: ForceTile(i, 6); break;
                }
            }
        }
    }
    public override void Update()
    {
        HandleInput();
    }
    public override void CheckTile(int index)
    {
        UpdateTile(index, 0); TriggerRandomTile(index);

        for (int i = 0; i < 9; i++)
        {
            if (GameTile[i].GetComponent<SpriteRenderer>().sprite == AllTiles[0].sprite)
            {
                switch (i)
                {
                    case 0: break;
                    case 1: break;
                    case 2: PlayTutorial(); break;
                    case 3: PlayGame(); break;
                    case 4: break;
                    case 5: ResetAll(); break;
                    case 6: NextCharacter(); break;
                    case 7: break;
                    case 8: /*Store*/ break;
                }
            }
        }
    }
}

[System.Serializable]
public class Character
{
    public string name;
    public Sprite sprite;
    public string Hint;
}
