using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    // Drag your 40 ScriptableObject assets here in the Inspector in order
    public List<BoardSpace> spaceDataList = new List<BoardSpace>();

    public BoardSpace GetSpaceData(int index)
    {
        return spaceDataList[index];
    }
}