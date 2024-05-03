using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LOB_Fade : MonoBehaviour
{
    public Image _image;
    public bool _animation;
    [SerializeField] private float speed;

    public static LOB_Fade Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void StartAnimation()
    {
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.audioSource.gameObject.SetActive(false);
            MenuManager.Instance.soundManager.PlaySound(0);
        }
        _image.gameObject.SetActive(true);
        _animation = true;
    }

    private void Update()
    {

        if (_animation)
        {
            if (_image.color.a < 1)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a + Time.deltaTime * speed);
            }
            else
            {
                if (MenuManager.Instance != null)
                {
                    MenuManager.Instance.StartGame();
                }
                else
                {
                    SceneManager.LoadScene("UIDefeatVictory");
                }
            }
        }
    }
}