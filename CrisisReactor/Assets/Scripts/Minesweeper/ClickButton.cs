using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    public bool isBomb;

    //interact with grid cell
    public void OnClick()
    {
        if (isBomb)
        {
            gameObject.GetComponent<Image>().color = Color.red;
            print("Dead"); 
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
            SetBoolButton.nbBoolSafe++;
            if (SetBoolButton.nbBoolSafe >= SetBoolButton.maxBoolSafe)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
                print("Win");
            }
        }
    }
}
