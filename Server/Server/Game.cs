using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Game
    {
        private Grid grid;
        private List<Player> players;

        public Game(List<Player> players)
        {
            this.players = players;
            this.grid = new Grid(10);
        }

        public void StartGame()
        {
            SendGlobalMessage("(cls) Game is starting!");
            SetupShips();

            int activePlayerID = 0;
            int winnerID = grid.GetWinnerID();
            while (winnerID == -1)
            {
                // Define players
                Player activePlayer = players[activePlayerID];
                Player waitingPlayer = players[1 - activePlayerID];

                // Inform waiting player
                waitingPlayer.SendMessage("Waiting for other player's turn...");

                // Get command from active player
                activePlayer.SendMessage("(cls)");
                activePlayer.SendMessage(this.grid.PrintGrid(activePlayerID));
                activePlayer.SendMessage("(action needed) Attack enemy's territory: (example input: \"1 2\")");

                while (true)
                {
                    List<int> coords = ConvertResponseToCoordsList(activePlayer.ReceiveMessage());

                    // TODO: validate input based on the grid (if not out of bounds, if the sub-grid belongs to the player)
                    if (coords.Count == 2)
                    {
                        Console.WriteLine("Player " + activePlayerID.ToString() + " attacked: " + coords[0].ToString() + ", " + coords[1].ToString());
                        Cell cell = grid.GetCell(coords[0], coords[1]);
                        if (cell.GetOwnerID() == activePlayerID)
                        {
                            activePlayer.SendMessage("(action needed) You cannot attack your own territory.");
                            continue;
                        }

                        cell.AddHit();
                        activePlayer.SendMessage("(cls)");
                        activePlayer.SendMessage(this.grid.PrintGrid(activePlayerID));
                    }
                    else
                    {
                        activePlayer.SendMessage("(action needed) Invalid input.");
                        continue;
                    }

                    // Get out of the while loop after successful attack (replace with a bool?)
                    break;
                }

                // Invert the active player's ID
                activePlayerID = Math.Abs(activePlayerID - 1);

                // Check game state
                winnerID = grid.GetWinnerID();
            }

            players[winnerID].SendMessage("You've won the game!");
            players[1 - winnerID].SendMessage("You've lost the game!");

            Console.WriteLine("Game ended!");
        }

        private void SetupShips()
        {
            // Player one setup ships
            players[1].SendMessage("Player one is setting up his ships...");
            PlaceShips(0);

            // Player two setup ships
            players[0].SendMessage("Player two is setting up his ships...");
            PlaceShips(1);
        }

        private void PlaceShips(int playerID)
        {
            Player player = players[playerID];
            player.SendMessage("(cls)");
            player.SendMessage(this.grid.PrintGrid(playerID));

            for (int i = 0; i < 3; i++)
            {
                player.SendMessage("(action needed) Place your 1x1 ship: (example input: \"1 2\")");
                List<int> coords = ConvertResponseToCoordsList(player.ReceiveMessage());

                // TODO: validate input if not out of bounds
                // TODO: validate if ship already placed
                if (coords.Count == 2)
                {
                    Console.WriteLine("Player " + playerID.ToString() + " placed 1x1 ship at: " + coords[0].ToString() + ", " + coords[1].ToString());
                    Cell cell = grid.GetCell(coords[0], coords[1]);
                    if (cell.GetOwnerID() != playerID)
                    {
                        player.SendMessage("This cell does not belong to you.");
                        i--;
                        continue;
                    }

                    cell.SetValue('1');
                    player.SendMessage("(cls)");
                    player.SendMessage(this.grid.PrintGrid(playerID));
                }
                else
                {
                    player.SendMessage("Invalid input.");
                    i--;
                    continue;
                }
            }
        }

        private void SendGlobalMessage(string message)
        {
            foreach (var player in this.players)
            {
                player.SendMessage(message);
            }
        }

        private List<int> ConvertResponseToCoordsList(string response)
        {
            return response
                .Split(" ")
                .Where(x => int.TryParse(x, out _))
                .Select(int.Parse)
                .Select(x => x - 1)
                .ToList();
        }
    }
}
