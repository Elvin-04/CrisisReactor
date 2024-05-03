using UnityEngine;
using UnityEngine.SceneManagement;

public class WG_Return : MonoBehaviour
{
    public void ReturnLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
