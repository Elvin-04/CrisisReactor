using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SpriteButton : MonoBehaviour
{
    public static S_SpriteButton Instance;
    public Sprite goodSprite;
    public Sprite badSprite;

    private void Awake()
    {
        Instance = this;
    }

    public void ReturnLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
