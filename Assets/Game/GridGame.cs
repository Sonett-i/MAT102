using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Grid : ScriptableObject
{
    public int Width;
    public int Height;
    public GameObject[] GridObjects;
    public GameObject gridObject;

    public CubePlayer player;
    GameObject lastPos;

    public Grid(int width, int height, GameObject gridObject)
	{
		Width = width;
		Height = height;
		GridObjects = new GameObject[width * height];
		this.gridObject = gridObject;
        this.GridObjects = new GameObject[Width * Height];
        Generate();
	}

	public void Generate()
	{
        int i = 0;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GridObjects[i] = Instantiate(gridObject);
                GridObjects[i].transform.position = new Vector3(x * 10f, 0, y * 10f);
                i++;
            }
        }
    }

    public int CoordinateToIndex(Vector2 Position)
	{
        int index = (int)(Position.y * this.Width + Position.x);

        return index;
    }

    public Vector2Int IndexToCoordinate(int index)
	{

        int x = index % Width; // Calculate x-coordinate
        int y = index / Height; // Calculate y-coordinate

        return new Vector2Int(x, y);
    }

    public GameObject PlayerPosition(Vector2Int pos)
	{
        return GridObjects[CoordinateToIndex(pos)];
	}

    public bool OutofBounds(Vector2Int position)
	{
        if (position.x < 0 || position.x >= Width)
		{
            return false;
		}
        if (position.y < 0 || position.y >= Height)
		{
            return false;
		}
        return true;
	}
    public void DrawPlayer(CubePlayer player)
	{

        Debug.Log($"Player Position: x: {player.Position.y}, y: {player.Position.x}");
        if (lastPos != null)
		{
            lastPos.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
        }
        GameObject go = PlayerPosition(player.Position);
        go.GetComponent<MeshRenderer>().material.color = player.Colour;
        lastPos = go;
	}
}
class CubePlayer
{
    public Vector2Int Position = new Vector2Int(0, 0);
    public GameObject currentTile;
    public int currentIndex = 0;
    public Color Colour;

    public CubePlayer(Vector2Int position, Color colour)
	{
		Position = position;
        this.Colour = colour;
	}

	public void Move(int x, int y, Grid grid)
	{
        Vector2Int newPos = new Vector2Int(this.Position.x + x, this.Position.y + y);

        if (!grid.OutofBounds(newPos))
		{
            Debug.Log("OUT OF BOUNDS");
        }
        else
		{
            this.Position.x = newPos.x;
            this.Position.y = newPos.y;
            grid.DrawPlayer(this);
        }
	}
}

public class GridGame : MonoBehaviour
{
    [SerializeField] Vector2 Box = new Vector2();
    [SerializeField] GameObject gridObject;

    [SerializeField] Color playerColour = new Color(0, 0, 0);
    [SerializeField] Color wallColour = new Color(0, 0, 0);
    [SerializeField] Color endColour = new Color(0, 0, 0);

    Vector2 End = new Vector2(0, 0);


    CubePlayer player;
    Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        if (gridObject)
		{
            grid = new Grid((int)Box.x, (int)Box.y, gridObject);
            player = new CubePlayer(new Vector2Int(0, 0), playerColour);
            grid.DrawPlayer(player);
        }
    }

	private void HandleMovement()
	{
        // Vertical
		if (Input.GetKeyDown(KeyCode.S))
		{
            player.Move(-1, 0, grid);
		}
        if (Input.GetKeyDown(KeyCode.W))
		{
            player.Move(1, 0, grid);
		}
        // Horizontal
        if (Input.GetKeyDown(KeyCode.D))
		{
            player.Move(0, 1, grid);
		}
        if (Input.GetKeyDown(KeyCode.A))
		{
            player.Move(0, -1, grid);
		}
	}

    void UpdatePlayerPosition()
	{
        grid.DrawPlayer(player);
	}
	void Update()
	{
		HandleMovement();
        //UpdatePlayerPosition();
	}

}
