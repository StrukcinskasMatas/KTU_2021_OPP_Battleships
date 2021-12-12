using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Iterator
{
    class UnitsCollection : IteratorAggregate
    {
        List<Units.Unit> _collection = new List<Units.Unit>();

        public List<Units.Unit> getItems()
        {
            return _collection;
        }

        public void AddItem(Units.Unit item)
        {
            this._collection.Add(item);
        }

        public override IEnumerator GetEnumerator()
        {
            return new UnitIterator(this);
        }
    }
}
