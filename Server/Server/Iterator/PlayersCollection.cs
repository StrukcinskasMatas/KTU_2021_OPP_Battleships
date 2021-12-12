using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Iterator
{
    class PlayersCollection : IteratorAggregate
    {
        List<Player> _collection = new List<Player>();

        public List<Player> getItems()
        {
            return _collection;
        }

        public void AddItem(Player item)
        {
            this._collection.Add(item);
        }

        public override IEnumerator GetEnumerator()
        {
            return new PlayerIterator(this);
        }
    }
}
