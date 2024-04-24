using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstantiatePrefab : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabPatern;
    public Vector2 mousePosition;

    void Start()
    {
        Instantiate(prefabPatern[Random.Range(0,prefabPatern.Count)], Vector3.zero, Quaternion.identity);
    }


    public void GetMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
}
