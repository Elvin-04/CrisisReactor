using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AC_GameManager : MonoBehaviour
{
    public List<AC_GridCell> grid = new List<AC_GridCell>();
    public GameObject P_cell;
    private AC_GridCell selectedCell;
    private int width;
    private int height;
    private GridLayoutGroup gridLayout;
    private AC_ENUM_Cell.CellType waitedAtom;
    [SerializeField] private MG_SoundManager soundManager;
    [SerializeField] private Image waitedAtomImage;
    [SerializeField] private TextMeshProUGUI waitedAtomText;
    [SerializeField] private Sprite[] atomsSprites;
    private bool canPlay = true;

        public MG_SoundManager GetSoundManager()
        {
            return soundManager;
        }
        void Start()
        {   
            width = Random.Range(5, 8);
            height = Random.Range(5, 9);
            gridLayout = GetComponent<GridLayoutGroup>();

            int randomizedWaitedAtom = Random.Range(0, 3);


            switch (randomizedWaitedAtom)
                        {
                            case 0:
                            waitedAtom = AC_ENUM_Cell.CellType.Blue;
                            waitedAtomText.text = "Résultat attendu : BLUE";
                            waitedAtomImage.sprite = atomsSprites[4];
                            break;
                            case 1:
                            waitedAtom = AC_ENUM_Cell.CellType.Magenta;
                            waitedAtomText.text = "Résultat attendu : MAGENTA";
                            waitedAtomImage.sprite = atomsSprites[5];
                            break;
                            case 2:
                            waitedAtom = AC_ENUM_Cell.CellType.Red;
                            waitedAtomText.text = "Résultat attendu : RED";
                            waitedAtomImage.sprite = atomsSprites[6];
                            break;
                        }
           



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
                    randomizerControl = 1;
                    break;
                    case 2:
                    randomizerIndex = 3;
                    randomizerControl = 2;
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
        if(canPlay)
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
    }

    public void OnCellUnselected()
    {
       selectedCell = null;
    }

    public AC_GridCell GetSelectedCell()
    {
        return selectedCell;
    }

    void OnWin()
    {
            PlayerPrefs.SetInt("MiniGame4", 1);
            SceneManager.LoadScene("Lobby");
            Debug.Log("winned");
    }

    public bool GetCanPlay()
    {
        return canPlay;
    }

    public void UpgradeCell(AC_GridCell _cellFrom)
    {
        if(canPlay)
        {
            if(CheckForUpgrade(_cellFrom, selectedCell))
            {
                soundManager.PlaySound(5);
                selectedCell.InitCell(UpgradeAtoms(selectedCell.GetCellType(), _cellFrom.GetCellType()));
                _cellFrom.InitCell(AC_ENUM_Cell.CellType.White);

                if(selectedCell.GetCellType() == waitedAtom)
                {
                    canPlay = false;
                    soundManager.PlaySound(7);
                    Invoke("OnWin", 1.75f);
                }

                selectedCell.ResetCell();
                OnCellUnselected();
            }
            else
            {
                soundManager.PlaySound(6);
            }
        }
        
    }

    private bool CheckForUpgrade(AC_GridCell _cellToUpgrade, AC_GridCell _cellToDestroy)
    {
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