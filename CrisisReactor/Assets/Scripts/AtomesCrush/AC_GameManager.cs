using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AC_GameManager : MonoBehaviour
{
    public List<AC_GridCell> grid = new List<AC_GridCell>();
    public GameObject P_cell;
    private AC_GridCell selectedCell;
    private int width;
    private int height;
    private GridLayoutGroup gridLayout;


        void Start()
        {   
            width = Random.Range(4, 12);
            height = Random.Range(4, 12);
            gridLayout = GetComponent<GridLayoutGroup>();
            CreateGrid();
        }

        private void CreateCell(AC_ENUM_Cell.CellType _cellType)
        {
            float cellWidth = gridLayout.cellSize.x;
            float cellHeight = gridLayout.cellSize.y;
            GameObject cellObject = Instantiate(P_cell);
            cellObject.transform.SetParent(transform);
            AC_GridCell castedCell = cellObject.GetComponent<AC_GridCell>();
            castedCell.gameManager = this;
            grid.Add(castedCell);
                     
            RectTransform rectTransform = cellObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(cellWidth, cellHeight);

            castedCell.InitCell(_cellType);
        }

    void ShuffleList(List<int> array)
    {
        System.Random systemRandom = new System.Random();
        int arrayCount = array.Count;

        while (arrayCount > 1)
        {
            arrayCount--;
            int nextValue = systemRandom.Next(arrayCount + 1);
            int value = array[nextValue];
            array[nextValue] = array[arrayCount];
            array[arrayCount] = value;
        }
    }

    private void CreateGrid()
    {
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayout.constraintCount = height;
        int randomizerControl = 0;
        List<int> randomizedIndex = new List<int>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                switch (randomizerControl)
                {
                    case 0:
                    randomizerControl = 1;
                    break;
                    case 1:
                    randomizerControl = 2;
                    break;
                    case 2:
                    randomizerControl = 0;
                    break;
                }
                randomizedIndex.Add(randomizerControl);
            }
        }
        
       ShuffleList(randomizedIndex);

        foreach (int _index in randomizedIndex)
        {
            CreateCell((AC_ENUM_Cell.CellType)_index);
        }
        
    }
    public void OnCellClicked(AC_GridCell _cellClicked)
    {
        selectedCell = _cellClicked;

        foreach (AC_GridCell cell in grid)
        {
            if(cell != null)
            {
                if(cell != _cellClicked.gameObject)
                {
                    cell.ResetCell();
                }
            }

        }
    }

    public void OnCellUnselected()
    {
       selectedCell = null;
    }

    public AC_GridCell GetSelectedCell()
    {
        return selectedCell;
    }

    public void UpgradeCell(AC_GridCell _cellFrom)
    {
        if(CheckForUpgrade(_cellFrom, selectedCell))
        {
            selectedCell.InitCell(UpgradeAtoms(selectedCell.GetCellType(), _cellFrom.GetCellType()));
            _cellFrom.InitCell(AC_ENUM_Cell.CellType.White);
            selectedCell.ResetCell();
            OnCellUnselected();
        }
    }

    private bool CheckForUpgrade(AC_GridCell _cellToUpgrade, AC_GridCell _cellToDestroy)
    {
        Debug.Log(Vector2.Distance(_cellToUpgrade.transform.position, _cellToDestroy.transform.position));
        if(Vector2.Distance(_cellToUpgrade.transform.position, _cellToDestroy.transform.position) < 156f && UpgradeAtoms(_cellToUpgrade.GetCellType(), selectedCell.GetCellType()) != AC_ENUM_Cell.CellType.Black)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    private Dictionary<(AC_ENUM_Cell.CellType, AC_ENUM_Cell.CellType), AC_ENUM_Cell.CellType> UpgradedCellColors = new Dictionary<(AC_ENUM_Cell.CellType, AC_ENUM_Cell.CellType), AC_ENUM_Cell.CellType>
    {
        {(AC_ENUM_Cell.CellType.White, AC_ENUM_Cell.CellType.Yellow), AC_ENUM_Cell.CellType.Cyan},
        {(AC_ENUM_Cell.CellType.Yellow, AC_ENUM_Cell.CellType.White), AC_ENUM_Cell.CellType.Cyan},

        {(AC_ENUM_Cell.CellType.Yellow, AC_ENUM_Cell.CellType.Cyan), AC_ENUM_Cell.CellType.Green},
        {(AC_ENUM_Cell.CellType.Cyan, AC_ENUM_Cell.CellType.Yellow), AC_ENUM_Cell.CellType.Green},

        {(AC_ENUM_Cell.CellType.Cyan, AC_ENUM_Cell.CellType.Green), AC_ENUM_Cell.CellType.Blue},
        {(AC_ENUM_Cell.CellType.Green, AC_ENUM_Cell.CellType.Cyan), AC_ENUM_Cell.CellType.Blue},

        {(AC_ENUM_Cell.CellType.Green, AC_ENUM_Cell.CellType.Blue), AC_ENUM_Cell.CellType.Magenta},
        {(AC_ENUM_Cell.CellType.Blue, AC_ENUM_Cell.CellType.Green), AC_ENUM_Cell.CellType.Magenta},

        {(AC_ENUM_Cell.CellType.Blue, AC_ENUM_Cell.CellType.Magenta), AC_ENUM_Cell.CellType.Red},
        {(AC_ENUM_Cell.CellType.Magenta, AC_ENUM_Cell.CellType.Blue), AC_ENUM_Cell.CellType.Red},

        {(AC_ENUM_Cell.CellType.Magenta, AC_ENUM_Cell.CellType.Red), AC_ENUM_Cell.CellType.Black},
        {(AC_ENUM_Cell.CellType.Red, AC_ENUM_Cell.CellType.Magenta), AC_ENUM_Cell.CellType.Black},
    };

    public AC_ENUM_Cell.CellType UpgradeAtoms(AC_ENUM_Cell.CellType color1, AC_ENUM_Cell.CellType color2)
    {
        if (UpgradedCellColors.TryGetValue((color1, color2), out AC_ENUM_Cell.CellType result))
        {
            return result;
        }
        else
        {
            return AC_ENUM_Cell.CellType.Black;
        }
    }
}