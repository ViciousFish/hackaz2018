using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Algorithms;
using MazeGrid;

public class MazeTest : NetworkBehaviour
{

    public int rows;
    public int columns;
    public GameObject mazeContainer, spawnManagerReference;
    private float wallLength = 3.0f;

    public int numOfFirstTier;
    public int numOfSecondTier;
    public int numOfThirdTier;
    public int numOfFinalTier;
    public int numOfGems;

    // Use this for initialization

	void Start ()
	{
		var grid = new MazeGrid.Grid (rows, columns);

		var maze = HuntAndKill.CreateMaze (grid);


		for (int x = 0; x < columns / 2; x++) {
			int row = Random.Range (1, rows - 2);
			int column = Random.Range (1, columns - 2);

			Cell cell = maze.GetCell (row, column);
			if (cell.Links ().Count < 4) {
				Cell neighbor = null;

				while (neighbor == null) {
					int direction = Random.Range (0, 3);
					switch (direction) {
					case 0:
						if (!cell.IsLinked (cell.North)) {
							neighbor = cell.North;
						}
						break;
					case 1:
						if (!cell.IsLinked (cell.East)) {
							neighbor = cell.East;
						}
						break;
					case 2:
						if (!cell.IsLinked (cell.South)) {
							neighbor = cell.South;
						}
						break;
					case 3:
						if (!cell.IsLinked (cell.West)) {
							neighbor = cell.West;
						}
						break;
					}
				}
				cell.Link (neighbor);
			}
		}

		for (int i = 0; i < maze.Rows; i++) {
			for (int j = 0; j < maze.Columns; j++) {
				Cell cell = maze.GetCell (i, j);
				int links = cell.Links ().Count;

				GameObject gameObject = null;

				switch (links) {
				case 0:
					gameObject = Instantiate (Resources.Load ("JamesTesting/Prefabs/Enclosed", typeof(GameObject))) as GameObject;
					break;

				case 1:
                        // Dead end
					gameObject = Instantiate (Resources.Load ("JamesTesting/Prefabs/DeadEnd", typeof(GameObject))) as GameObject;
					Vector3 rotation;
					if (cell.IsLinked (cell.North)) {
						rotation = new Vector3 (0f, 270f, 0f);
					} else if (cell.IsLinked (cell.East)) {
						rotation = new Vector3 (0f, 0f, 0f);
					} else if (cell.IsLinked (cell.South)) {
						rotation = new Vector3 (0f, 90f, 0f);
					} else {
						rotation = new Vector3 (0f, 180f, 0f);
					}

					gameObject.transform.localEulerAngles = rotation;
					break;

				case 2:
                        //Hallway or corner
                        
					if (cell.IsLinked (cell.North) && cell.IsLinked (cell.South)) {
						gameObject = Instantiate (Resources.Load ("JamesTesting/Prefabs/Hallway", typeof(GameObject))) as GameObject;
						gameObject.transform.localEulerAngles = new Vector3 (0f, 90f, 0f);
					} else if (cell.IsLinked (cell.East) && cell.IsLinked (cell.West)) {
						gameObject = Instantiate (Resources.Load ("JamesTesting/Prefabs/Hallway", typeof(GameObject))) as GameObject;
						gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
					} else if (cell.IsLinked (cell.North) && cell.IsLinked (cell.East)) {
						gameObject = Instantiate (Resources.Load ("JamesTesting/Prefabs/Corner", typeof(GameObject))) as GameObject;
						gameObject.transform.localEulerAngles = new Vector3 (0f, 270f, 0f);
					} else if (cell.IsLinked (cell.North) && cell.IsLinked (cell.West)) {
						gameObject = Instantiate (Resources.Load ("JamesTesting/Prefabs/Corner", typeof(GameObject))) as GameObject;
						gameObject.transform.localEulerAngles = new Vector3 (0f, 180f, 0f);
					} else if (cell.IsLinked (cell.South) && cell.IsLinked (cell.East)) {
						gameObject = Instantiate (Resources.Load ("JamesTesting/Prefabs/Corner", typeof(GameObject))) as GameObject;
						gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
					} else {
						gameObject = Instantiate (Resources.Load ("JamesTesting/Prefabs/Corner", typeof(GameObject))) as GameObject;
						gameObject.transform.localEulerAngles = new Vector3 (0f, 90f, 0f);
					}
					break;

				case 3:
                        
                        gameObject = Instantiate(Resources.Load("JamesTesting/Prefabs/SingleWall", typeof(GameObject))) as GameObject;
                        if (!cell.IsLinked(cell.North))
                        {
                            gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                        }
                        if (!cell.IsLinked(cell.East))
                        {
                            gameObject.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                        }
                        if (!cell.IsLinked(cell.South))
                        {
                            gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        }
                        if (!cell.IsLinked(cell.West))
                        {
                            gameObject.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
                        }
                        break;
                    case 4:
                        gameObject = Instantiate(Resources.Load("JamesTesting/Prefabs/NoWalls", typeof(GameObject))) as GameObject;
                        break;
                }
                setLocalPosition(gameObject, i, j);
            }
        }


        createSpawnPoints(numOfFirstTier, maze, "enemyTier1");
        createSpawnPoints(numOfSecondTier, maze, "enemyTier2");
        createSpawnPoints(numOfThirdTier, maze, "enemyTier3");
        createSpawnPoints(numOfFinalTier, maze, "enemyTier4");
        createGemSpawnPoints(numOfGems, maze, "gem");
        spawnManagerReference.GetComponent<SpawnManager>().StartManagement();
    }

    void setLocalPosition(GameObject gObject, int i, int j)
    {
        gObject.transform.parent = mazeContainer.transform;
        gObject.transform.localPosition = new Vector3(i * wallLength + wallLength / 2, 0.0f, j * wallLength + wallLength / 2);
        NetworkServer.Spawn(gObject);
    }

    void setSpawnLocalPosition(GameObject gObject, int i, int j){
        gObject.transform.parent = mazeContainer.transform;
        gObject.transform.localPosition = new Vector3(i * wallLength + wallLength / 2, 0.0f, j * wallLength + wallLength / 2);
        gObject.transform.parent =  spawnManagerReference.transform;
    }

    void createGemSpawnPoints(int numOfSpawns, MazeGrid.Grid maze, string spawnTag){

        // Upper right corner
        int row = Random.Range(0, rows / 2);
        int col = Random.Range(columns / 2, columns - 1);

        GameObject spawnPoint = Instantiate(Resources.Load("JamesTesting/Prefabs/SpawnPoint", typeof(GameObject)), spawnManagerReference.transform) as GameObject;
        spawnPoint.tag = spawnTag;
        setSpawnLocalPosition(spawnPoint, row, col);

        // Lower left corner
        row = Random.Range(rows / 2, rows - 1);
        col = Random.Range(0, columns / 2);

        spawnPoint = Instantiate(Resources.Load("JamesTesting/Prefabs/SpawnPoint", typeof(GameObject)), spawnManagerReference.transform) as GameObject;
        spawnPoint.tag = spawnTag;
        setSpawnLocalPosition(spawnPoint, row, col);

        // Lower right corner
        row = Random.Range(rows / 2, rows - 1);
        col = Random.Range(columns / 2, columns - 1);

        spawnPoint = Instantiate(Resources.Load("JamesTesting/Prefabs/SpawnPoint", typeof(GameObject)), spawnManagerReference.transform) as GameObject;
        spawnPoint.tag = spawnTag;
        setSpawnLocalPosition(spawnPoint, row, col);

        
    }

    void createSpawnPoints(int numOfSpawns, MazeGrid.Grid maze, string spawnTag){
        for (int i = 0; i < numOfSpawns; i++){

            int row = Random.Range(0, rows - 1);
            int col = Random.Range(0, columns - 1);

            while(row < 3 && col < 3){
                row = Random.Range(0, rows - 1);
                col = Random.Range(0, columns - 1);
            }

            GameObject spawnPoint = Instantiate(Resources.Load("JamesTesting/Prefabs/SpawnPoint", typeof(GameObject)), spawnManagerReference.transform) as GameObject;
            spawnPoint.tag = spawnTag;
            setSpawnLocalPosition(spawnPoint, row, col);
        }
    }

    Vector3 getNextPosition(MazeGrid.Grid maze, Cell current, Cell target)
    {
        MazeSolver solver = new MazeSolver(maze, 0, 0, rows - 1, columns - 1);
        LinkedList<Cell> path = solver.Solve(current);

        Cell next = path.First.Next.Value;
        return new Vector3(next.Row * wallLength + wallLength / 2, 0.0f, next.Column * wallLength + wallLength / 2);
    }

}
