using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Server.StrategyObserverBuilder;
using Server.Units;
using Server.Decorator;
using Server.Adapter;
using Server.Command;
using Server.COR;
using Server.Mediator;
using Server.StateFlyweightProxy;
using Server.Visitor;

namespace Server
{
    class Facade
    {
        Composite.Fleet fleet0 = new Composite.Fleet();
        Composite.Fleet fleet1 = new Composite.Fleet();
        public Context gameStateContext = new Context(new ConcreteRunningGameState());
        MissMethod method1 = new HighMissChance();
        MissMethod method2 = new AvarageMissChance();
        MissMethod method3 = new LowMissChance();

        // Singleton instance
        private static Facade instance = null;
        private Facade(List<Player> players)
        {
            Console.WriteLine("Game singleton initialized.");
            this.players = players;
            this.grid = new Grid(10);
            this.playersCollection = new Iterator.PlayersCollection();
            foreach (Player player in players)
            {
                playersCollection.AddItem(player);
            }
        }

        private Grid grid;
        private List<Player> players;
        private Iterator.PlayersCollection playersCollection;

        public static Facade getInstance(List<Player> players)
        {
            if (instance == null)
            {
                instance = new Facade(players);
            }

            return instance;
        }

        public void StartGame()
        {
            SendGlobalMessage("Game is starting!", true, false);
            Invoker invoker = new Invoker();
            //invoker.SetOnStart(new SimpleCommand("Say Hi!"));
            Receiver receiver = new Receiver();

            SetupShips();
            int activePlayerID = 0;

            //SharedFolderProxy folderProxy1 = new SharedFolderProxy(emp1);
            //folderProxy1.PerformRWOperations();

            // Iterator use #3
            foreach(Player player in playersCollection)
            {
                ProxySubscription proxySubscription = new ProxySubscription(player, grid);
                proxySubscription.SubscribeToMapChanges();
            }

            // WinCondition gameMode = new HitAllWin();
            WinCondition gameMode = new FirstHitWin();

            int winnerID = gameMode.GetWinnerId(grid);
            Message message = new Message();
            Message response = new Message();
            while (winnerID == -1)
            {
                // Define players
                Player activePlayer = players[activePlayerID];
                Player waitingPlayer = players[1 - activePlayerID];
                FirstPlayer component1 = new FirstPlayer(activePlayer);
                SecondPlayer component2 = new SecondPlayer(waitingPlayer);
                new ConcreteMediator(component1, component2);
                invoker.SetOnStart(new MessageSender(receiver, activePlayer.GetSocket(), this.grid.PrintGrid(activePlayerID), true, false));
                invoker.SetOnFinish(new MessageSender(receiver, activePlayer.GetSocket(), "Select action (A)ttack, (C)opy, (U)pgrade, (S)irens", false, true));
                Boolean attackMove = false;
                Boolean copyMove = false;
                // Inform waiting player
                // Get command from active player
                waitingPlayer.SendMessage("Waiting for other player's turn...", false, false);
                invoker.StartSending();
                //activePlayer.SendMessage("Select action (A)ttack, (C)opy U(pgrade)", false, true);
                //padaryti print grind kaip commandą kuri iškviečiama invokerio
                //activePlayer.SendMessage("(action needed) Attack enemy's territory: (example input: \"1 2\")");
                string type = activePlayer.ReceiveMessage()[0].ToString(); //TODO: this is a hack, need to fix message sending
                var attack = new AttackHandler();
                var copy = new CopyHandler();
                var upgrade = new UpgradeHandler();
                attack.SetNext(copy).SetNext(upgrade);
                var result = attack.Handle(type);
                switch (type)
                {
                    case "A":
                        if (gameStateContext.GetState() == "ConcreteRunningGameState") {
                            activePlayer.SendMessage($"{result}", false, true);
                            attackMove = true;
                            break;
                        } else
                        {
                            activePlayer.SendMessage("GAME IS PAUSED!!!!", false, true);
                            continue;
                        }
           
                    case "C":
                        if (gameStateContext.GetState() == "ConcreteRunningGameState")
                        {
                            activePlayer.SendMessage($"{result}", false, true);
                            copyMove = true;
                            break;
                        }
                        else {
                            activePlayer.SendMessage("GAME IS PAUSED!!!!", false, true);
                            continue;
                        }
                    case "U":
                        if (gameStateContext.GetState() == "ConcreteRunningGameState")
                        {
                            activePlayer.SendMessage($"{result}", false, true);
                            break;
                        }
                        else {
                            activePlayer.SendMessage("GAME IS PAUSED!!!!", false, true);
                            continue;
                        }
                           
                    case "P":
                        if (gameStateContext.GetState() == "ConcreteRunningGameState")
                        {
                            SendGlobalMessage("Game WAS PAUSED!", false, true);
                            gameStateContext.RequestToChangeGameState();
                            continue;
                        }
                        else {
                            SendGlobalMessage("Game WAS UNPAUSED!", false, true);
                            gameStateContext.RequestToChangeGameState();
                            continue;
                        }
                            
                    default:
                        if (gameStateContext.GetState() == "ConcreteRunningGameState")
                        {
                            activePlayer.SendMessage("Invalid input.", false, true);
                            continue;
                        }
                        else
                        {
                            activePlayer.SendMessage("Game IS PAUSED!", false, true);
                            continue;
                        }
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
                                    if (cell.getObj() != null)
                                    {
                                        Units.Unit shipUnit = cell.getObj();
                                        Units.Unit fireDecoratedUnit = new FireDecorator(shipUnit);
                                        Units.Unit waterDecoratedUnit = new WaterDecorator(fireDecoratedUnit);
                                        Units.Unit lightingDecoratedUnit = new LightningDecorator(waterDecoratedUnit);
                                        Console.WriteLine(lightingDecoratedUnit.Operation() + " " + shipUnit.ShowStatus(new AdapterDead(new StatusDead())));
                                    }
                                }
                                activePlayer.SendMessage(this.grid.PrintGrid(activePlayerID), true, false);

                                if (activePlayerID == 0)
                                {
                                    fleet0.turnOnSirens();
                                }
                                else
                                {
                                    fleet1.turnOnSirens();
                                }
                            }
                        } else if (copyMove)
                        {
                            Units.Unit shipUnit = cell.getObj();
                            Units.Unit newShip = (Units.Unit)shipUnit.DeepClone();
                            grid.placeShipToRandomCell(activePlayerID, newShip);
                            activePlayer.SendMessage(this.grid.PrintGrid(activePlayerID), true, false);
                        } else
                        {
                            component1.UpgradeShip(cell, this.grid, activePlayerID);
                            activePlayer.SendMessage("Type R to revert, or enter to skip:", false, true);
                            if (activePlayer.ReceiveMessage() == "R")
                            {
                                component1.RevertShip(cell, this.grid, activePlayerID);
                            }
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
                winnerID = gameMode.GetWinnerId(grid);
            }
            players[winnerID].SendMessage("You've won the game!", false, false);
            players[1 - winnerID].SendMessage("You've lost the game!", false, false);

            Console.WriteLine("Game ended!");
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
            Units.Battleship battleship;
            Units.AbstractFactory unitFactory;

            // Iterator use #1
            var unitsCollection = new Iterator.UnitsCollection();

            // Iterator use #2
            var shipSizesCollection = new Iterator.ShipSizesCollection();
            shipSizesCollection.AddItem(new Iterator.ShipSize(1));
            shipSizesCollection.AddItem(new Iterator.ShipSize(2));
            shipSizesCollection.AddItem(new Iterator.ShipSize(1));

            foreach (Iterator.ShipSize shipSize in shipSizesCollection)
            {
                battleship = creator.CreateBattleship(shipSize.GetShipSize());
                unitFactory = battleship.GetAbstractFactory();

                player.SendMessage("Choose your ship type: (T)ank or (U)tility:", false, true);

                Units.Unit shipUnit;
                while (true)
                {
                    string type = player.ReceiveMessage()[0].ToString(); //TODO: this is a hack, need to fix message sending
                    switch (type)
                    {
                        case "T":
                            if (gameStateContext.GetState() == "ConcreteRunningGameState")
                            {
                                player.SendMessage("Choose your tank color: (G)reen or (R)ed:", false, true);
                                string color = player.ReceiveMessage()[0].ToString();
                                
                                if (color.Trim() == "G")
                                {
                                    shipUnit = unitFactory.CreateTank(char.Parse(color));
                                    shipUnit.setPerk();
                                    shipUnit.setMissChance(findMissChanceByPerk(shipUnit));
                                    shipUnit.getConfiguration();
                                    break;

                                }
                                else if (color.Trim() == "R")
                                {
                                    shipUnit = unitFactory.CreateTank(char.Parse(color));
                                    shipUnit.setPerk();
                                    shipUnit.setMissChance(findMissChanceByPerk(shipUnit));
                                    shipUnit.getConfiguration();
                                    break;
                                }
                                else {
                                    player.SendMessage("Invalid input.", false, true);
                                    continue;
                                }
                            }
                            else
                            {
                                player.SendMessage("GAME IS PAUSED!!!!", false, true);
                                continue;
                            }

                        case "U":
                            if (gameStateContext.GetState() == "ConcreteRunningGameState")
                            {
                                shipUnit = unitFactory.CreateUtility();
                                shipUnit.setPerk();
                                shipUnit.setMissChance(findMissChanceByPerk(shipUnit));
                                shipUnit.getConfiguration();
                                break;
                            }
                            else
                            {
                                player.SendMessage("GAME IS PAUSED!!!!", false, true);
                                continue;
                            }

                        case "P":
                            if (gameStateContext.GetState() == "ConcreteRunningGameState")
                            {
                                SendGlobalMessage("Game WAS PAUSED!", false, true);
                                gameStateContext.RequestToChangeGameState();
                                continue;
                            }
                            else
                            {
                                SendGlobalMessage("Game WAS UNPAUSED!", false, true);
                                gameStateContext.RequestToChangeGameState();
                                continue;
                            }
                        default:
                            if (gameStateContext.GetState() == "ConcreteRunningGameState")
                            {
                                player.SendMessage("Invalid input.", false, true);
                                continue;
                            }
                            else
                            {
                                player.SendMessage("Game IS PAUSED!", false, true);
                                continue;
                            }
                    }

                    break;
                }
                //shipUnit.perk.skill()
                if (playerID == 0)
                {
                    fleet0.Add(shipUnit);
                }
                else
                {
                    fleet1.Add(shipUnit);
                }

                unitsCollection.AddItem(shipUnit);

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

                    Console.WriteLine(String.Format("Player {0} placed {1}{2} ship at: {3}, {4}, {5}", 
                        playerID.ToString(), shipUnit.GetUnitType(), shipUnit.GetSizeString(), coords[0].ToString(), coords[1].ToString(), shipUnit.ShowStatus(new StatusAlive())));
                    
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

            foreach (var element in unitsCollection)
            {
                Console.WriteLine($"Player {playerID} unit - {element}");
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
        private double findMissChanceByPerk(Unit shipUnit)
        {
            if (shipUnit.perk.skill() == "Slow")
                return method3.addMissChance(shipUnit);
            else if (shipUnit.perk.skill() == "Durable")
                return method2.addMissChance(shipUnit);
            else
                return method1.addMissChance(shipUnit);
        }
    }
}
