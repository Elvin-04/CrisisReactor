using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LOB_Timer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float totalTime = 300;
    [SerializeField] private float currentTime = 300;
    [SerializeField] private TextMeshProUGUI timerText;
    [HideInInspector] public int value = 0;

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
        currentTime -= Time.unscaledDeltaTime;
        if (timerText != null)
            timerText.text = ((int)currentTime / 60).ToString("00") + ":" + ((int)currentTime % 60).ToString("00");
        else
            if(GameObject.Find("TimerText") != null)
                timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
    }

}
