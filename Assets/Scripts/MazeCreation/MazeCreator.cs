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
    private Graph cellGraph;
    private Dictionary<string, int> cellIDToNodeID = new Dictionary<string, int>();
    private Dictionary<int, string> nodeIDToCellID = new Dictionary<int, string>();

    //Unity-related
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject wireboxPrefab;
    [SerializeField] private GameObject dynamic;
    [SerializeField] private Vector3 unityRowPosition = new Vector3(0f, 0f, 0f); //Initial row's Unity position
    [SerializeField] private Vector3 ratSpawnPosition = new Vector3(-2.4f, 0.7f, 0f); //Initial row's Unity position
    private Vector3 zero = new Vector3(0f, 0f, 0f);
    private List<GameObject> unityRows = new List<GameObject>(); //List of row GameObjects
    private List<GameObject> lasers = new List<GameObject>(); //List of laser GameObjects

    void Start()
    {
        GameObject rat = GameObject.FindGameObjectsWithTag("Player")[0];
        rat.transform.position = ratSpawnPosition;

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
        cellGraph = new Graph(numRows * numCols);

        //Initialize the maze with empty maze cells
        int nodeID = 0;
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                ellersMaze[row, col] = new MazeCell();
                
                // Map cell row/col to node number
                cellIDToNodeID.Add(row + " " + col, nodeID);
                nodeIDToCellID.Add(nodeID, row + " " + col);
                nodeID++;
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
        PlaceLasersAndWeakenWalls();
        ConvertEllersMaze();
        CalculateWireLocation();
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
                    //Destroy(currentMazeCell.GetChild(0).gameObject); //Cannot change children in a prefab, so I can safely assume that this object is the right/east wall
                    currentMazeCell.GetChild(0).gameObject.SetActive(false);
                }

                if (ellersMaze[currRow, i].HasSouthWall() == false)
                {
                    //Destroy(currentMazeCell.GetChild(1).gameObject); //Destroy south wall of current maze cell
                    //Destroy(nextMazeCell.GetChild(2).gameObject); //Destroy north wall of adjacent maze cell in the next row
                    currentMazeCell.GetChild(1).gameObject.SetActive(false);
                    nextMazeCell.GetChild(2).gameObject.SetActive(false);
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
                //Destroy(currentMazeCell.GetChild(0).gameObject); //Destroy right/east wall
                currentMazeCell.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    private void PlaceLasersAndWeakenWalls()
    {
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                // A small buffer to leave the top left with no special walls
                if (row > 3 || col > 3)
                {
                    Transform currentMazeCell = unityRows[row].transform.GetChild(col);

                    // 25% chance to change opening into laser
                    if (!ellersMaze[row, col].HasSouthWall())
                    {
                        if (rnd.Next(0, 5) == 1)
                        {
                            ellersMaze[row, col].PlaceSouthLaser();
                            currentMazeCell.GetChild(1).gameObject.SetActive(true);
                            currentMazeCell.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                            lasers.Add(currentMazeCell.GetChild(1).gameObject);
                        }
                    }
                    if (!ellersMaze[row, col].HasRightWall())
                    {
                        if (rnd.Next(0, 5) == 1)
                        {
                            ellersMaze[row, col].PlaceRightLaser();
                            currentMazeCell.GetChild(0).gameObject.SetActive(true);
                            currentMazeCell.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                            lasers.Add(currentMazeCell.GetChild(0).gameObject);
                        }
                    }

                    // 20% chance to change wall into a destructible wall
                    if (ellersMaze[row, col].HasSouthWall() && col != numCols - 1)
                    {
                        if (rnd.Next(0, 6) == 1)
                        {
                            ellersMaze[row, col].WeakenSouthWall();
                            currentMazeCell.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        }
                    }
                    if (!ellersMaze[row, col].HasRightWall() && row != numRows - 1)
                    {
                        if (rnd.Next(0, 6) == 1)
                        {
                            ellersMaze[row, col].WeakenRightWall();
                            currentMazeCell.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        }
                    }
                }
            }
        }
    }

    private void ConvertEllersMaze()
    {
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                // Loop through cell row/col, get associated node number
                // Exploit structure of maze to link to right/south cell if there is an opening
                // This generates a graph where the links only exist between nodes if there is an opening between cells
                // We treat lasers like solid walls, so this graph is only the first traversable part of the maze
                int thisNodeID;
                cellIDToNodeID.TryGetValue(row + " " + col, out thisNodeID);

                if (!ellersMaze[row, col].HasSouthLaser() && !ellersMaze[row, col].HasSouthWall())
                {
                    int southNodeID;
                    cellIDToNodeID.TryGetValue((row + 1) + " " + col, out southNodeID);

                    cellGraph.addEdge(thisNodeID, southNodeID);
                    Debug.Log("S " + thisNodeID + " " + southNodeID);
                }
                if (!ellersMaze[row, col].HasRightLaser() && !ellersMaze[row, col].HasRightWall())
                {
                    int rightNodeID;
                    cellIDToNodeID.TryGetValue(row + " " + (col + 1), out rightNodeID);

                    cellGraph.addEdge(thisNodeID, rightNodeID);
                    Debug.Log("R " + thisNodeID + " " + rightNodeID);
                }
            }
        }
    }
    private void CalculateWireLocation()
    {
        // BFS search to get the longest possible path from the traversable part of the maze, and get the last cell of that path
        int startNodeID;
        cellIDToNodeID.TryGetValue(0 + " " + 0, out startNodeID);
        int lastNodeID = cellGraph.breadthFirstSearch(startNodeID);

        string lastCellID;
        nodeIDToCellID.TryGetValue(lastNodeID, out lastCellID);

        Debug.Log("Longest path is from " + 0 + " " + 0 + " to " + lastCellID);
        string[] coords = lastCellID.Split(' ');

        int row = int.Parse(coords[0]);
        int col = int.Parse(coords[1]);

        Vector3 locationToSpawnWireboxAt = unityRows[row].transform.GetChild(col).position;

        Instantiate(wireboxPrefab, locationToSpawnWireboxAt, Quaternion.identity);
        wireboxPrefab.transform.GetChild(0).GetComponent<InteractableWires>().SetObjectToChange(this.gameObject);
        // TODO Spawn wire and associated functions
    }

    public void ApplyWireEffect()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
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

    private class Graph
    {
        private int numVertices;
        private List<int>[] adjList;

        public Graph(int vertices)
        {
            this.numVertices = vertices;
            this.adjList = new List<int>[this.numVertices];

            for (int i = 0; i < this.numVertices; ++i)
            {
                this.adjList[i] = new List<int>();
            }
        }

        public void addEdge(int u, int v)
        {
            this.adjList[u].Add(v);
            this.adjList[v].Add(u);
        }

        public int breadthFirstSearch(int u)
        {
            int[] dist = new int[this.numVertices];

            for (int i = 0; i < this.numVertices; i++)
            {
                dist[i] = -1;
            }

            Queue<int> q = new Queue<int>();
            q.Enqueue(u);

            dist[u] = 0;
            while (q.Count != 0)
            {
                int t = q.Dequeue();

                for (int i = 0; i < this.adjList[t].Count; i++)
                {
                    int v = this.adjList[t][i];

                    if (dist[v] == -1)
                    {
                        q.Enqueue(v);
                        dist[v] = dist[t] + 1;
                    }
                }
            }
            int maxDist = 0;
            int lastCell = 0;

            // get farthest node distance and its index
            for (int i = 0; i < this.numVertices; ++i)
            {
                if (dist[i] > maxDist)
                {
                    maxDist = dist[i];
                    lastCell = i;
                }
            }

            return lastCell;
        }

    }

    private class MazeCell
    {
        private bool hasSouthWall = true;
        private bool hasRightWall = true;
        private bool hasSouthLaser = false;
        private bool hasRightLaser = false;
        private bool isSouthDestructible = false;
        private bool isRightDestructible = false;
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

        public bool HasSouthLaser()
        {
            return hasSouthLaser;
        }
        public void PlaceSouthLaser()
        {
            hasSouthLaser = true;
        }

        public bool IsSouthDestructible()
        {
            return isSouthDestructible;
        }

        public void WeakenSouthWall()
        {
            isSouthDestructible = true;
        }

        public bool HasRightWall()
        {
            return hasRightWall;
        }
        public void DestroyRightWall()
        {
            hasRightWall = false;
        }

        public bool HasRightLaser()
        {
            return hasRightLaser;
        }

        public void PlaceRightLaser()
        {
            hasRightLaser = true;
        }

        public bool IsRightDestructible()
        {
            return isRightDestructible;
        }

        public void WeakenRightWall()
        {
            isRightDestructible = true;
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