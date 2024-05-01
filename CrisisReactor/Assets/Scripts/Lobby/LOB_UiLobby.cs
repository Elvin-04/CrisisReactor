using System.Collections;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LOB_UiLobby : MonoBehaviour
{
    [SerializeField] private GameObject uiRestart;
    [SerializeField] private GameObject uiMainMenu;
    [SerializeField] private Image uiFade;
    [SerializeField] private GameObject buttonRed;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Game") == 1)
        {
            uiFade.gameObject.SetActive(false);
            buttonRed.gameObject.SetActive(false);
        }
        else
        {
            Camera.main.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>().renderPostProcessing = true;
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Game", 1);
        Camera.main.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>().renderPostProcessing = false;
        StartCoroutine(StartFadeOut(0.005f));
    }
    IEnumerator StartFadeOut(float second)
    {
        uiFade.color -= new Color(0,0,0,Time.deltaTime * 2);
        if (uiFade.color.a > 0.0f)
        {
            yield return new WaitForSeconds(second);
            StartCoroutine(StartFadeOut(second));
        }
        else
        {
            buttonRed.SetActive(false);
            uiFade.gameObject.SetActive(false);
        }
        yield return null;
    }
    public void Restart()
    {
        uiRestart.SetActive(true);
    }

    public void MainMenu()
    {
        uiMainMenu.SetActive(true);
    }
    public void NoButton()
    {
        uiMainMenu.SetActive(false);
        uiRestart.SetActive(false);
    }
    public void YesButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        int timer = PlayerPrefs.GetInt("Timer");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Timer", timer);
    }
    public void YesButtonRestart()
    {
        LOB_Timer.instance.currentTime = PlayerPrefs.GetInt("Timer");
        SceneManager.LoadScene("Lobby");
        int timer = PlayerPrefs.GetInt("Timer");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Timer", timer);
    }
}
