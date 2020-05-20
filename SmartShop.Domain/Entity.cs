using System;

namespace SmartShop.Domain
{
    public abstract class Entity
    {
    }

    public abstract class Entity<TKey>
    {
        public virtual TKey Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            Entity<TKey> item = (Entity<TKey>)obj;
            return item.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            if (object.ReferenceEquals(left, right))
                return true;

            if ((Entity<TKey>)left == null || (Entity<TKey>)right == null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
