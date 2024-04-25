using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AIO_CharacterController : MonoBehaviour
{
    [SerializeField] private List<Sprite> characterSprites = new List<Sprite>();
    private Vector3 worldPosition = Vector3.zero;
    private float moveSpeed = 5f;
    private float rotationSpeed = 200f;
    private InputAction action;

    void Start()
    {
        PlayerInput playerInput = GameObject.FindGameObjectWithTag("MultiSceneManager").GetComponent<PlayerInput>();
        action = playerInput.currentActionMap["PointerMove"];
        
        action.performed += OnMove;
    }

    public void SetSpriteByPattern(int patternIndex)
    {
        GetComponent<SpriteRenderer>().sprite = characterSprites[patternIndex];
    }

    void OnDestroy()
    {
        if (action != null)
        {
            action.performed -= OnMove;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = context.ReadValue<Vector2>();
        Camera mainCamera = Camera.main;
        worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
        
        worldPosition.z = transform.position.z;
    }

    void Update()
    {
        Vector3 direction = worldPosition - transform.position;
        float distance = direction.magnitude;

        if (distance > 0.01f)
        {
            direction.Normalize();

            transform.position += direction * moveSpeed * Time.deltaTime;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x , transform.position.y, 0f);
        }
    }
}
