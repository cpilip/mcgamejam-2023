using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreator : MonoBehaviour
{
    //Singleton for the MazeCreator
    private static MazeCreator instance;
    public static MazeCreator Instance { get { return instance; } }

    //Maze generation and data-related
    [SerializeField] private int numRows = 10;
    [SerializeField] private int numCols = 6;
    private MazeCell[,] ellersMaze;

    private int currLabel;
    private System.Random rnd = new System.Random();

    //Graph conversion and path calculation
    private int[,] adjMatrix;
    //private Hash

    //Unity-related
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject dynamic;
    [SerializeField] private Vector3 unityRowPosition = new Vector3(0f, 0f, 0f); //Initial row's Unity position
    private Vector3 zero = new Vector3(0f, 0f, 0f);
    private List<GameObject> unityRows = new List<GameObject>(); //List of row GameObjects

    

    void Start()
    {
        //Initialize MazeCreator
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        ellersMaze = new MazeCell[numRows, numCols];
        adjMatrix = new int[numRows * numCols, numRows * numCols];

        //Initialize the maze with empty maze cells
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                ellersMaze[row, col] = new MazeCell();
            }
        }

        //Labelling first row from; zero means a cell is unlabelled
        for (int i = 1; i <= numCols; i++)
        {
            ellersMaze[0, i - 1].SetLabel(i);
        }

        //The current label
        currLabel = numCols + 1;

        GenerateEllersMaze();
        ConvertEllersMaze();
        //PrintEllersMaze();
    }

    //Merge across a row
    //Merge two cells across a row (stopping at the last cell, which should keep its right wall) 
    //If cell A and B are of the same set, do not merge them; otherwise, flip a coin to decide if the right wall of cell A gets destroyed
    private void MergeHorizontally(int row)
    {
        for (int col = 0; col < numCols - 1; col++)
        {
            if (ellersMaze[row, col].GetLabel() != ellersMaze[row, col + 1].GetLabel())
            {
                if (rnd.Next(0, 2) == 1)
                {
                    ellersMaze[row, col].DestroyRightWall();

                    //If cell A's right wall is destroyed, set cell B to the same label as the current cell
                    int oldLabel = ellersMaze[row, col + 1].GetLabel();
                    ellersMaze[row, col + 1].SetLabel(ellersMaze[row, col].GetLabel());

                    //If any other indices have the same label, set them to the new label
                    for (int c = 0; c < numCols; c++)
                    {
                        if (ellersMaze[row, c].GetLabel() == oldLabel)
                        {
                            ellersMaze[row, c].SetLabel(ellersMaze[row, col].GetLabel());
                        }
                    }
                }
            }

        }
    }

    //Merge two rows
    private void MergeVertically(int prevRow, int currRow)
    {
        //Extend all present sets first - create a map of the label and the indices associated with the label
        Dictionary<int, List<int>> setMap = new Dictionary<int, List<int>>();

        for (int col = 0; col < numCols; col++)
        {
            if (setMap.TryGetValue(ellersMaze[prevRow, col].GetLabel(), out List<int> value))
            {
                List<int> indices = setMap[ellersMaze[prevRow, col].GetLabel()];
                indices.Add(col);

                setMap[ellersMaze[prevRow, col].GetLabel()] = indices;
            }
            else
            {
                List<int> indices = new List<int>();
                indices.Add(col);
                setMap.Add(ellersMaze[prevRow, col].GetLabel(), indices);
            }
        }

        foreach (KeyValuePair<int, List<int>> set in setMap)
        {
            //Choose at least one index to extend for each label in the row
            int indexToExtend = set.Value[(rnd.Next(set.Value.Count))];
            ellersMaze[prevRow, indexToExtend].DestroySouthWall();

            //If the south wall of the current cell is destroyed, set the cell south of the current cell to the same label as the current cell
            ellersMaze[currRow, indexToExtend].SetLabel(ellersMaze[prevRow, indexToExtend].GetLabel());

            //Flip a coin for the rest of the indices that have this label
            foreach (int m in set.Value)
            {
                if (rnd.Next(0, 2) == 1 && m != indexToExtend) //Ignore the index we've already extended
                {
                    ellersMaze[prevRow, m].DestroySouthWall();
                    ellersMaze[currRow, m].SetLabel(ellersMaze[prevRow, m].GetLabel());
                }
            }
        }

        //Finally, assign a new label to the unlabelled cells in the next row
        for (int col = 0; col < numCols; col++)
        {
            if (ellersMaze[currRow, col].GetLabel() == 0)
            {
                ellersMaze[currRow, col].SetLabel(currLabel);
                currLabel++;
            }
        }

    }

    private void GenerateEllersMaze()
    {
        //Create Unity rows
        for (int row = 0; row < numRows; row++)
        {
            GameObject currRow = Instantiate(rowPrefab, zero, Quaternion.identity);
            for (int col = 1; col < numCols; col++)
            {
                // Spawn new cell
                GameObject newCell = Instantiate(cellPrefab, zero, Quaternion.identity);
                Vector3 spawnAtPosition = new Vector3(3.0f * col, 0f, 0f);

                // Set parent of new cell
                newCell.name = "Maze Cell " + col;
                newCell.transform.SetParent(currRow.transform);
                newCell.transform.position = spawnAtPosition;
            }

            unityRows.Add(currRow);
            currRow.transform.position = unityRowPosition;
            unityRowPosition.y -= 3f;
            currRow.transform.SetParent(dynamic.transform);
        }

        //Create maze, stopping at the last row
        for (int currRow = 0; currRow < numRows - 1; currRow++)
        {
            MergeHorizontally(currRow);
            MergeVertically(currRow, currRow + 1);

            //Update corresponding rows in Unity
            for (int i = 0; i < numCols; i++)
            {
                //Get Unity maze cell
                Transform currentMazeCell = unityRows[currRow].transform.GetChild(i);
                Transform nextMazeCell = unityRows[currRow + 1].transform.GetChild(i);

                if (ellersMaze[currRow, i].HasRightWall() == false)
                {
                    Destroy(currentMazeCell.GetChild(0).gameObject); //Cannot change children in a prefab, so I can safely assume that this object is the right/east wall
                }

                if (ellersMaze[currRow, i].HasSouthWall() == false)
                {
                    Destroy(currentMazeCell.GetChild(1).gameObject); //Destroy south wall of current maze cell
                    Destroy(nextMazeCell.GetChild(2).gameObject); //Destroy north wall of adjacent maze cell in the next row
                }
            }
        }

        //Last row just merges all disjoint cells
        for (int col = 0; col < numCols - 1; col++)
        {
            if (ellersMaze[numRows - 1, col].GetLabel() != ellersMaze[numRows - 1, col + 1].GetLabel())
            {
                ellersMaze[numRows - 1, col].DestroyRightWall();

                //If cell A's right wall is destroyed, set cell B to the same label as the current cell
                int oldLabel = ellersMaze[numRows - 1, col + 1].GetLabel();
                ellersMaze[numRows - 1, col + 1].SetLabel(ellersMaze[numRows - 1, col].GetLabel());

                //If any other indices have the same label, set them to the new label
                for (int c = 0; c < numCols; c++)
                {
                    if (ellersMaze[numRows - 1, c].GetLabel() == oldLabel)
                    {
                        ellersMaze[numRows - 1, c].SetLabel(ellersMaze[numRows - 1, col].GetLabel());
                    }
                }
            }
        }

        //Update last row in Unity
        for (int i = 0; i < numCols; i++)
        {
            //Get Unity maze cell
            Transform currentMazeCell = unityRows[numRows - 1].transform.GetChild(i);

            if (ellersMaze[numRows - 1, i].HasRightWall() == false)
            {
                Destroy(currentMazeCell.GetChild(0).gameObject); //Destroy right/east wall
            }
        }
    }
    private void ConvertEllersMaze()
    {
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                if (ellersMaze[row, col].HasSouthWall())
                {
                    //cell += "S";
                }
                if (ellersMaze[row, col].HasRightWall())
                {
                    //cell += "R";
                }
            }
        }
    }

    private void PrintEllersMaze()
    {
        for (int row = 0; row < numRows; row++)
        {
            string rowCells = "";
            for (int col = 0; col < numCols; col++)
            {
                string cell = ellersMaze[row, col].GetLabel().ToString();
                if (ellersMaze[row, col].HasSouthWall())
                {
                    cell += "S";
                }
                if (ellersMaze[row, col].HasRightWall())
                {
                    cell += "R";
                }

                cell += "\t";
                rowCells += cell;
            }
            Debug.Log(rowCells);
        }
    }

    private class MazeCell
    {
        private bool hasSouthWall = true;
        private bool hasRightWall = true;
        private int label = 0;

        public MazeCell()
        {

        }

        public bool HasSouthWall()
        {
            return hasSouthWall;
        }

        public void DestroySouthWall()
        {
            hasSouthWall = false;
        }

        public bool HasRightWall()
        {
            return hasRightWall;
        }

        public void DestroyRightWall()
        {
            hasRightWall = false;
        }

        public int GetLabel()
        {
            return label;
        }
        public void SetLabel(int label)
        {
            this.label = label;
        }
    }
}