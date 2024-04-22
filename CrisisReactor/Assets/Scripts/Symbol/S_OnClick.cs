using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_OnClick : MonoBehaviour
{
    public bool isGoodSprite;
    [SerializeField] private Image image;

    public void OnClick()
    {
        if (isGoodSprite)
        {
            image.color = Color.green;
            gameObject.GetComponent<Button>().interactable = false;
            S_GameManager.nbGoodprite--;
            print(S_GameManager.nbGoodprite);
            if (S_GameManager.nbGoodprite <= 0 )
            {
                SceneManager.LoadScene("Lobby");
            }
        }
        else
        {
            image.color = Color.red;
            LOB_Timer.instance.RemoveTimer(20);
        }
    }
}
