using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditMenu;
    [SerializeField] private Toggle _toggleFullScreen;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject mainMenuButton;

    private void Start()
    {
        _toggleFullScreen.isOn = Screen.fullScreen;
    }

    public void OpenPlayPanel()
    {
        playPanel.SetActive(true);
        mainMenuButton.SetActive(false);
    }

    public void BackMainMenu()
    {
        playPanel.SetActive(false);
        mainMenuButton.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void Settings()
    {
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
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
}
