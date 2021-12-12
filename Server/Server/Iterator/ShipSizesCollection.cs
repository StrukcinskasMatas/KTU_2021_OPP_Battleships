using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Iterator
{
    class ShipSizesCollection : IteratorAggregate
    {
        List<ShipSize> _collection = new List<ShipSize>();

        public List<ShipSize> getItems()
        {
            return _collection;
        }

        public void AddItem(ShipSize item)
        {
            this._collection.Add(item);
        }

        public override IEnumerator GetEnumerator()
        {
            return new ShipSizeIterator(this);
        }
    }
}
