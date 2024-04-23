using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LOB_Timer : MonoBehaviour
{
    [Header("Timer")]
    public float totalTime = 300;
    public float currentTime = 300;
    [SerializeField] private TextMeshProUGUI timerText;
    [HideInInspector] public int value = 0;
    int currentTimeInt;
    [HideInInspector] public bool endGame;

    public static LOB_Timer instance;

    [Header("DigiCode")]
    public GameObject digicodePrefab;
    [SerializeField] private int digicodeSpawnCount;
    private List<float> timeCodeDigicode = new List<float>();
    private int currentIndex;
    public List<string> timerScene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


    }


    //Create DontDestroyOnLoad scene 
    private void Start()
    {
        GameObject[] multiScene = GameObject.FindGameObjectsWithTag("MultiSceneManager");

        if(multiScene.Length <= 1)
        {
            DontDestroyOnLoad(gameObject);
            value = 1;

            for (int i = 0; i < digicodeSpawnCount; i++)
            {
                timeCodeDigicode.Add((int)Random.Range(30, totalTime - 15f));
                Debug.Log(timeCodeDigicode[i]);
            }

            currentIndex = 0;
        }
        else
        {
            
            foreach (GameObject go in multiScene)
            {
                if (go.GetComponent<LOB_Timer>().value == 0)
                {
                    Destroy(go);
                }
                
            }
        }
    }

    

    private void FixedUpdate()
    {
        //Update the timer variable
        if (currentTime > 0)
            currentTime -= Time.unscaledDeltaTime;
        //Update and set the time text on screen
        if (timerText != null)
            timerText.text = ((int)currentTime / 60).ToString("00") + ":" + ((int)currentTime % 60).ToString("00");
        else
            if(GameObject.Find("TimerText") != null)
                timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();

        //Play a tick sound each second
        if(currentTimeInt != (int)currentTime)
        {
            currentTimeInt = (int)currentTime;
            GetComponent<AudioSource>().Play();
        }


        if(timeCodeDigicode.Contains((int)currentTime) && timerScene.Contains(SceneManager.GetActiveScene().name) && GameObject.FindGameObjectWithTag("Timer") == null)
        {
            if (currentIndex + 1 < digicodeSpawnCount)
                currentIndex++;
            else
                currentIndex = 0;
            Instantiate(digicodePrefab);
            timeCodeDigicode.Remove((int)currentTime);
        }

        //Loose condition
        if(currentTime <= 0 && !endGame)
        {
            currentTime = -1;
            endGame = true;
            SceneManager.LoadScene("UIDefeatVictory");
            //Play anim or sounds here
        }
    }

    public void RemoveTimer(float second)
    {
        currentTime -= second;
        if (currentTime < 0)
            currentTime = 0;
    }

}
