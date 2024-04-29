using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_OnClick : MonoBehaviour
{
    public bool isGoodSprite;
    private MG_SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("soundManager").GetComponent<MG_SoundManager>();
    }

    //compare selected symbol with waited symbol

    public void OnClick()
    {
        if (isGoodSprite)
        {
            gameObject.GetComponent<Image>().sprite = S_SpriteButton.Instance.goodSprite;
            gameObject.GetComponent<Button>().interactable = false;
            S_GameManager.nbGoodprite--;
            if (S_GameManager.nbGoodprite <= 0 )
            {
                soundManager.PlaySound(1);
                Invoke("OnVictory", 1.10f);
            }
            else
            {
                soundManager.PlaySound(0);
            }
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = S_SpriteButton.Instance.badSprite;
            soundManager.PlaySound(2);
            LOB_Timer.instance.RemoveTimer(20);
        }
    }
    private void OnVictory()
    {
        PlayerPrefs.SetInt("MiniGame6", 1);
        SceneManager.LoadScene("Lobby");
        Debug.Log("Win");
    }
}
