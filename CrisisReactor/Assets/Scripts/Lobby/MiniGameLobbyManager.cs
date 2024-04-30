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
        if(context.performed && SceneManager.GetActiveScene().name != "Lobby" && GameObject.Find("UI_UniversalDigicode(Clone)") == null)
        {
            LeaveMiniGame();
        }
    }

    public void LeaveMiniGame()
    {
        
        if(GameObject.Find("UI_UniversalDigicode(Clone)") == null)
        {
            backCanvas.SetActive(false);
            SceneManager.LoadScene("Lobby");
        }
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
