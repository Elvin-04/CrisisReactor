using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    public bool isBomb;

    public void OnClick()
    {
        if (isBomb)
        {
            gameObject.GetComponent<Image>().color = Color.red;    //Changer Sprite
            print("Dead");  //Enlever du timer
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;  //Changer Sprite
            SetBoolButton.nbBoolSafe++;
            if (SetBoolButton.nbBoolSafe >= SetBoolButton.maxBoolSafe)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
                print("Win");
            }
        }
    }
}
