using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class O_MoveOnde : MonoBehaviour
{
    [SerializeField] private List<Image> onde = new();
    [SerializeField] private GameObject ondes;
    [SerializeField] private float speed;
    private int indexMove;
    private float timer;
    private bool canMove;
    [SerializeField] private float second;

    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private MG_SoundManager soundManager;

    public static O_MoveOnde instance;
    public Vector2 mousePosition;

    private void Awake()
    {
        instance = this;
    }

    public void SetMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        indexMove = onde.Count - 1;
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }


    private void Move()
    {
        for (int i = 0; i < onde.Count; i++)
        {
            onde[i].transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        }
        timer += Time.fixedDeltaTime;
        if (timer >= 0.99f * ondes.transform.localScale.x)
        {
            timer = 0;
            if (indexMove >= onde.Count - 1)
            {
                onde[onde.Count - 1].transform.position = onde[0].transform.position;
                onde[onde.Count - 1].transform.position -= new Vector3(100 * ondes.transform.localScale.x, 0, 0);
                indexMove--;    
            }
            else
            {
                onde[indexMove].transform.position = onde[indexMove + 1].transform.position;
                onde[indexMove].transform.position -= new Vector3(100 * ondes.transform.localScale.x, 0, 0);
                if (indexMove <= 0)
                    indexMove = onde.Count - 1;
                else
                    indexMove--;
            }
        }
    }

    public void StartMove()
    {
        if (!canMove)
        {
            canMove = true;
            button1.SetActive(false);
            button2.SetActive(false);
            StartCoroutine(StopMoving(second));
        }
    }

    IEnumerator StopMoving(float second)
    {
        yield return new WaitForSeconds(second);
        if (Mathf.Abs(ondes.transform.localScale.x - O_PaternOnde.scaleX) < 0.75 && Mathf.Abs(ondes.transform.localScale.y - O_PaternOnde.scaleY) < 0.5)
        {
            canMove = false;
            PlayerPrefs.SetInt("MiniGame7", 1);
            soundManager.PlaySound(0);
            yield return new WaitForSeconds(1f);
            GameObject.Find("MultiSceneManager").GetComponent<MiniGameLobbyManager>().LeaveMiniGame();
            print("Win");
        }
        else
        {
            soundManager.PlaySound(1);
            canMove = false;
            button1.SetActive(true);
            button2.SetActive(true);
            LOB_Timer.instance.RemoveTimer(20);
        }
    }
}
