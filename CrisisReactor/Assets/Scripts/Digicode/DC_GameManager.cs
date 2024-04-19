using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DC_GameManager : MonoBehaviour
{
    private string enteredCode;
    private string waitedCode;
    [SerializeField] private TextMeshProUGUI enteredCodeText;

    void Awake()
    {
        InitCodeFromPool();
    }

    private void InitCodeFromPool()
    {
        int randomizedIndex = Random.Range(0, DC_DigicodeCodesList.correctCodes.Count - 1);
        waitedCode = DC_DigicodeCodesList.correctCodes[randomizedIndex];
        Debug.Log("good code = " + waitedCode);
    }

    public void enterNumber(int _newNumber)
    {
        if(enteredCode != null)
        {
            if(enteredCode.Length < 4)
            {
                enteredCode += _newNumber;
                UpadteEnteredTextUI();
            }
        }
        else
        {
            enteredCode += _newNumber;
            UpadteEnteredTextUI();
        }

    }

    private void CheckCode()
    {
        if(enteredCode == waitedCode)
        {
            Debug.Log("winned");
            DC_DigicodeCodesList.correctCodes.Remove(enteredCode);
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            Debug.Log("wrong code");
            enteredCode = "";
            UpadteEnteredTextUI();
            InitCodeFromPool();
        }
    }

    public void ValidCode()
    {
        CheckCode();
    }
    
    private void UpadteEnteredTextUI()
    {
        enteredCodeText.text = enteredCode;
    }
}
