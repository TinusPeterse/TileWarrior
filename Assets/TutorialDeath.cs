using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialDeath : MonoBehaviour
{
    public void PlayTutorial()
    {
        SceneManager.LoadScene(2);
    }
}
