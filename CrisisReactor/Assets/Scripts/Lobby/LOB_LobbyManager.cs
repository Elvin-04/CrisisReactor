using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    private void Awake()
    {
        mainCamera = Camera.main;

        initCameraPosition = mainCamera.transform.position;
        initCameraSize = mainCamera.orthographicSize;

        canvas = GameObject.Find("Canvas");
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("Zoom") == 1)
            CancelZoom();
    }


    private void FixedUpdate()
    {
        //Do the camera zoom
        if(zoom || cancelZoom)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, cameraSizeDestination, cameraZoomSpeed * Time.deltaTime);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPositionDestination, cameraZoomSpeed * Time.deltaTime);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10f);

            if(zoom && mainCamera.orthographicSize - cameraSizeDestination <= 0.05f
                && Vector2.Distance(mainCamera.transform.position, cameraPositionDestination) <= 0.05f)
            {
                zoom = false;
                PlayerPrefs.SetInt("Zoom", 1);
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

    //Set the values
    public void Zoom(LOB_MiniGame game)
    {
        cameraPositionDestination = game.position;
        cameraSizeDestination = game.size;
        minigameSelected = game;

        zoom = true;
        canvas.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Zoom", 0);
    }

    //Set the values
    public void CancelZoom()
    {
        mainCamera.transform.position = new Vector3(PlayerPrefs.GetFloat("CamPosX"), PlayerPrefs.GetFloat("CamPosY"), -10);
        mainCamera.orthographicSize = PlayerPrefs.GetFloat("CamSize");

        cameraPositionDestination = initCameraPosition;
        cameraSizeDestination = initCameraSize;
        minigameSelected = null;

        cancelZoom = true;
    }


}
