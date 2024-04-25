using UnityEngine;
using UnityEngine.EventSystems;

public class O_ChangeHeightOnde : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject ondes;
    [SerializeField] private float maxScaleY;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");   //This can be used
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        //Vector3 dir = Input.mousePosition - gameObject.transform.position;
        Vector3 mousePos = O_MoveOnde.instance.mousePosition;
        Vector3 dir = mousePos - gameObject.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, gameObject.transform.rotation.y, 1));
        if (gameObject.transform.eulerAngles.z > 180)
        {
            ondes.transform.localScale = new Vector3(ondes.transform.localScale.x, 0.5f + ((360 - gameObject.transform.eulerAngles.z) / (180 / (maxScaleY - 0.5f))), 1);
        }
        else
        {
            ondes.transform.localScale = new Vector3(ondes.transform.localScale.x, 0.5f + (Mathf.Abs(gameObject.transform.eulerAngles.z) / (180 / (maxScaleY - 0.5f))), 1);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
    }
}
