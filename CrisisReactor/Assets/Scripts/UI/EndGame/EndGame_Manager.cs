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

        GameObject[] getBackButtonCanvas = GameObject.FindGameObjectsWithTag("BackButtonCanvas");
        foreach (GameObject go in getBackButtonCanvas)
        {
            go.SetActive(false);
        }
    }
    public void Restart()
    {
        LOB_Timer.instance.currentTime = LOB_Timer.instance.totalTime;
        LOB_Timer.instance.endGame = false;
        SceneManager.LoadScene("Lobby");
        PlayerPrefs.DeleteAll();
    }
    public void MainMenu()
    {
        LOB_Timer.instance.currentTime = LOB_Timer.instance.totalTime;
        LOB_Timer.instance.endGame = false;
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.DeleteAll();
    }
}
