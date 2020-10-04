using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public GameObject cellPrefab;

	public CellScript[,] grid;

	bool simulate = true;

	int gridWidth = 100;
	int gridHeight = 100;

	float cellDimension = 1f;
	float padding = 0.1f;

	// These varaibles are used to control the rate that the grid updates itself
	int time = 0;
	float timer = 0;
	float timerRate = 0.5f;

	public float possibilitiesOfEachCellBeingAlive;

	public bool boolLayers = true;//true默认一层 

	public bool everAliveReproduction = true;//true默认可以繁殖

	// Start is called before the first frame update
	void Start()
	{
		// Instantiate a 2D array
		grid = new CellScript[gridWidth, gridHeight];

		GameObject cellFather = new GameObject();
		cellFather.name = "cellFather";

		// Instantiate a grid of cubes ("cells") and position it according to its
		// place in the 2 dimensional array ("grid").
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				Vector3 pos = new Vector3(x * (cellDimension + padding), 0, y * (cellDimension + padding));
				GameObject cellObj = Instantiate(cellPrefab, pos, Quaternion.identity);
				cellObj.transform.SetParent(cellFather.transform);
				cellObj.transform.localScale = new Vector3(cellDimension, cellDimension, cellDimension);
				CellScript cs = cellObj.GetComponent<CellScript>();
				cs.possibilitiesOfAlive = possibilitiesOfEachCellBeingAlive;
				cs.x = x;
				cs.y = y;
				grid[x, y] = cs;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;

		if (timer < 0 && simulate) {

            if (boolLayers == true)
            {
				generateNextState();
			}
            else
            {
				generateNextStateTwoLayers();
			}
			
			timer = timerRate;
		}
	}
	
	void generateNextState()
	{
		time++;

		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				List<CellScript> liveNeighbors = gatherLiveNeighbors(x, y);
				//Apply the 4 rules from Conway's Gaem of Life (https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)
				//1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
				if (grid[x, y].Alive && liveNeighbors.Count < 2) {
					grid[x, y].nextAlive = false;
				}
				//2. Any live cell with two or three live neighbours lives on to the next generation.
				else if (grid[x, y].Alive && (liveNeighbors.Count == 2 || liveNeighbors.Count == 3)) {
					grid[x, y].nextAlive = true;
					if (everAliveReproduction == true)
                    {
						grid[x, y].nextAlive = true;
                    }
                    else
                    {
						if (grid[x, y].everAlive == false)
                        {
							grid[x, y].nextAlive = true;
						}
                    }
				}
				//3. Any live cell with more than three live neighbours dies, as if by overpopulation.
				else if (grid[x, y].Alive && liveNeighbors.Count > 3) {
					grid[x, y].nextAlive = false;
				}
				//4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
				else if (!grid[x, y].Alive && liveNeighbors.Count == 3) {
					if (everAliveReproduction == true)
					{
						grid[x, y].nextAlive = true;
					}
					else
					{
						if (grid[x, y].everAlive == false)
						{
							grid[x, y].nextAlive = true;
						}
					}
					//grid[x, y].nextAlive = true;
				}
			}
		}
		
		//Now that we have looped through all of the cells in the grid, and stored what their alive status should
		//	be in each cell's nextAlivevariable, update each cell's alive state to be that value.
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				grid[x, y].Alive = grid[x, y].nextAlive;
			}
		}
	}
	
	List<CellScript> gatherLiveNeighbors(int x, int y)
	{
		List<CellScript> liveNeighbors = new List<CellScript>();
		//Spend some time thinking about how this considers all surrounding cells in grid[x,y]
		//why now indexing bad values of grid.
		for (int i = Mathf.Max(0, x - 1); i <= Mathf.Min(gridWidth - 1, x + 1); i++) {
			for (int j = Mathf.Max(0, y - 1); j <= Mathf.Min(gridHeight - 1, y + 1); j++) {
				//Add all live neighbors of (x, y) excluding itself
				if (!(x == i && y == j)) {
					if (grid[i,j].Alive) {
						liveNeighbors.Add(grid[i,j]);
					}
				}
			}
		}

		return liveNeighbors;
	}

	void generateNextStateTwoLayers()
	{
		Debug.Log("layer2");
		time++;

		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				float liveNeighbors = gatherTwoLayersLiveNeighbors(x, y);
				//Apply the 4 rules from Conway's Gaem of Life (https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)
				//1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
				if (grid[x, y].Alive && liveNeighbors < 2)
				{
					grid[x, y].nextAlive = false;
				}
				//2. Any live cell with two or three live neighbours lives on to the next generation.
				else if (grid[x, y].Alive && (liveNeighbors == 2 || liveNeighbors== 3))
				{
					if (everAliveReproduction == true)
					{
						grid[x, y].nextAlive = true;
					}
					else
					{
						if (grid[x, y].everAlive == false)
						{
							grid[x, y].nextAlive = true;
						}
					}
					//grid[x, y].nextAlive = true;
				}
				//3. Any live cell with more than three live neighbours dies, as if by overpopulation.
				else if (grid[x, y].Alive && liveNeighbors > 3)
				{
					grid[x, y].nextAlive = false;
				}
				//4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
				else if (!grid[x, y].Alive && liveNeighbors == 3)
				{
					if (everAliveReproduction == true)
					{
						grid[x, y].nextAlive = true;
					}
					else
					{
						if (grid[x, y].everAlive == false)
						{
							grid[x, y].nextAlive = true;
						}
					}
					//grid[x, y].nextAlive = true;
				}
			}
		}

		//Now that we have looped through all of the cells in the grid, and stored what their alive status should
		//	be in each cell's nextAlivevariable, update each cell's alive state to be that value.
		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				grid[x, y].Alive = grid[x, y].nextAlive;
			}
		}
	}

	float gatherTwoLayersLiveNeighbors(int x, int y)
	{
		float liveNeighbors = 0;
		//Spend some time thinking about how this considers all surrounding cells in grid[x,y]
		//why now indexing bad values of grid.
		for (int i = Mathf.Max(0, x - 2); i <= Mathf.Min(gridWidth - 1, x + 2); i++)
		{
			for (int j = Mathf.Max(0, y - 2); j <= Mathf.Min(gridHeight - 1, y + 2); j++)
			{
				//Add all live neighbors of (x, y) excluding itself
                if (i == x - 2 || i == x + 2 || j == y - 2 || j == y + 2)
                {
					if (grid[i, j].Alive)
					{
						liveNeighbors += 0.5f;
					}
				}
                else
                {
					if (!(x == i && y == j))
					{
						if (grid[i, j].Alive)
						{
							liveNeighbors += 1;
						}
					}
				}
			}
		}

		return liveNeighbors;
	}

	public void SimulateToggle(bool checkValue)
	{
		simulate = !simulate;
	}
}
