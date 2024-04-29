using System.Collections.Generic;
using UnityEngine;

public class CheckWinWire : MonoBehaviour
{
    [SerializeField] private List<WG_GameManager> listWG = new();

    public static CheckWinWire Instance;

    private void Awake()
    {
        Instance = this;
    }
    public bool CheckWin()
    {
        for (int i = 0; i < listWG.Count; i++)
        {
            if (!listWG[i].isConnect) 
            {
                return false;
            }
        }
        return true;
    }
}
