using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AC_GridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public AC_GameManager gameManager;
    private Image image;
    private bool isSelected = false;
    private bool isEnabled = true;
    private AC_ENUM_Cell.CellType cellType;
    private MG_SoundManager soundManager;
    private GameObject VFX = null;


    public void SetsoundManager(MG_SoundManager _soundManager)
    {
        soundManager = _soundManager;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(gameManager.GetIsPlaying())
        {
            soundManager.PlaySound(1);
            if(gameManager.GetSelectedCell() != null)
            {
                if(gameManager.GetSelectedCell() != this)
                gameManager.HoveredCell = this;
            }
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(gameManager.HoveredCell == this)
        gameManager.HoveredCell = null;
    }


    //need to keep it for D&D working 
    public void OnDrag(PointerEventData pointerEventData)
    {}

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if(gameManager.GetIsPlaying())
        {
         if(gameManager.GetSelectedCell() == null)
            {
                soundManager.PlaySound(3);
                gameManager.OnCellClicked(this);
                image.transform.localScale = new Vector2(0.75f, 0.75f);
                isSelected = !isSelected;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(gameManager.HoveredCell != null)
        {
            gameManager.UpgradeCell(gameManager.HoveredCell);
        }
        image.transform.localScale = new Vector2(1f, 1f);
        gameManager.OnCellUnselected();
        gameManager.HoveredCell = null;
    }

    void Start()
    {
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


    public void ToggleEnabled(bool _enabled)
    {
        isEnabled = _enabled;
        image.transform.localScale = new Vector2(0f, 0f);
    }

    public AC_ENUM_Cell.CellType GetCellType()
    {
        return cellType;
    }
}