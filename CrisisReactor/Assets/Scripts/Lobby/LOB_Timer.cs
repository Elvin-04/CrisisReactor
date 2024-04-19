using TMPro;
using UnityEngine;

public class LOB_Timer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float totalTime = 300;
    [SerializeField] private float currentTime = 300;
    [SerializeField] private TextMeshProUGUI timerText;
    [HideInInspector] public int value = 0;
    int currentTimeInt;


    //Create DontDestroyOnLoad scene 
    private void Start()
    {
        GameObject[] multiScene = GameObject.FindGameObjectsWithTag("MultiSceneManager");

        if(multiScene.Length <= 1)
        {
            DontDestroyOnLoad(gameObject);
            value = 1; 
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

        //Loose condition
        if(currentTime <= 0)
        {
            Debug.Log("Loose");
            //Play anim or sounds here
        }

    }

}
