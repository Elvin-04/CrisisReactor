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
        
        if (screenPosition.x < 0.025f)
        {
            screenPosition.x = 0.975f;
            InverseRotation();
        }
        else if (screenPosition.x > 0.975f)
        {
            screenPosition.x = 0.025f;
            InverseRotation();
        }

        if (screenPosition.y < 0.025f)
        {
            screenPosition.y = 0.975f;
            InverseRotation();
        }
        else if (screenPosition.y > 0.975f)
        {
            screenPosition.y = 0.025f;
            InverseRotation();
        }

        transform.position = mainCamera.ViewportToWorldPoint(screenPosition);
    }

    void InverseRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
    }
}
