using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AC_GameManager : MonoBehaviour
{
    private List<AC_GridCell> grid = new List<AC_GridCell>();
    public GameObject P_cell;
    private AC_GridCell selectedCell;
    private int width;
    private int height;
    private GridLayoutGroup gridLayout;
    private AC_ENUM_Cell.CellType waitedAtom;
    [SerializeField] private GameObject waitedAtomLayout;
    [SerializeField] private MG_SoundManager soundManager;
    [SerializeField] private GameObject[] atomsVFX;
    private bool isPlaying = true;
    private AC_GridCell hoveredCell;

    public AC_GridCell GetNearestCell(AC_GridCell currentCell)
    {
        AC_GridCell nearestCell = null;
        float nearestDistance = float.MaxValue;
        foreach (AC_GridCell cell in grid)
        {
            if(cell != this)
            {
                float distance = Vector2.Distance(currentCell.transform.position, cell.transform.position);
                if (distance < nearestDistance && cell != currentCell)
                {
                    nearestDistance = distance;
                    nearestCell = cell;
                }
            }
        }

        if(nearestDistance > 0.5f)
        {
            return null;   
        }
        else
        {
            return nearestCell;
        }
    }
        public AC_GridCell HoveredCell
        {
            get {return hoveredCell;}
            set {hoveredCell = value;}
        }
        public GameObject[] GetAtomsVFX()
        {
            return atomsVFX;
        }
        public MG_SoundManager GetSoundManager()
        {
            return soundManager;
        }
        public bool GetIsPlaying()
        {
            return isPlaying;
        }

        public GameObject SpawnVFXAndAttach(GameObject _gameObjectParent, int _cellType)
        {
            GameObject VFX = Instantiate(atomsVFX[_cellType]);
            VFX.transform.parent = _gameObjectParent.transform;
            VFX.transform.localScale = new Vector3(35f, 35f, 35f);
            VFX.transform.localPosition = Vector3.zero;

            return VFX;
        }
        void Start()
        {   
            width = Random.Range(5, 9);
            height = Random.Range(5, 7);
            gridLayout = GetComponent<GridLayoutGroup>();

            int randomizedWaitedAtom = Random.Range(0, 3);
            
            switch (randomizedWaitedAtom)
                        {
                            case 0:
                            waitedAtom = AC_ENUM_Cell.CellType.Blue;
                            break;
                            case 1:
                            waitedAtom = AC_ENUM_Cell.CellType.Magenta;
                            break;
                            case 2:
                            waitedAtom = AC_ENUM_Cell.CellType.Red;
                            break;
                        }

            SpawnVFXAndAttach(waitedAtomLayout, (int)waitedAtom);
            CreateGrid();
        }

        private void CreateCell(AC_ENUM_Cell.CellType _cellType)
        {
            float cellWidth = gridLayout.cellSize.x;
            float cellHeight = gridLayout.cellSize.y;
            GameObject cellObject = Instantiate(P_cell);
            cellObject.transform.SetParent(transform);
            AC_GridCell castedCell = cellObject.GetComponent<AC_GridCell>();
            castedCell.SetsoundManager(soundManager);
            castedCell.gameManager = this;
            grid.Add(castedCell);
                     
            RectTransform rectTransform = cellObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(cellWidth, cellHeight);

            castedCell.InitCell(_cellType);
           // GameObject dragArea = Instantiate(draggableAreaPrefab, cellObject.transform.position, Quaternion.identity);
          //  dragAreaByCell.Add(castedCell, dragArea);
        }

    //randomize atoms to have the same numbers at each games but not to the same positions

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

    //init grid and cells with random control
    private void CreateGrid()
    {
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayout.constraintCount = height;
        int randomizerControl = 0;
        int randomizerIndex = 0;
        List<int> randomizedIndex = new List<int>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                switch (randomizerIndex)
                {
                    case 0:
                    randomizerIndex = 1;
                    randomizerControl = 0;
                    break;
                    case 1:
                    randomizerIndex = 2;
                    randomizerControl = 0;
                    break;
                    case 2:
                    randomizerIndex = 3;
                    randomizerControl = 1;
                    break;
                    case 3:
                    randomizerIndex = 4;
                    randomizerControl = 2;
                    break;
                    case 4:
                    randomizerIndex = 0;
                    randomizerControl = 2;
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

    private void OnVictory()
    {
        PlayerPrefs.SetInt("MiniGame4", 1);
        SceneManager.LoadScene("Lobby");
        Debug.Log("winned");
    }

    public void UpgradeCell(AC_GridCell _cellFrom)
    {
        if(CheckForUpgrade(_cellFrom, selectedCell))
        {
            soundManager.PlaySound(5);
            _cellFrom.InitCell(UpgradeAtoms(selectedCell.GetCellType(), _cellFrom.GetCellType()));
            selectedCell.InitCell(AC_ENUM_Cell.CellType.White);

            if(_cellFrom.GetCellType() == waitedAtom)
            {
                isPlaying = false;
                soundManager.PlaySound(7);
                Invoke("OnVictory", 2.25f);
            }
            _cellFrom.ResetCell();
            OnCellUnselected();
        }
        else
        {
            soundManager.PlaySound(6);
        }
    }

    private bool CheckForUpgrade(AC_GridCell _cellToUpgrade, AC_GridCell _cellToDestroy)
    {
        if(_cellToUpgrade !=null)
        {
                if(Vector2.Distance(_cellToUpgrade.transform.position, _cellToDestroy.transform.position) < 2.1f && UpgradeAtoms(_cellToUpgrade.GetCellType(), selectedCell.GetCellType()) != AC_ENUM_Cell.CellType.Black)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        else
        {
            return false;
        }
    }



    private Dictionary<(AC_ENUM_Cell.CellType, AC_ENUM_Cell.CellType), AC_ENUM_Cell.CellType> UpgradedCellColors = new Dictionary<(AC_ENUM_Cell.CellType, AC_ENUM_Cell.CellType), AC_ENUM_Cell.CellType>
    {
        {(AC_ENUM_Cell.CellType.White, AC_ENUM_Cell.CellType.White), AC_ENUM_Cell.CellType.Yellow},

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