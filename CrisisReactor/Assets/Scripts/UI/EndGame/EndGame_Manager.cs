using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textEndGame;

    private void Start()
    {
        if (PlayerPrefs.GetString("Victory") != "")
        {
            textEndGame.text = "Victory\n" + "Timer : " + PlayerPrefs.GetString("Victory");
        }
        else
        {
            textEndGame.text = "Defeat";
        }
    }
    public void Restart()
    {
        LOB_Timer.instance.currentTime = PlayerPrefs.GetInt("Timer");
        LOB_Timer.instance.endGame = false;
        SceneManager.LoadScene("Lobby");
        int timer = PlayerPrefs.GetInt("Timer");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Timer", timer);
    }
    public void MainMenu()
    {
        LOB_Timer.instance.currentTime = LOB_Timer.instance.totalTime;
        LOB_Timer.instance.endGame = false;
        SceneManager.LoadScene("MainMenu");
        int timer = PlayerPrefs.GetInt("Timer");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Timer", timer);
    }
}
