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
    private MG_SoundManager soundManager;
    private bool win;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("soundManager").GetComponent<MG_SoundManager>();
        initCase = currentCase;
    }
    public void Move(string direction)
    {
        if (!win)
        {
            soundManager.PlaySound(0);
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
    }

    public void SetColor(bool canGo, int addCurrentCase) 
    {
        if (canGo)
        {
            cases[currentCase].gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, 1);
            currentCase += addCurrentCase;
            cases[currentCase].gameObject.GetComponent<Image>().color += new Color(0, 0, 0, 1);
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
            soundManager.PlaySound(1);
            Invoke("OnVictory", 1.10f);
            win = true;
        }
    }

    private void OnVictory()
    {
            PlayerPrefs.SetInt("MiniGame1", 1);
            SceneManager.LoadScene("Lobby");
            Debug.Log("Win");
    }

    public void ResetCase()
    {
        soundManager.PlaySound(2);
        cases[currentCase].gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, 1);
        currentCase = initCase;
        cases[currentCase].gameObject.GetComponent<Image>().color += new Color(0, 0, 0, 1);
        Debug.Log("Dead");
        LOB_Timer.instance.RemoveTimer(20);
    }

    public void Update()
    {
        if (!win)
        {
            MoveWithArrow();
        }
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
