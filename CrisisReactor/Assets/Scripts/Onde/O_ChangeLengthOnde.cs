using UnityEngine;
using UnityEngine.EventSystems;

public class O_ChangeLengthOnde : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject ondes;
    [SerializeField] private float maxScaleX;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");   //This can be used
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        Vector3 dir = Input.mousePosition - gameObject.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (gameObject.transform.eulerAngles.z < 360 && gameObject.transform.eulerAngles.z > 270)
        {
            gameObject.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, gameObject.transform.rotation.y, 1));
            ondes.transform.localScale = new Vector3(0.75f + (Mathf.Abs(gameObject.transform.eulerAngles.z) / (180 / (maxScaleX - 0.75f)) / 2), ondes.transform.localScale.y, 1);
            if (gameObject.transform.eulerAngles.z < 180)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 359);
                ondes.transform.localScale = new Vector3(0.75f + (Mathf.Abs(gameObject.transform.eulerAngles.z) / (180 / (maxScaleX - 0.75f)) / 2), ondes.transform.localScale.y, 1);
            }
        }
        else if (gameObject.transform.eulerAngles.z < 90)
        {
            gameObject.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, gameObject.transform.rotation.y, 1));
            ondes.transform.localScale = new Vector3(0.75f + (Mathf.Abs(gameObject.transform.eulerAngles.z) / (180 / (maxScaleX - 0.75f)) / 2), ondes.transform.localScale.y, 1);
            if (gameObject.transform.eulerAngles.z > 180)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                ondes.transform.localScale = new Vector3(0.75f + (Mathf.Abs(gameObject.transform.eulerAngles.z) / (180 / (maxScaleX - 0.75f)) / 2), ondes.transform.localScale.y, 1);
            }
        }
        else
        {
            gameObject.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, gameObject.transform.rotation.y, 1));
            ondes.transform.localScale = new Vector3(0.75f + (Mathf.Abs(gameObject.transform.eulerAngles.z) / (180 / (maxScaleX - 0.75f)) / 2), ondes.transform.localScale.y, 1);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
    }
}
