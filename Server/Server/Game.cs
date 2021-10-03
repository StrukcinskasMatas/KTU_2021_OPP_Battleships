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

            // TODO: next things
            Console.WriteLine("This is it, nothing here?!");
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
            player.SendMessage(this.grid.PrintGrid());

            for (int i = 0; i < 3; i++)
            {
                player.SendMessage("(action needed) Place your 1x1 ship: (example input: \"1 2\")");
                string response = player.ReceiveMessage();

                // TODO: validate input based on the grid (if not out of bounds, if the sub-grid belongs to the player)
                var coords = response.Split(" ").Where(x => int.TryParse(x, out _)).Select(int.Parse).Select(x => x - 1).ToList();
                if (coords.Count == 2)
                {
                    Console.WriteLine("Should place 1x1 ship at: " + coords[0].ToString() + " and " + coords[1].ToString());
                    Cell cell = grid.GetCell(coords[0], coords[1]);
                    if (cell.GetOwnerID() != playerID)
                    {
                        player.SendMessage("This cell does not belong to you.");
                        i--;
                        continue;
                    }

                    cell.SetValue('1');
                    player.SendMessage("(cls)");
                    player.SendMessage(this.grid.PrintGrid());
                }
                else
                {
                    player.SendMessage("Invalid input.");
                    i--;
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
    }
}
