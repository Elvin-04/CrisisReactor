using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MG_GameManager : MonoBehaviour
{

    private Sprite tempoCircle;
    private Animator animator;
    private InputField inputField;
    private string waitedWord;
    private Dictionary<char, string> morseCode = new Dictionary<char, string>()
    {	{'A', ".-"},{'B', "-..."},{'C', "-.-."},{'D', "-.."},{'E', "."},{'F', "..-."},{'G', "--."},{'H', "...."},{'I', ".."},
        {'J', ".---"},{'K', "-.-"},{'L', ".-.."},{'M', "--"},{'N', "-."},{'O', "---"},{'P', ".--."},{'Q', "--.-"},{'R', ".-."},
        {'S', "..."},{'T', "-"},{'U', "..-"},{'V', "...-"},{'W', ".--"},{'X', "-..-"},{'Y', "-.--"},{'Z', "--.."},{'0', "-----"},
        {'1', ".----"},{'2', "..---"},{'3', "...--"},{'4', "....-"},{'5', "....."},{'6', "-...."},{'7', "--..."},{'8', "---.."},
        {'9', "----."}
    };

    

    void Awake()
    {
        RandomWord();
    }

    private void RandomWord()
    {
        waitedWord = "";
        for (int i = 0; i < 3; i++)
        {
            waitedWord = waitedWord + morseCode.ElementAt(Random.Range(0, morseCode.Count)).Key;
        }
        Debug.Log("mot attendu = " + waitedWord);

    }

    public void PlayAnimButton()
    {

    }

    public void ConfirmWord(string commitedWord)
    {
        CheckWord(commitedWord);
    }

    private void CheckWord(string commitedWord)
    {
        if(commitedWord == waitedWord)
        {
            Debug.Log("win");
        }
    }



}
