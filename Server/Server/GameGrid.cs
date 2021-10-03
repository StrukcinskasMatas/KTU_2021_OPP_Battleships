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

        public Cell GetCell(int x, int y)
        {
            return grid[x, y];
        }

        public string PrintGrid()
        {
            string gridVisuals = "  ";
            for (int i = 0; i < size; i++)
            {
                gridVisuals += (i + 1).ToString();
            }
            gridVisuals += "\n\n";

            for (int i = 0; i < size; i++)
            {
                gridVisuals += (i + 1).ToString() + " ";

                for (int j = 0; j < size; j++)
                {
                    gridVisuals += grid[i, j].GetValue().ToString();
                }

                gridVisuals += "\n";
            }

            return gridVisuals;
        }
    }

    class Cell
    {
        private char value;
        private int ownerID;
        private int hits; // amount of times this cell was hit

        public Cell(int ownerID)
        {
            this.hits = 0;
            this.value = '0';
            this.ownerID = ownerID;
        }

        public char GetValue()
        {
            return this.value;
        }

        public void SetValue(char newValue)
        {
            this.value = newValue;
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
