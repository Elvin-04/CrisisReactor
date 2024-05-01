using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MG_GameManager : MonoBehaviour
{

    [SerializeField] private GameObject tempoCircle;
    [SerializeField] private GameObject tempoCircleGray;
    [SerializeField] private GameObject soundIcon;
    [SerializeField] private MG_SoundManager soundManager;
    private Animator animator;
    private string waitedWord;
    [SerializeField] private Button buttonStart;

    //morseCode dictionnary is used to translate from char to morse code
    private Dictionary<char, string> morseCode = new Dictionary<char, string>()
    {	{'A', ".-"},{'B', "-..."},{'C', "-.-."},{'D', "-.."},{'E', "."},{'F', "..-."},{'G', "--."},{'H', "...."},{'I', ".."},
        {'J', ".---"},{'K', "-.-"},{'L', ".-.."},{'M', "--"},{'N', "-."},{'O', "---"},{'P', ".--."},{'Q', "--.-"},{'R', ".-."},
        {'S', "..."},{'T', "-"},{'U', "..-"},{'V', "...-"},{'W', ".--"},{'X', "-..-"},{'Y', "-.--"},{'Z', "--.."},{'0', "-----"},
        {'1', ".----"},{'2', "..---"},{'3', "...--"},{'4', "....-"},{'5', "....."},{'6', "-...."},{'7', "--..."},{'8', "---.."},
        {'9', "----."}
    };

    private void Start()
    {
        RandomWord();
        tempoCircleGray.gameObject.SetActive(true);
    }

    private void RandomWord()
    {
        waitedWord = "";
        for (int i = 0; i < 3; i++)
        {
            waitedWord = waitedWord + morseCode.ElementAt(UnityEngine.Random.Range(0, morseCode.Count)).Key;
        }
        Debug.Log("mot attendu = " + waitedWord);
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
        else if (!string.Equals(_commitedWord, ""))
        {
            Debug.Log("Perdu");
            LOB_Timer.instance.RemoveTimer(20);
            soundManager.PlaySound(4);
        }
    }


    //execute morse sequence with sound and sprite swapping
    IEnumerator ExecuteMorse()
    {
        foreach (char _letter in waitedWord)
        {
            if (morseCode.TryGetValue(_letter, out string morseSequence))
            {
                foreach (char morseChar in morseSequence)
                {
                    float duration = (morseChar == '.') ? 0.2f : 1.5f;
                    tempoCircle.gameObject.SetActive(true);
                    soundIcon.gameObject.SetActive(true);
                    if (duration == 0.2f)
                    {
                        soundManager.PlaySound(5);
                    }
                    else
                    {
                        soundManager.PlaySound(6);
                    }
                    yield return new WaitForSeconds(duration);
                    soundIcon.gameObject.SetActive(false);
                    tempoCircle.gameObject.SetActive(false);
                        
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(3f);
            }
        }
        tempoCircleGray.gameObject.SetActive(true);
        buttonStart.interactable = true;
    }

    public void StartMorse()
    {
        tempoCircleGray.gameObject.SetActive(false);
        PlayAnimButton();
        buttonStart.interactable = false;
    }
}
