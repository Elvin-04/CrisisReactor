using UnityEngine;
using UnityEngine.EventSystems;

public class WG_GameManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 initPos;
    [SerializeField] private GameObject endPos;
    private float distEndPos;

    private void Start()
    {
        initPos = transform.position;
        distEndPos = endPos.GetComponent<RectTransform>().sizeDelta.x / 2;
        CheckWinWire.maxWire++;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");   //A voir si cela peut servir
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        SetPosImage(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        if (Vector3.Distance(Input.mousePosition, endPos.transform.position) > distEndPos)
        {
            ResetPos();
            Debug.Log("Dead");
        }
        else
        {
            SetPosImage(endPos.transform.position);
            Debug.Log("Success");
            CheckWinWire.nbWireWonnect++;
            if (CheckWinWire.CheckWin())
            {
                Debug.Log("Win");
            }
        }
    }

    private void ResetPos()
    {
        transform.position = initPos;
        transform.localScale = Vector3.one;
        transform.eulerAngles = Vector3.zero;
    }

    private void SetPosImage(Vector3 pos)
    {
        //Set position
        gameObject.transform.localScale = new Vector3(Vector3.Distance(pos, initPos) / gameObject.GetComponent<RectTransform>().sizeDelta.x, 1, 1);
        Vector3 newPos = (pos - initPos) / 2 + initPos;
        transform.position = newPos;

        //Rotate
        Vector3 dir = pos - gameObject.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, gameObject.transform.rotation.y, 1));
    }
}
