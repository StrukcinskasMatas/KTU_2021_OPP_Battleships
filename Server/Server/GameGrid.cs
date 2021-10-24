using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Grid
    {
        private int size;
        private Cell[,] grid;

        public Grid(int size)
        {
            this.size = size;
            Cell[,] grid = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = new Cell(i < 5 ? 0 : 1);
                }
            }
            this.grid = grid;
        }
        public void placeShipToRandomCell(int ownerID, Units.Unit obj)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cell cell = grid[i, j];
                    if (cell.GetOwnerID() == ownerID && cell.GetValue() == '0')
                    {
                        grid[i, j].SetValue(obj);
                        return;
                    }
                }
            }
        }
        public Cell GetCell(int x, int y)
        {
            return grid[x, y];
        }

        public string PrintGrid(int playerID)
        {
            string gridVisuals = "";

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (playerID == grid[i, j].GetOwnerID())
                    {
                        gridVisuals += grid[i, j].GetValue().ToString();
                    }
                    else
                    {
                        gridVisuals += grid[i, j].GetHits() == 0 ? 'o' : 'x';
                    }
                }

                gridVisuals += "\n";
            }

            return gridVisuals;
        }



        // returns -1 if there's no winner
        // otherwise, returns the winners ID
        public int GetWinnerID()
        {
            // Run through player 0 cells
            int aliveShips = 0;
            for (int i = 0; i < size / 2; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cell cell = grid[i, j];
                    if (cell.GetHits() == 0 && cell.GetValue() != '0')
                    {
                        aliveShips++;
                    }
                }
            }
            if (aliveShips == 0)
            {
                // player 1 won, since player 0 got no ships alive
                return 1;
            }

            aliveShips = 0;
            // Run through player 1 cells
            for (int i = size / 2; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cell cell = grid[i, j];
                    if (cell.GetHits() == 0 && cell.GetValue() != '0')
                    {
                        aliveShips++;
                    }
                }
            }
            if (aliveShips == 0)
            {
                // player 0 won, since player 1 got no ships alive
                return 0;
            }

            return -1;
        }
    }

    class Cell
    {
        private Units.Unit obj;
        //private char value;
        private int ownerID;
        private int hits; // amount of times this cell was hit

        public Cell(int ownerID)
        {
            this.hits = 0;
            this.ownerID = ownerID;
        }

        public char GetValue()
        {
            if (this.obj == null)
            {
                return '0';
            }
            return this.obj.GetUnitTypeSymbol();
        }
        public Units.Unit getObj()
        {
            return this.obj;
        }
        public void SetValue(Units.Unit newValue)
        {
            this.obj = newValue;
        }

        public int GetHits()
        {
            return this.hits;
        }

        public void AddHit()
        {
            this.hits++;
        }

        public int GetOwnerID()
        {
            return this.ownerID;
        }
    }
}
