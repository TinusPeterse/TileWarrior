using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class HighScoreScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private int MageHighScore, WarriorHighScore;
    void Start()
    {
        MageHighScore = PlayerPrefs.GetInt("MageHighScore");
        WarriorHighScore = PlayerPrefs.GetInt("WarriorHighScore");
        text.text = "Warrior : " + WarriorHighScore.ToString() + "\nMage : " + MageHighScore.ToString();
    }
    public void Update()
    {
        MageHighScore = PlayerPrefs.GetInt("MageHighScore");
        WarriorHighScore = PlayerPrefs.GetInt("WarriorHighScore");
        text.text = "Warrior : " + WarriorHighScore.ToString() + "\nMage : " + MageHighScore.ToString();
    }
}
