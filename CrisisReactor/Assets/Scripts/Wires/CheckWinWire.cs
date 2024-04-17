using UnityEngine;

public class CheckWinWire : MonoBehaviour
{
    public static int maxWire;
    public static int nbWireWonnect;

    public static bool CheckWin()
    {
        return maxWire == nbWireWonnect;
    }
}
