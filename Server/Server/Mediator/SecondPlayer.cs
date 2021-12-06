using Server.Memento;
using System;

namespace Server.Mediator
{
    class SecondPlayer : BaseComponent
    {
        private Player player;
        private Caretaker m = new Caretaker();

        public SecondPlayer(Player player)
        {
            this.player = player;
        }
        public void UpgradeShip(Cell cell, Grid grid, int activePlayerID)
        {
            Units.Unit shipUnit = cell.getObj();

            m.Memento = shipUnit.SaveSchieldMemento();
            shipUnit.ChangeShield("Platinum");

            player.SendMessage(grid.PrintGrid(activePlayerID), true, false);
            this._mediator.Notify(this, 1);
        }
        public void PostMessage()
        {
            player.SendMessage("Oponent made an upgrade.", false, false);
        }
        public void PostMessageAboutUpgrade()
        {
            player.SendMessage("Ship upgraded sucessfully.", false, false);
        }
        public void RevertShip(Cell cell, Grid grid, int activePlayerID)
        {
            Units.Unit shipUnit = cell.getObj();

            shipUnit.RestoreShiledMemeto(m.Memento);

            player.SendMessage(grid.PrintGrid(activePlayerID), true, false);
            this._mediator.Notify(this, 1);
        }
    }
}
