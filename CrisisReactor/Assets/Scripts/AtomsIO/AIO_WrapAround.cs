using UnityEngine;

public class AIO_WrapAround : MonoBehaviour
{
    void Update()
    {
        WrapAroundScreen();
    }

    //mirror teleport on screen borders
    void WrapAroundScreen()
    {
        Camera mainCamera = Camera.main;
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (screenPosition.x < 0)
        {
            screenPosition.x = 1;
            InverseRotation();
        }
        else if (screenPosition.x > 1)
        {
            screenPosition.x = 0;
            InverseRotation();
        }

        if (screenPosition.y < 0)
        {
            screenPosition.y = 1;
            InverseRotation();
        }
        else if (screenPosition.y > 1)
        {
            screenPosition.y = 0;
            InverseRotation();
        }

        transform.position = mainCamera.ViewportToWorldPoint(screenPosition);
    }

    void InverseRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
    }
}
