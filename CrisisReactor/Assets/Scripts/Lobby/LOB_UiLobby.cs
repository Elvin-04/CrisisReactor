using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LOB_UiLobby : MonoBehaviour
{
    public void Restart()
    {
        LOB_Timer.instance.currentTime = PlayerPrefs.GetInt("Timer");
        SceneManager.LoadScene("Lobby");
        int timer = PlayerPrefs.GetInt("Timer");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Timer", timer);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        int timer = PlayerPrefs.GetInt("Timer");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Timer", timer);
    }
}
