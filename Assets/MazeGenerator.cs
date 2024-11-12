using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    private int[,] maze;
    private System.Random rand = new System.Random();

    void Start()
    {
        GenerateMaze();
        DrawMaze();
    }

    void GenerateMaze()
    {
        // Ensure width and height are odd to avoid extra outer walls
        width = width % 2 == 0 ? width - 1 : width;
        height = height % 2 == 0 ? height - 1 : height;

        maze = new int[width, height];

        // Initialize maze cells as walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1; // 1 represents wall
            }
        }

        // Start carving the maze from (1,1)
        CarvePath(1, 1);

        // Set the starting point (1, 1) and the end point near the opposite corner
        maze[1, 1] = 0; // Start point
        maze[width - 2, height - 2] = 0; // End point
    }

    void CarvePath(int x, int y)
    {
        int[] directions = { 0, 1, 2, 3 };
        ShuffleArray(directions);

        foreach (int direction in directions)
        {
            int nx = x, ny = y;

            switch (direction)
            {
                case 0: ny = y - 2; break; // Up
                case 1: ny = y + 2; break; // Down
                case 2: nx = x - 2; break; // Left
                case 3: nx = x + 2; break; // Right
            }

            if (nx > 0 && ny > 0 && nx < width - 1 && ny < height - 1 && maze[nx, ny] == 1)
            {
                maze[nx, ny] = 0;
                maze[x + (nx - x) / 2, y + (ny - y) / 2] = 0;
                CarvePath(nx, ny);
            }
        }
    }

    void DrawMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 1 && y == 1)
                {
                    // Start point with a green cube
                    GameObject startCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    startCube.transform.position = new Vector3(x, 0, y);
                    startCube.transform.localScale = new Vector3(1, 2, 1);
                    startCube.GetComponent<Renderer>().material.color = Color.green;
                }
                else if (x == width - 2 && y == height - 2)
                {
                    // End point with a red cube
                    GameObject endCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    endCube.transform.position = new Vector3(x, 0, y);
                    endCube.transform.localScale = new Vector3(1, 2, 1);
                    endCube.GetComponent<Renderer>().material.color = Color.red; // Make it red to indicate the end
                }
                else if (maze[x, y] == 1)
                {
                    // Wall cube
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.position = new Vector3(x, 0, y);
                    wall.transform.localScale = new Vector3(1, 2, 1);
                }
            }
        }
    }


    void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int r = rand.Next(i + 1);
            int temp = array[i];
            array[i] = array[r];
            array[r] = temp;
        }
    }
}
