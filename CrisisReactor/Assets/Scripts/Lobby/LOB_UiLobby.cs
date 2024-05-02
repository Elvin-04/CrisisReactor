using System.Collections;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LOB_UiLobby : MonoBehaviour
{
    [SerializeField] private GameObject uiRestart;
    [SerializeField] private GameObject uiMainMenu;
    [SerializeField] private GameObject uiQuit;
    [SerializeField] private Image uiFade;
    [SerializeField] private GameObject buttonRed;
    [SerializeField] private GameObject uiSettings;
    [SerializeField] private Toggle _toggleFullScreen;
    [SerializeField] private Slider sliderGeneral;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    private MG_SoundManager soundManager;
    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("soundManager").GetComponent<MG_SoundManager>();
        sliderGeneral.value = PlayerPrefs.GetFloat("General");
        sliderMusic.value = PlayerPrefs.GetFloat("Music");
        sliderSFX.value = PlayerPrefs.GetFloat("SFX");
        _toggleFullScreen.isOn = Screen.fullScreen;
        if (PlayerPrefs.GetInt("Game") == 1)
        {
            uiFade.gameObject.SetActive(false);
            buttonRed.gameObject.SetActive(false);
        }
        else
        {
            uiFade.gameObject.SetActive(true);
            Camera.main.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>().renderPostProcessing = true;
        }
    }

    public void StartGame()
    {
        soundManager.PlaySound(0);
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
    public void OpenUiSetting()
    {
        uiSettings.SetActive(true);
    }
    public void NoButton()
    {
        uiMainMenu.SetActive(false);
        uiRestart.SetActive(false);
        uiQuit.SetActive(false);
    }
    public void YesButtonMainMenu()
    {
        PlayerPrefs.SetFloat("General", sliderGeneral.value);
        PlayerPrefs.SetFloat("Music", sliderMusic.value);
        PlayerPrefs.SetFloat("SFX", sliderSFX.value);
        DeletePlayerPref();
        PlayerPrefs.SetInt("Sound", 1);
        SceneManager.LoadScene("MainMenu");
    }
    public void YesButtonRestart()
    {
        LOB_Timer.instance.currentTime = PlayerPrefs.GetInt("Timer");
        SceneManager.LoadScene("Lobby");
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
    public void CloseSettings()
    {
        uiSettings.SetActive(false);
        GetValueSlider();
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void GetValueSlider()
    {
        PlayerPrefs.SetFloat("General", sliderGeneral.value);
        PlayerPrefs.SetFloat("Music", sliderMusic.value);
        PlayerPrefs.SetFloat("SFX", sliderSFX.value);
        PlayerPrefs.SetInt("Sound", 1);
    }
    public void QuitGame()
    {
        uiQuit.SetActive(true);
    }
    public void YesButtonQuit()
    {
        Application.Quit();
        DeletePlayerPref();
    }
}
