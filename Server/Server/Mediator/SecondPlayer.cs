using System;

namespace Server.Mediator
{
    class SecondPlayer : BaseComponent
    {
        private Player player;

        public SecondPlayer(Player player)
        {
            this.player = player;
        }
        public void UpgradeShip(Cell cell, Grid grid, int activePlayerID)
        {
            Units.Unit shipUnit = cell.getObj();
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

    }
}
