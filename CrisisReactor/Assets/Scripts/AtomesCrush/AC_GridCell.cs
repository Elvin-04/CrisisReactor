using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AC_GridCell : MonoBehaviour, IPointerEnterHandler
{
    public AC_GameManager gameManager;
    private Image image;
    private bool isSelected = false;
    private bool isEnabled = true;
    private AC_ENUM_Cell.CellType cellType;
    private MG_SoundManager soundManager;

    public void SetsoundManager(MG_SoundManager _soundManager)
    {
        soundManager = _soundManager;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        soundManager.PlaySound(1);
    }

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if(isEnabled && gameManager.GetCanPlay())
        {
            if(!isSelected)
            {
                if(gameManager.GetSelectedCell() == null)
                {
                    soundManager.PlaySound(3);
                    gameManager.OnCellClicked(this);
                    image.transform.localScale = new Vector2(0.75f, 0.75f);
                    isSelected = !isSelected;
                }
                else
                {
                    gameManager.UpgradeCell(this);
                }
            }
            else
            {
                gameManager.OnCellUnselected();
                image.transform.localScale = new Vector2(1f, 1f);
                isSelected = !isSelected;
            }
        }
    }

    public void InitCell(AC_ENUM_Cell.CellType _cellType)
    {
        image = GetComponent<Image>();
        cellType = _cellType;
        switch (cellType)
        {
            case AC_ENUM_Cell.CellType.Red:
                image.color = Color.red;
                break;
            case AC_ENUM_Cell.CellType.Green:
                image.color = Color.green;
                break;
            case AC_ENUM_Cell.CellType.Blue:
                image.color = Color.blue;
                break;
            case AC_ENUM_Cell.CellType.Yellow:
                image.color = Color.yellow;
                break;
            case AC_ENUM_Cell.CellType.White:
                image.color = Color.white;
                break;
            case AC_ENUM_Cell.CellType.Cyan:
                image.color = Color.cyan;
                break;
            case AC_ENUM_Cell.CellType.Magenta:
                image.color = Color.magenta;
                break;
            case AC_ENUM_Cell.CellType.Black:
                image.color = Color.black;
                break;
            default:
                break;
        }
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