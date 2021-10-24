using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Server.StrategyObserverBuilder;
using Server.Units;
using Server.Decorator;

namespace Server
{
    class Game
    {
        // Singleton instance
        private static Game instance = null;
        private Grid grid;
        private List<Player> players;

        private Game(List<Player> players)
        {
            Console.WriteLine("Game singleton initialized.");
            this.players = players;
            this.grid = new Grid(10);
        }

        public static Game getInstance(List<Player> players)
        {
            if (instance == null)
            {
                instance = new Game(players);
            }

            return instance;
        }

        public void StartGame()
        {
            SendGlobalMessage("Game is starting!", true, false);
            SetupShips();
            Subscribe();

            int activePlayerID = 0;
            int winnerID = grid.GetWinnerID();
            Message message = new Message();
            Message response = new Message();
            while (winnerID == -1)
            {
                // Define players
                Player activePlayer = players[activePlayerID];
                Player waitingPlayer = players[1 - activePlayerID];
                Boolean attackMove = false;
                // Inform waiting player
                // Get command from active player
                waitingPlayer.SendMessage("Waiting for other player's turn...", false, false);
                activePlayer.SendMessage(this.grid.PrintGrid(activePlayerID), true, false);
                activePlayer.SendMessage("Select action (A)ttack, (C)opy", false, true);

                //activePlayer.SendMessage("(action needed) Attack enemy's territory: (example input: \"1 2\")");
                string type = activePlayer.ReceiveMessage()[0].ToString(); //TODO: this is a hack, need to fix message sending
                switch (type)
                {
                    case "A":
                        activePlayer.SendMessage("Attack enemy's territory: (example input: \"1 2\")", false, true);
                        //shipUnit = unitFactory.CreateTank();
                        attackMove = true;
                        break;
                    case "C":
                        activePlayer.SendMessage("Select ship to copy: (example input: \"1 2\")", false, true);
                        //shipUnit = unitFactory.CreateUtility();
                        break;
                    default:
                        //player.SendMessage("(action needed) Invalid input.");
                        continue;
                }

                while (true)
                {
                    List<int> coords = ConvertResponseToCoordsList(activePlayer.ReceiveMessage());

                    // TODO: validate input based on the grid (if not out of bounds, if the sub-grid belongs to the player)
                    if (coords.Count == 2)
                    {
                        Cell cell = grid.GetCell(coords[0], coords[1]);

                        if (attackMove)
                        {
                            activePlayer.SendMessage("Choose rocket explosion type (small, medium, big):", false, true);

                            Explosions? explosionType = CovertResponseToExplotionType(activePlayer.ReceiveMessage(), activePlayer);
                            Console.WriteLine(explosionType);

                            if (explosionType != null) {
                                
                                List<Coordinates> coodinatesListWithExplosionEffect = new List<Coordinates>();

                                if (explosionType == Explosions.Small)
                                {
                                    RocketStrategyInterface smallExplosion = new SmallExplosion();
                                    Rocket rocket = new Rocket(strategy: smallExplosion);
                                    coodinatesListWithExplosionEffect = rocket.explotionDamageArea(coords[0], coords[1]);
                                }
                                else if (explosionType == Explosions.Medium)
                                {
                                    RocketStrategyInterface mediumExplosion = new MediumExplosion();
                                    Rocket rocket = new Rocket(strategy: mediumExplosion);
                                    coodinatesListWithExplosionEffect = rocket.explotionDamageArea(coords[0], coords[1]);
                                }
                                else if (explosionType == Explosions.Big)
                                {
                                    RocketStrategyInterface bigExplosion = new BigExplosion();
                                    Rocket rocket = new Rocket(strategy: bigExplosion);
                                    coodinatesListWithExplosionEffect = rocket.explotionDamageArea(coords[0], coords[1]);
                                }

                                Console.WriteLine("Player " + activePlayerID.ToString() + " attacked: " + coords[0].ToString() + ", " + coords[1].ToString());
                                if (cell.GetOwnerID() == activePlayerID)
                                {
                                    activePlayer.SendMessage("You cannot attack your own territory.", false, true);
                                    continue;
                                }

                                foreach (Coordinates coordinates in coodinatesListWithExplosionEffect)
                                {
                                    cell = grid.GetCell(coordinates.GetCoordinates().Item1, coordinates.GetCoordinates().Item2);
                                    Console.WriteLine(grid.GetCell(coordinates.GetCoordinates().Item1, coordinates.GetCoordinates().Item2));

                                    cell.AddHit();
                                }
                                activePlayer.SendMessage(this.grid.PrintGrid(activePlayerID), true, false);
                            }
                        } else
                        {
                            Units.Unit shipUnit = cell.getObj();
                            // FIXME: TO DO - Implement case when user enters random coordinates and not the ship coordinates
                            Units.Unit newShip = (Units.Unit)shipUnit.Clone();
                            //Console.WriteLine(newShip.GetUnitInfo());
                            grid.placeShipToRandomCell(activePlayerID, newShip);
                            activePlayer.SendMessage(this.grid.PrintGrid(activePlayerID), true, false);
                        }
                    }
                    else
                    {
                        activePlayer.SendMessage("Invalid input.", false, true);
                        continue;
                    }

                    //// Get out of the while loop after successful attack (replace with a bool?)
                    break;
                }

                // Invert the active player's ID
                activePlayerID = Math.Abs(activePlayerID - 1);

                // Check game state
                winnerID = grid.GetWinnerID();
            }
            players[winnerID].SendMessage("You've won the game!", false, false);
            players[1 - winnerID].SendMessage("You've lost the game!", false, false);

            Console.WriteLine("Game ended!");
        }

        private void Subscribe()
        {
            grid.Attach(players[0]);
            grid.Attach(players[1]);
        }

        private void SetupShips()
        {
            // Player one setup ships
            players[1].SendMessage("Player one is setting up his ships...", false, false);
            PlaceShips(0);

            // Player two setup ships
            players[0].SendMessage("Player two is setting up his ships...", false, false);
            PlaceShips(1);
        }

        private void PlaceShips(int playerID)
        {
            Player player = players[playerID];
            player.SendMessage(this.grid.PrintGrid(playerID), true, false);

            Units.Creator creator = new Units.BattleshipCreator();
            int[] shipSizes = new int[] { 1 };

            foreach (var shipSize in shipSizes)
            {
                Units.Battleship battleship = creator.CreateBattleship(shipSize);
                Units.AbstractFactory unitFactory = battleship.GetAbstractFactory();
                player.SendMessage("Choose your ship type: (T)ank or (U)tility:", false, true);

                Units.Unit shipUnit;
                while (true)
                {
                    string type = player.ReceiveMessage()[0].ToString(); //TODO: this is a hack, need to fix message sending
                    switch (type)
                    {
                        case "T":
                            shipUnit = unitFactory.CreateTank();
                            shipUnit.getConfiguration();
                            //grid.Attach(shipUnit);
                            break;
                        case "U":
                            shipUnit = unitFactory.CreateUtility();
                            shipUnit.getConfiguration();
                            //grid.Attach(shipUnit);
                            break;
                        default:
                            player.SendMessage("Invalid input.", false, true);
                            continue;
                    }

                    break;
                }
                Units.Unit decoratedUnit = new BlueColorDecorator(shipUnit);
                Units.Unit reddecoratedUnit = new RedColorDecorator(decoratedUnit);
                Units.Unit greendecoratedUnit = new GreenColorDecorator(reddecoratedUnit);
                Console.WriteLine(greendecoratedUnit.Operation());
                
                // TODO: validate input if not out of bounds
                // TODO: validate if ship already placed
                player.SendMessage(String.Format("Place your {0} {1} ship: (example coords input: \"1 2\")", shipUnit.GetUnitType(), shipUnit.GetSizeString()), false, true);
                while (true)
                {
                    List<int> coords = ConvertResponseToCoordsList(player.ReceiveMessage());
                    if (coords.Count != 2)
                    {
                        player.SendMessage("Invalid input.", false, true);
                        continue;
                    }

                    Console.WriteLine(String.Format("Player {0} placed {1}{2} ship at: {3}, {4}", 
                        playerID.ToString(), shipUnit.GetUnitType(), shipUnit.GetSizeString(), coords[0].ToString(), coords[1].ToString()));

                    Cell cell = grid.GetCell(coords[0], coords[1]);
                    if (cell.GetOwnerID() != playerID)
                    {
                        player.SendMessage("This cell does not belong to you.", false, false);
                        continue;
                    }

                    for (int i = 0; i < shipUnit.GetLenght(); i++)
                    {
                        Cell cellToChange = grid.GetCell(coords[0], coords[1] + i);
                        cellToChange.SetValue(shipUnit);
                    }
                    player.SendMessage(this.grid.PrintGrid(playerID), true, false);
                    break;
                }
            }
        }

        private void SendGlobalMessage(string msg, bool clear, bool action)
        {
            foreach (var player in this.players)
            {
                player.SendMessage(msg, clear, action);
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

        private Explosions CovertResponseToExplotionType(string response, Player player) {

            Console.WriteLine(response);

            if (response.ToLower().Trim()[0] == 's')
            {
                return Explosions.Small;
            }
            else if (response.ToLower().Trim()[0] == 'm')
            {
                return Explosions.Medium;
            }
            else if (response.ToLower().Trim()[0] == 'b')
            {
                return Explosions.Big;
            }
            else 
            {
                Console.WriteLine("HEREEEEEEE!!!!!!!");
                player.SendMessage("Please choose an existing explosion type: small, medium, big", false, false);
                return default;
            }
        }
    }
}
