using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


public class MG_GameManager : MonoBehaviour
{

    [SerializeField] private Image tempoCircle;
    [SerializeField] private MG_SoundManager soundManager;
    private Animator animator;
    private string waitedWord;

    //morseCode dictionnary is used to translate from char to morse code
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
            waitedWord = waitedWord + morseCode.ElementAt(UnityEngine.Random.Range(0, morseCode.Count)).Key;
        }
        Debug.Log("mot attendu = " + waitedWord);
        PlayAnimButton();
    }

    public void PlayAnimButton()
    {
        StartCoroutine(ExecuteMorse());
    }

    public void ConfirmWord(string _commitedWord)
    {
        CheckWord(_commitedWord);
    }

    private void CheckWord(string _commitedWord)
    {
        if(string.Equals(_commitedWord, waitedWord, StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Gagné");
            PlayerPrefs.SetInt("MiniGame5", 1);
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            Debug.Log("Perdu");
            LOB_Timer.instance.RemoveTimer(20);
        }
    }


    //execute morse sequence with sound and sprite swapping
    IEnumerator ExecuteMorse()
    {
        while (true)
        {
            foreach (char _letter in waitedWord)
            {
                if (morseCode.TryGetValue(_letter, out string morseSequence))
                {
                    foreach (char morseChar in morseSequence)
                    {
                        float duration = (morseChar == '.') ? 0.2f : 1.5f;
                        ChangeSpriteColor(Color.white);
                        yield return new WaitForSeconds(duration);
                        ChangeSpriteColor(Color.black);
                        soundManager.PlaySound();
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield return new WaitForSeconds(3f);
                }
            }
            ChangeSpriteColor(Color.red);
            yield return new WaitForSeconds(5f);
        }
    }

    void ChangeSpriteColor(Color _color)
    {
        tempoCircle.color = _color;
    }

}
