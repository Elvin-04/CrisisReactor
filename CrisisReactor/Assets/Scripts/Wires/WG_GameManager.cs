using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WG_GameManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 initPos;
    private float sizeXWire;
    [SerializeField] private GameObject endPos;
    private float distEndPos;
    [HideInInspector] public bool isConnect;
    private MG_SoundManager soundManager;
    [SerializeField] private ParticleSystem particleSpark;
    [SerializeField] private Camera mainCamera;

    private void Start()
    { 
        sizeXWire = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        initPos = mainCamera.WorldToScreenPoint(transform.position) - new Vector3(sizeXWire / 2, 0, 0);
        print(initPos);
        distEndPos = endPos.GetComponent<RectTransform>().sizeDelta.x;
        soundManager = GameObject.FindGameObjectWithTag("soundManager").GetComponent<MG_SoundManager>();
        StartCoroutine(StartParticle());
        if (Random.Range(0,3) == 0)
        {
            particleSpark.Play();
        }
    }

    /// 
    // drag and drop operations
    public void OnBeginDrag(PointerEventData eventData)
    {
        soundManager.PlaySound(0);
        Debug.Log("BeginDrag");   //This can be used
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        SetPosImage(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        if (Vector3.Distance(Input.mousePosition + new Vector3(0, 0, 100), mainCamera.WorldToScreenPoint(endPos.transform.position)) > distEndPos)
        {
            ResetPos();
            soundManager.PlaySound(2);
            Debug.Log("Dead");
            isConnect = false;
            LOB_Timer.instance.RemoveTimer(20);
        }
        else
        {
            SetPosImage(mainCamera.WorldToScreenPoint(endPos.transform.position));
            soundManager.PlaySound(1);
            Debug.Log("Success");
            StopAllCoroutines();
            particleSpark.Stop();
            isConnect = true;
            if (CheckWinWire.Instance.CheckWin())
            {
                Debug.Log("Win");
                PlayerPrefs.SetInt("MiniGame3", 1);
                SceneManager.LoadScene("Lobby");
            }
        }
    }

    ////
    private void ResetPos()
    {
        transform.position = mainCamera.ScreenToWorldPoint(initPos + new Vector3(sizeXWire / 2, 0, 0));
        transform.localScale = Vector3.one;
        transform.eulerAngles = Vector3.zero;
    }

    IEnumerator StartParticle()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));
        if (Random.Range(0,3) == 0)
            soundManager.PlaySound(3);
        particleSpark.Play();
        StartCoroutine(StartParticle());
    }

    private void SetPosImage(Vector3 pos)
    {
        //Set position and scale
        gameObject.transform.localScale = new Vector3(Vector3.Distance(pos + new Vector3(0,0,100), initPos) / sizeXWire, 1, 1);
        Vector3 newPos = (pos - initPos) / 2 + initPos;
        transform.position = mainCamera.ScreenToWorldPoint(newPos);

        //Rotate
        Vector3 dir = pos - mainCamera.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, gameObject.transform.rotation.y, 1));
    }
}
