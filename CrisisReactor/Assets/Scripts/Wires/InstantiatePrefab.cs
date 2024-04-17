using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabPatern;
    void Start()
    {
        Instantiate(prefabPatern[Random.Range(0,4)], Vector3.zero, Quaternion.identity);
    }
}
