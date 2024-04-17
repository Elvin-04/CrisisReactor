using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LOB_Timer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float totalTime = 300;
    [SerializeField] private TextMeshProUGUI timerText;
    private float currentTime;

    private void Start()
    {
        currentTime = totalTime;
    }

    private void FixedUpdate()
    {
        currentTime -= Time.deltaTime;
        timerText.text = (int)currentTime / 60 + ":" + (int)currentTime % 60;
    }

}
