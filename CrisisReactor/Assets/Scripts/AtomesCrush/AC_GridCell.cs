using UnityEngine;
using UnityEngine.UI;

public class AC_GridCell : MonoBehaviour
{
    public AC_GameManager gameManager;
    private Image image;
    private bool isSelected = false;
    private bool isEnabled = true;
    private AC_ENUM_Cell.CellType cellType;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
       
        if(isEnabled)
        {
            if(!isSelected)
            {
                if(gameManager.GetSelectedCell() == null)
                {
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