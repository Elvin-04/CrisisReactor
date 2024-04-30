using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    public bool isBomb;
    private MG_SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("soundManager").GetComponent<MG_SoundManager>();
    }

    //interact with grid cell

    private void OnLoose()
    {
        SetBoolButton.nbBoolSafe = 0;
        LOB_Timer.instance.RemoveTimer(20);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClick()
    {
        if(GameObject.FindGameObjectWithTag("soundManager").GetComponent<SetBoolButton>().getCanPlay())
            if (isBomb)
            {
                GameObject.FindGameObjectWithTag("soundManager").GetComponent<SetBoolButton>().OnBombExplode();
                soundManager.PlaySound(1);
                print("Dead");
                gameObject.GetComponent<Image>().sprite = SetBoolButton.Instance.spriteRed;
                Invoke("OnLoose", 1f);
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = SetBoolButton.Instance.spriteYellow;
                gameObject.GetComponent<Button>().interactable = false;
                SetBoolButton.nbBoolSafe++;
                
                if (SetBoolButton.nbBoolSafe >= SetBoolButton.maxBoolSafe)
                {
                    SetBoolButton.nbBoolSafe = 0;
                    soundManager.PlaySound(2);
                    Invoke("OnVictory", 1.10f);
                }
                else
                {
                    soundManager.PlaySound(0);
                }
            }
    }
    private void OnVictory()
    {
        PlayerPrefs.SetInt("MiniGame2", 1);
        GameObject.Find("MultiSceneManager").GetComponent<MiniGameLobbyManager>().LeaveMiniGame();
        Debug.Log("Win");
    }
}
