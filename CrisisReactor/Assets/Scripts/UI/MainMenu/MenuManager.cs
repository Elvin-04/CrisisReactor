using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditMenu;
    [SerializeField] private Toggle _toggleFullScreen;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private TMP_Dropdown dropDownDifficulty;
    [SerializeField] private Slider sliderGeneral;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Volume volume;
    [HideInInspector] public MG_SoundManager soundManager;
    public static MenuManager Instance;
    public AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        soundManager = soundManager = GameObject.FindGameObjectWithTag("soundManager").GetComponent<MG_SoundManager>();
        SetVolumeSlider();
        _toggleFullScreen.isOn = Screen.fullScreen;
        switch (PlayerPrefs.GetInt("Timer"))
        {
            case 300:
                dropDownDifficulty.value = 2;
                break;
            case 600:
                dropDownDifficulty.value = 0;
                break;
            case 450:
                dropDownDifficulty.value = 1;
                break;
            case 0:
                dropDownDifficulty.value = 1;
                break;
        }
    }

    public void OpenPlayPanel()
    {
        playPanel.SetActive(true);
        mainMenuButton.SetActive(false);
        Camera.main.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>().renderPostProcessing = false;
    }

    public void BackMainMenu()
    {
        playPanel.SetActive(false);
        mainMenuButton.SetActive(true);
        Camera.main.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>().renderPostProcessing = true;
    }

    public void StartGame()
    {
        PlayerPrefs.SetFloat("General", sliderGeneral.value);
        PlayerPrefs.SetFloat("Music", sliderMusic.value);
        PlayerPrefs.SetFloat("SFX", sliderSFX.value);
        switch (dropDownDifficulty.value)
        {
            case 0:
                PlayerPrefs.SetInt("Timer", 600);
                break;
            case 1:
                PlayerPrefs.SetInt("Timer", 450);
                break;
            case 2:
                PlayerPrefs.SetInt("Timer", 300);
                break;
            default:
                print("No value");
                break;
        }
        if (LOB_Timer.instance != null)
        {
            LOB_Timer.instance.currentTime = PlayerPrefs.GetInt("Timer");
        }
        SceneManager.LoadScene("Lobby");
    }
    public void Settings()
    {
        mainMenuButton.SetActive(false);
        settingsMenu.SetActive(true);
        Bloom bloom = new Bloom();
        volume.profile.TryGet(out bloom);
        bloom.active = false;
    }
    public void CloseSettings()
    {
        mainMenuButton.SetActive(true);
        settingsMenu.SetActive(false);
        Bloom bloom = new Bloom();
        volume.profile.TryGet(out bloom);
        bloom.active = true;
    }
    public void Credit()
    {
        creditMenu.SetActive(true);
    }
    public void CloseCredit()
    {
        creditMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
    public void SetVolumeSlider()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            sliderGeneral.value = PlayerPrefs.GetFloat("General");
            sliderMusic.value = PlayerPrefs.GetFloat("Music");
            sliderSFX.value = PlayerPrefs.GetFloat("SFX");
        }
    }
}
