using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    public bool isBomb;

    //interact with grid cell
    public void OnClick()
    {
        if (isBomb)
        {
            SetBoolButton.nbBoolSafe = 0;
            print("Dead");
            LOB_Timer.instance.RemoveTimer(20);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
            gameObject.GetComponent<Button>().interactable = false;
            SetBoolButton.nbBoolSafe++;
            if (SetBoolButton.nbBoolSafe >= SetBoolButton.maxBoolSafe)
            {
                SetBoolButton.nbBoolSafe = 0;
                SceneManager.LoadScene("Lobby");
                PlayerPrefs.SetInt("MiniGame2", 1);
                print("Win");
            }
        }
    }
}
