using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MiniGameLobbyManager : MonoBehaviour
{
    public void LeaveMiniGame(InputAction.CallbackContext context)
    {
        if(context.performed && SceneManager.GetActiveScene().name != "Lobby" && GameObject.Find("UI_UniversalDigicode(Clone)") == null && SceneManager.GetActiveScene().name != "UIDefeatVictory")
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
