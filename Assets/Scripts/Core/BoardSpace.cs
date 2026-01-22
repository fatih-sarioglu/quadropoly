using UnityEngine;

// This allows you to right-click in Unity to create new spaces
[CreateAssetMenu(fileName = "NewBoardSpace", menuName = "Quadropoly/Space")]
public class BoardSpace : ScriptableObject
{
    public string spaceName;
    public int boardIndex; // 0 to 39
    public SpaceType type;

    public enum SpaceType
    {
        Property,
        Station,
        Utility,
        Tax,
        Chance,
        CommunityChest,
        GO,
        Jail,
        FreePark,
        GoToJail
    }
}