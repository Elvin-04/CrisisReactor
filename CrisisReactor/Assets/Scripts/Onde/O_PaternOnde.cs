using TMPro;
using UnityEngine;

public class O_PaternOnde : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCode;
    public static float scaleX;
    public static float scaleY;
    void Start()
    {
        SetCode();
    }

    private void SetCode()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                textCode.text = "WILLY";
                scaleX = 5.0f;
                scaleY = 2.5f;
                break;
            case 1:
                textCode.text = "STEPH";
                scaleX = 4.0f;
                scaleY = 1.0f;
                break;
            case 2:
                textCode.text = "PATRIK";
                scaleX = 1.0f;
                scaleY = 2f;
                break;
            case 3:
                textCode.text = "TAGBO";
                scaleX = 0.75f;
                scaleY = 0.5f;
                break;
            default:
                print("No Code");
                break;
        }
    }
}
