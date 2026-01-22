using UnityEngine;

public class SpaceController : MonoBehaviour
{
    // This will hold the ScriptableObject data (PropertySpace or BoardSpace)
    public BoardSpace data;

    // We can use this later to display the name or color in the UI
    public void Initialize(BoardSpace spaceData)
    {
        data = spaceData;
        this.name = spaceData.boardIndex + "_" + spaceData.spaceName;
    }
}