using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MiniGameLobbyManager : MonoBehaviour
{

    [SerializeField] public GameObject backCanvas;

    public static MiniGameLobbyManager instance;

    private void Awake()
    {
        instance = this;
        backCanvas.SetActive(false);
    }


    public void LeaveMiniGame(InputAction.CallbackContext context)
    {
        if(context.performed && SceneManager.GetActiveScene().name != "Lobby")
        {
            LeaveMiniGame();
        }
    }

    public void LeaveMiniGame()
    {
        SceneManager.LoadScene("Lobby");
        backCanvas.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
