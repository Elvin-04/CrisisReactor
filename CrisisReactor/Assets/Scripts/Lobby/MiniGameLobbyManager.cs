using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MiniGameLobbyManager : MonoBehaviour
{
    public void LeaveMiniGame(InputAction.CallbackContext context)
    {
        if(context.performed && SceneManager.GetActiveScene().name != "Lobby")
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
