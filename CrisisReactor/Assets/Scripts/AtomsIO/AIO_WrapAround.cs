using UnityEngine;

public class AIO_WrapAround : MonoBehaviour
{
    void Update()
    {
        WrapAroundScreen();
    }

    void WrapAroundScreen()
    {
        Camera mainCamera = Camera.main;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0)
        {
            viewportPosition.x = 1;
            InverseRotation();
        }
        else if (viewportPosition.x > 1)
        {
            viewportPosition.x = 0;
            InverseRotation();
        }

        if (viewportPosition.y < 0)
        {
            viewportPosition.y = 1;
            InverseRotation();
        }
        else if (viewportPosition.y > 1)
        {
            viewportPosition.y = 0;
            InverseRotation();
        }
        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);
    }

    void InverseRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
    }
}
