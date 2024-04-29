using UnityEngine;
using UnityEngine.UI;

public class MM_MinValueAudio : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Update()
    {
        SetMinValueAudio();
    }

    public void SetMinValueAudio()
    {
        if (slider.value == -30)
        {
            slider.minValue = -80;
            slider.value = slider.minValue;
        }
        else if (slider.minValue == -80 && slider.value != -80)
        {
            slider.minValue = -30;
            slider.value = slider.minValue + 1;
        }
    }
}