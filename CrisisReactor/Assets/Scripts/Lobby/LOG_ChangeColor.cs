using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LOG_ChangeColor : MonoBehaviour
{
    [SerializeField] private List<Image> imageMiniGame = new();
    [SerializeField] private List<Button> buttonLobby = new();
    private int nbMiniGameFinish;
    void Start()
    {
        for (int i = 0; i < imageMiniGame.Count; i++)
        {
            if (PlayerPrefs.GetInt("MiniGame" + (i + 1).ToString()) == 1)
            {
                imageMiniGame[i].color = new Color(0, 1, 0, 177f / 255f);
                buttonLobby[i].interactable = false;
                nbMiniGameFinish++;
                if (nbMiniGameFinish >= buttonLobby.Count)
                {
                    PlayerPrefs.SetString("Victory", ((int)LOB_Timer.instance.currentTime / 60).ToString("00") + ":" + ((int)LOB_Timer.instance.currentTime % 60).ToString("00"));
                    SceneManager.LoadScene("UIDefeatVictory");
                }
            }
            else
            {

                imageMiniGame[i].color = new Color(1, 0, 0, 177f / 255f);
            }
        }
    }
}
