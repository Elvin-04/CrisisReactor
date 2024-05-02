using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AIO_CharacterController : MonoBehaviour
{
    [SerializeField] private List<Sprite> characterSprites = new List<Sprite>();
    private Vector3 worldPosition = Vector3.zero;
    private float moveSpeed = 5f;
    private float rotationSpeed = 500f;
    private InputAction action;
    private Vector3 direction = Vector3.zero;
    private float distance;

    void Start()
    {
        PlayerInput playerInput = GameObject.FindGameObjectWithTag("MultiSceneManager").GetComponent<PlayerInput>();
        action = playerInput.currentActionMap["MousePosition"];
        
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

    void Movement()
    {
            direction.Normalize();

            transform.position += direction * moveSpeed * Time.deltaTime;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x , transform.position.y, 0f);
    }

    //direction and movement computing
    void Update()
    {
        direction = worldPosition - transform.position;
        distance = direction.magnitude;

        if (distance > 0.5f)
        {
            Movement();
        }
    }
}
