using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class L_Player : MonoBehaviour
{
    [SerializeField] private List<L_Case> cases = new();
    [SerializeField] private int currentCase;
    private int initCase;
    [SerializeField] private int lenght;

    private void Start()
    {
        initCase = currentCase;
    }
    public void Move(string direction)
    {
        switch (direction)
        {
            case "Up":
                SetColor(cases[currentCase].canGoUp, -lenght);
                break;
            case "Down":
                SetColor(cases[currentCase].canGoDown, lenght);
                break;
            case "Left":
                SetColor(cases[currentCase].canGoLeft, -1);
                break;
            case "Right":
                SetColor(cases[currentCase].canGoRight, 1);
                break;
            default:
                print("No direction");
                break;
        }
    }

    public void SetColor(bool canGo, int addCurrentCase) 
    {
        if (canGo)
        {
            cases[currentCase].gameObject.GetComponent<Image>().color = Color.gray;
            currentCase += addCurrentCase;
            cases[currentCase].gameObject.GetComponent<Image>().color = Color.green;
            CheckWin();
        }
        else
        {
            ResetCase();
        }
    }

    public void CheckWin()
    {
        if (cases[currentCase].isEndCase)
        {
            PlayerPrefs.SetInt("MiniGame1", 1);
            SceneManager.LoadScene("Lobby");
            Debug.Log("Win");
        }
    }

    public void ResetCase()
    {
        cases[currentCase].gameObject.GetComponent<Image>().color = Color.gray;
        currentCase = initCase;
        cases[currentCase].gameObject.GetComponent<Image>().color = Color.green;
        Debug.Log("Dead");
        LOB_Timer.instance.RemoveTimer(20);
    }

    public void Update()
    {
        MoveWithArrow();
    }

    public void MoveWithArrow()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move("Left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move("Right");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move("Up");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move("Down");
        }
    }
}
