using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class HighScoreScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        text.text = "Warrior : " + PlayerPrefs.GetInt("WarriorHighScore").ToString() + "\nMage : " + PlayerPrefs.GetInt("MageHighScore").ToString();
    }
}
