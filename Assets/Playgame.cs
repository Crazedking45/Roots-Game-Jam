using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Playgame : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial_Level_1");
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
