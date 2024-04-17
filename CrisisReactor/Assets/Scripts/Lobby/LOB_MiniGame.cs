using UnityEngine;

[CreateAssetMenu(fileName = "MiniGame", menuName = "ScriptableObject/LobbyScriptableObject", order = 1)]
public class LOB_MiniGame : ScriptableObject
{
    public float size;
    public Vector2 position;
    public string miniGameScene;
}
