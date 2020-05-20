using System.Collections.Generic;
using System.Linq;

namespace SmartShop.Domain
{
    public abstract  class ValueObject
    {
        protected virtual IEnumerable<object> GetValues()
        {
            var type = GetType();
            foreach (var p in type.GetProperties().OrderBy(c => c.Name))
            {
                if (p.CanRead)
                {
                    yield return p.GetValue(this);
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ValueObject item = (ValueObject)obj;
            IEnumerator<object> thisValues = GetValues().GetEnumerator();
            IEnumerator<object> otherValues = item.GetValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (object.ReferenceEquals(thisValues.Current, null) ^ object.ReferenceEquals(otherValues, null))
                    return false;

                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                    return false;
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetValues()
                .Select(c => c != null ? c.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject v1, ValueObject v2)
        {
            if (object.ReferenceEquals(v1, v2))
                return true;

            if ((ValueObject)v1 == null || (ValueObject)v2 == null)
                return false;

            return v1.Equals(v2);
        }

        public static bool operator !=(ValueObject v1, ValueObject v2)
        {
            return !(v1 == v2);
        }
    }
}
