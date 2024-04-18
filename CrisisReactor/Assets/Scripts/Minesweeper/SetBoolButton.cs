using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBoolButton : MonoBehaviour
{
    [SerializeField] private List<bool> yellow = new();
    [SerializeField] private List<bool> red = new();
    [SerializeField] private List<bool> blue = new();
    [SerializeField] private List<bool> green = new();

    [SerializeField] private List<GameObject> button = new();
    [SerializeField] private Image backGround;

    public static int nbBoolSafe = 0;
    public static int maxBoolSafe;

    private void Start()
    {
        SetColor();
    }

    public void SetColor()
    {
        switch (Random.Range(0,4))
        {
            case 0:
                backGround.color = Color.red;
                SetBool(red);
                maxBoolSafe = SetMaxBoolSafe(red);
                break;
            case 1:
                backGround.color = Color.green;
                SetBool(green);
                maxBoolSafe = SetMaxBoolSafe(green);
                break;
            case 2:
                backGround.color = Color.blue;
                SetBool(blue);
                maxBoolSafe = SetMaxBoolSafe(blue);
                break;
            case 3:
                backGround.color = Color.yellow;
                SetBool(yellow);
                maxBoolSafe = SetMaxBoolSafe(yellow);
                break;
        }
    }

    public void SetBool(List<bool> list)
    {
        for (int i = 0; i < button.Count; i++)
        {
            button[i].GetComponent<ClickButton>().isBomb = list[i];
        }
    }

    private int SetMaxBoolSafe(List<bool> list)
    {
        int nbBool = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i])
            {
                nbBool++;
            }
        }
        return nbBool;
    }
}
