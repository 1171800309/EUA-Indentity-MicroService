using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EUA.Core.Model
{
    public abstract class Entity
    {
        [Key]
        [Column]
        public string Uid { get; set; }

        public override bool Equals(object obj)
        {
            var formatObj = obj as Entity;
            if (ReferenceEquals(this, formatObj))
                return true;
            if (ReferenceEquals(null, formatObj))
                return false;

            return Uid.Equals(formatObj.Uid);
        }

        public static bool operator ==(Entity obj1, Entity obj2)
        {
            if (ReferenceEquals(obj1, null) && ReferenceEquals(obj2, null))
                return true;
            if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
                return false;

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Entity obj1, Entity obj2)
        {
            return !(obj1 == obj2);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 555) + Uid.GetHashCode();
        }
    }
}
