using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AC_GridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public AC_GameManager gameManager;
    private Image image;
    private bool isSelected = false;
    private AC_ENUM_Cell.CellType cellType;
    private MG_SoundManager soundManager;
    private GameObject VFX = null;
    private Camera mainCamera;
    private Vector3 cachedPos = Vector3.zero;
    [SerializeField] private GameObject draggableAreaPrefab;
    private GameObject dragArea;
    public void SetsoundManager(MG_SoundManager _soundManager)
    {
        soundManager = _soundManager;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        soundManager.PlaySound(1);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(gameManager.HoveredCell == this)
        gameManager.HoveredCell = null;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if(gameManager.GetIsPlaying() && pointerEventData.button != PointerEventData.InputButton.Right)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(pointerEventData.position.x, pointerEventData.position.y, 0));
            Vector3 targetPos = new Vector3(Mathf.Clamp(mousePosition.x, cachedPos.x - 1.5f, cachedPos.x + 1.5f), Mathf.Clamp(mousePosition.y, cachedPos.y - 1.5f, cachedPos.y + 1.5f),0);
            transform.position = targetPos;
        }
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if(gameManager.GetIsPlaying())
        {
            if(gameManager.GetSelectedCell() == null && pointerEventData.button != PointerEventData.InputButton.Right)
                {
                    dragArea = Instantiate(draggableAreaPrefab, transform.position, transform.rotation);
                   // dragArea.transform.SetParent(transform);
                   // gameManager.GetDraggableArea(this).SetActive(true);
                    cachedPos = transform.position;
                    soundManager.PlaySound(3);
                    gameManager.OnCellClicked(this);
                    image.transform.localScale = new Vector2(0.75f, 0.75f);
                    isSelected = !isSelected;
                }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(gameManager.GetIsPlaying() && eventData.button != PointerEventData.InputButton.Right)
        {
            Destroy(dragArea);
            //gameManager.GetDraggableArea(this).SetActive(false);
            gameManager.HoveredCell = gameManager.GetNearestCell(this);
            transform.position = cachedPos;

            if(gameManager.HoveredCell != null)
            {
                gameManager.UpgradeCell(gameManager.HoveredCell);
            }

            image.transform.localScale = new Vector2(1f, 1f);
            gameManager.OnCellUnselected();
            gameManager.HoveredCell = null;
        }
    }

    void Start()
    {
        mainCamera = FindAnyObjectByType<Camera>();
        ResetCell();
    }

    public void InitCell(AC_ENUM_Cell.CellType _cellType)
    {
        image = GetComponent<Image>();
        cellType = _cellType;
        if(VFX)
        {
            Destroy(VFX);
        }
        VFX = gameManager.SpawnVFXAndAttach(gameObject, (int)cellType);
    }

    public void ResetCell()
    {
        image.transform.localScale = new Vector2(1f, 1f);
        isSelected = false;
    }

    public AC_ENUM_Cell.CellType GetCellType()
    {
        return cellType;
    }
}