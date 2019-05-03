using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI ExpText;
    public GameObject[] button = new GameObject[6];
    public GameObject PlayButton;
    public GameObject ExplanationButton;
    public Character[] characters;
    public Character current;
    public int SelectedCharacter = 0;
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
    public void Explanation()
    {
        ExpText.enabled = !ExpText.enabled;
        for (int i = 0; i < 6; i++)
        {
            button[i].GetComponent<Button>().enabled = !button[i].GetComponent<Button>().enabled;
            button[i].GetComponent<Image>().enabled = !button[i].GetComponent<Image>().enabled;
            if (button[i].GetComponentInChildren<TextMeshProUGUI>() != null)
            {
                button[i].GetComponentInChildren<TextMeshProUGUI>().enabled = !button[i].GetComponentInChildren<TextMeshProUGUI>().enabled;
            }
        }
    }
    public void NextCharacter()
    {
        if (SelectedCharacter + 1 > characters.Length - 1) { SelectedCharacter = 0; }
        else { SelectedCharacter++; }   
        SetCharacter(SelectedCharacter);
    }

    public void SetCharacter(int characterIndex)
    {
        PlayButton.GetComponent<Image>().sprite = characters[characterIndex].sprite;
        ExpText.text = characters[characterIndex].Hint;
        ExplanationButton.GetComponentInChildren<TextMeshProUGUI>().text = characters[characterIndex].name;
    }
}
[System.Serializable]
public class Character
{
    public string name;
    public Sprite sprite;
    public string Hint;
}
