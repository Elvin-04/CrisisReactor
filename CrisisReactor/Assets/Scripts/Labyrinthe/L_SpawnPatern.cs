using System.Collections.Generic;
using UnityEngine;

public class L_SpawnPatern : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabPatern = new();

    private void Start()
    {
        Instantiate(prefabPatern[Random.Range(0, prefabPatern.Count)], Vector3.zero, Quaternion.identity);
    }
}
