using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textEndGame;
    [SerializeField] private Image background;
    private MG_SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("soundManager").GetComponent<MG_SoundManager>();
        if (PlayerPrefs.GetString("Victory") != "")
        {
            textEndGame.text = "Victory\n" + "Timer : " + PlayerPrefs.GetString("Victory");
        }
        else
        {
            background.color = Color.black;
            background.sprite = null;
            textEndGame.text = "Defeat";
            soundManager.PlaySound(0);
        }
    }
    public void Restart()
    {
        LOB_Timer.instance.currentTime = PlayerPrefs.GetInt("Timer");
        LOB_Timer.instance.endGame = false;
        SceneManager.LoadScene("Lobby");
        DeletePlayerPref();
    }
    public void MainMenu()
    {
        LOB_Timer.instance.currentTime = LOB_Timer.instance.totalTime;
        LOB_Timer.instance.endGame = false;
        SceneManager.LoadScene("MainMenu");
        DeletePlayerPref();
    }
    private void DeletePlayerPref()
    {
        int timer = PlayerPrefs.GetInt("Timer");
        int sound = PlayerPrefs.GetInt("Sound");
        float general = PlayerPrefs.GetFloat("General");
        float music = PlayerPrefs.GetFloat("Music");
        float sfx = PlayerPrefs.GetFloat("SFX");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Timer", timer);
        PlayerPrefs.SetInt("Sound", sound);
        PlayerPrefs.SetFloat("General", general);
        PlayerPrefs.SetFloat("Music", music);
        PlayerPrefs.SetFloat("SFX", sfx);
    }
}
