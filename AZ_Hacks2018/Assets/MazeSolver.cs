using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeGrid;

public class MazeSolver {
    private MazeGrid.Grid maze;
    private int startRow, startCol, endRow, endCol;

    public MazeSolver(MazeGrid.Grid maze, int startRow, int startCol, int endRow, int endCol)
    {
        this.maze = maze;
        this.startRow = startRow;
        this.startCol = startCol;
        this.endRow = endRow;
        this.endCol = endCol;
    }

    public LinkedList<Cell> Solve(Cell start)
    {
        LinkedList<Cell> path = new LinkedList<Cell>();
        if (start.Row == endRow && start.Column == endCol)
        {
            path.AddLast(start);
            start.visited = true;
            return path;
        } else if (start.visited)
        {
            return path;
        } else
        {
            start.visited = true;
            foreach (Cell c in start.Links())
            {
                LinkedList<Cell> childPath = Solve(c);
                foreach(Cell cell in childPath)
                {
                    if (cell.Row == endRow && cell.Column == endCol)
                    {
                        path.AddLast(start);
                        foreach(Cell child in childPath)
                        {
                            path.AddLast(child);
                        }
                        return path;
                    }
                }
            }
        }
        return path;
    }

}

class Coordinate
{
    public int row;
    public int col;

    public Coordinate(int row, int col)
    {
        this.row = row;
        this.col = col;
    }
}
