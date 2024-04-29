using System.Collections;
using UnityEngine;

public class LOB_Error : MonoBehaviour
{
    [SerializeField] private GameObject objectError;
    [SerializeField] private float second;

    private void Start()
    {
        StartCoroutine(StartAnimation(second));
    }

    IEnumerator StartAnimation(float second)
    {
        objectError.SetActive(true);
        yield return new WaitForSeconds(second);
        objectError.SetActive(false);
        yield return new WaitForSeconds(second);
        StartCoroutine(StartAnimation(second));
    }
}
