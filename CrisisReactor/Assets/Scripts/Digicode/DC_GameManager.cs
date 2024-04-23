using TMPro;
using UnityEngine;

public class DC_GameManager : MonoBehaviour
{
    private string enteredCode;
    private string waitedCode;
    [SerializeField] private TextMeshProUGUI enteredCodeText;
    [SerializeField] private MG_SoundManager soundManager;

    void OnEnable()
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
        soundManager.PlaySound(0);
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

    private void OnWin()
    {
            Debug.Log("winned");
            DC_DigicodeCodesList.correctCodes.Remove(enteredCode);
            Destroy(transform.parent.gameObject);
    }

    private void CheckCode()
    {
        if(enteredCode == waitedCode)
        {
            soundManager.PlaySound(2);

            Invoke("OnWin", 1.25f);
           
        }
        else
        {
            soundManager.PlaySound(1);
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
