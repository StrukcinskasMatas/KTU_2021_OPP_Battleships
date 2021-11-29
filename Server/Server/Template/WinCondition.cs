using System;

namespace Server
{
    abstract class WinCondition
    {
        public int GetWinnerId(Grid grid)
        {
            AnnounceStart();
            int Id = FindWinnerId(grid);
            LogWinnerID(Id);
            return Id;
        }

        protected abstract void AnnounceStart();

        protected abstract int FindWinnerId(Grid grid);

        protected void LogWinnerID(int id)
        {
            if (id != -1)
            {
                Console.WriteLine(String.Format("Found winner id: {0}", id));
                return;
            }

            Console.WriteLine("No winner found...");
        }
    }

    class FirstHitWin : WinCondition
    {
        protected override void AnnounceStart()
        {
            Console.WriteLine("Checking for win condition where first ship hit wins");
        }

        protected override int FindWinnerId(Grid gameGrid)
        {
            int size = gameGrid.GetSize();
            Cell[,] grid = gameGrid.GetGrid();

            // Run through player 0 cells
            for (int i = 0; i < size / 2; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cell cell = grid[i, j];
                    if (cell.GetHits() > 0 && cell.GetValue() != '0')
                    {
                        // player 1 won, since player 0 got a hit ship
                        return 1;
                    }
                }
            }

            // Run through player 1 cells
            for (int i = size / 2; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cell cell = grid[i, j];
                    if (cell.GetHits() > 0 && cell.GetValue() != '0')
                    {
                        // player 0 won, since player 1 got a hit ship
                        return 0;
                    }
                }
            }

            return -1;
        }
    }

    class HitAllWin : WinCondition
    {
        protected override void AnnounceStart()
        {
            Console.WriteLine("Checking for win condition where hitting all ships wins");
        }

        protected override int FindWinnerId(Grid gameGrid)
        {
            int size = gameGrid.GetSize();
            Cell[,] grid = gameGrid.GetGrid();

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
}
