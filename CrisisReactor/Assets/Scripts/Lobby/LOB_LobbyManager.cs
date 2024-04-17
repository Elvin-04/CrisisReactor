using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LOB_LobbyManager : MonoBehaviour
{
    private Camera mainCamera;

    private Vector2 initCameraPosition;
    private float initCameraSize;
    [SerializeField] private float cameraZoomSpeed;


    private Vector2 cameraPositionDestination;
    private float cameraSizeDestination;
    private LOB_MiniGame minigameSelected;

    [SerializeField] private GameObject canvas;

    private bool zoom = false;
    private bool cancelZoom = false;

    [Header("Camera Shake")]
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    private void Awake()
    {
        mainCamera = Camera.main;

        initCameraPosition = mainCamera.transform.position;
        initCameraSize = mainCamera.orthographicSize;

        canvas = GameObject.Find("Canvas");
    }

    private void Start()
    {
        CancelZoom();
    }


    private void FixedUpdate()
    {

        if(zoom || cancelZoom)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, cameraSizeDestination, cameraZoomSpeed * Time.deltaTime);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPositionDestination, cameraZoomSpeed * Time.deltaTime);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10f);

            if(zoom && mainCamera.orthographicSize - cameraSizeDestination <= 0.05f
                && Vector2.Distance(mainCamera.transform.position, cameraPositionDestination) <= 0.05f)
            {
                zoom = false;
                PlayerPrefs.SetFloat("CamPosX", cameraPositionDestination.x);
                PlayerPrefs.SetFloat("CamPosY", cameraPositionDestination.y);
                PlayerPrefs.SetFloat("CamSize", cameraSizeDestination);
                SceneManager.LoadScene(minigameSelected.miniGameScene);
            }

            if(cancelZoom && mainCamera.orthographicSize - cameraSizeDestination >= -0.05f
                && Vector2.Distance(mainCamera.transform.position, cameraPositionDestination) <= 0.05f)
            {
                cancelZoom = false;
                if(canvas != null)
                    canvas.SetActive(true);
            }
        }
    }

    public void Zoom(LOB_MiniGame game)
    {
        cameraPositionDestination = game.position;
        cameraSizeDestination = game.size;
        minigameSelected = game;

        zoom = true;
        canvas.SetActive(false);
    }

    public void CancelZoom()
    {
        mainCamera.transform.position = new Vector3(PlayerPrefs.GetFloat("CamPosX"), PlayerPrefs.GetFloat("CamPosY"), -10);
        mainCamera.orthographicSize = PlayerPrefs.GetFloat("CamSize");

        cameraPositionDestination = initCameraPosition;
        cameraSizeDestination = initCameraSize;
        minigameSelected = null;

        cancelZoom = true;
    }

    //IEnumerator CameraShake()
    //{
    //    Vector3 startPosition = mainCamera.transform.localPosition;
    //    float elapsedTime = 0f;

    //    while(elapsedTime < duration)
    //    {
    //        float x = Random.Range(-1f, 1f) * magnitude;
    //        float y = Random.Range(-1f, 1f) * magnitude;

    //        Vector3 pos = new Vector3(x, y, startPosition.z);
    //        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, pos, Time.deltaTime * 0.1f );

    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    while(Vector3.Distance(mainCamera.transform.position, startPosition) > 0.01f)
    //    {
    //        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, startPosition, Time.deltaTime * 2f);
    //        yield return null;
    //    }

    //    mainCamera.transform.position = startPosition;
    //}


}
