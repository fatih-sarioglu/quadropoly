using UnityEngine;

[ExecuteInEditMode] // This allows the script to run in the Editor
public class BoardLayout : MonoBehaviour
{
    public GameObject spacePrefab;
    public float spaceWidth = 1.0f;
    public float spaceLength = 1.5f;

    [Header("Editor Tools")]
    public bool buildBoard; // Check this to build
    public bool clearBoard; // Check this to delete everything

    void Update()
    {
        // Check if the user toggled the "Build" checkbox in the Inspector
        if (buildBoard)
        {
            buildBoard = false; // Reset the checkbox immediately
            CreateBoard();
        }

        // Check if the user toggled the "Clear" checkbox
        if (clearBoard)
        {
            clearBoard = false;
            ClearBoard();
        }
    }

    void CreateBoard()
    {
        ClearBoard();

        // 1. Get the BoardManager so we can look at your 40 data assets
        BoardManager boardManager = GetComponent<BoardManager>();

        for (int i = 0; i < 40; i++)
        {
            Vector3 position = CalculatePosition(i);
            Quaternion rotation = CalculateRotation(i);

            GameObject newSpace = Instantiate(spacePrefab, position, rotation);
            newSpace.transform.SetParent(this.transform);

            // 2. Add the SpaceController to this specific tile
            SpaceController controller = newSpace.AddComponent<SpaceController>();

            // 3. Link the data from BoardManager to this tile
            if (boardManager != null && boardManager.spaceDataList.Count > i)
            {
                controller.Initialize(boardManager.GetSpaceData(i));
            }
            else
            {
                newSpace.name = "Space_" + i + " (No Data Found)";
            }

            if (i % 10 == 0) // Corner
            {
                newSpace.transform.localScale = new Vector3(newSpace.transform.localScale.x * spaceLength, newSpace.transform.localScale.y, newSpace.transform.localScale.z);
            }
        }
        Debug.Log("Board Built Successfully!");
    }

    void ClearBoard()
    {
        // This helper deletes all children of the "BoardParent"
        // We use a while loop because children indices change when destroyed
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    // Rotations: Rectangles must face the 'center' of the board
    Quaternion CalculateRotation(int index)
    {
        if (index < 10) return Quaternion.Euler(0, -90, 0);   // Side 1: Left
        if (index < 20) return Quaternion.Euler(0, 0, 0);  // Side 2: Top-Left
        if (index < 30) return Quaternion.Euler(0, 90, 0);  // Side 3: Top-Right
        return Quaternion.Euler(0, 180, 0);                    // Side 4: Right
    }

    Vector3 CalculatePosition(int index)
    {
        float x = 0;
        float z = 0;
        
        // Calculate the center-to-center distance for the edge
        // The edge consists of 9 properties and the "gap" to the next corner
        float edgeLength = (9 * spaceWidth) + spaceLength;

        // Side 1: Left (GO to Jail)
        if (index < 10) {
            x = 0;
            z = CalculateOffset(index);
        }
        // Side 2: Top (Jail to Free Parking)
        else if (index < 20) {
            x = CalculateOffset(index - 10);
            z = edgeLength;
        }
        // Side 3: Right (Free Parking to Go To Jail)
        else if (index < 30) {
            x = edgeLength;
            z = edgeLength - CalculateOffset(index - 20);
        }
        // Side 4: Bottom (Go To Jail back to GO)
        else {
            x = edgeLength - CalculateOffset(index - 30);
            z = 0;
        }

        return new Vector3(x, 0, z);
    }

    // This helper function handles the shift needed for corners vs properties
    float CalculateOffset(int stepInSide)
    {
        if (stepInSide == 0) return 0; // The Corner itself
        
        // Position = (Half of Corner) + (Half of Property) + (Steps * Width)
        // But since we place by center, it's simpler:
        float firstPropertyPos = (spaceLength / 2f) + (spaceWidth / 2f);
        return firstPropertyPos + ((stepInSide - 1) * spaceWidth);
    }
}
