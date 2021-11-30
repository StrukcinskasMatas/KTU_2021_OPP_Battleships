using System;

namespace Server.Composite
{
    public abstract class GroupedUnit
    {
        public virtual void Add(GroupedUnit unit)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(GroupedUnit unit)
        {
            throw new NotImplementedException();
        }

        public abstract void turnOnSirens();

        public virtual bool isUnit() 
        {
            return false;
        }
    }
}
