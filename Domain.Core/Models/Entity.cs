using FluentValidation;
using FluentValidation.Results;
using System;

namespace Autoglass.Domain.Core.Models
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {

        #region Properties
        public ValidationResult ValidationResult { get; protected set; }
        #endregion

        #region Constructors
        protected Entity()
        {
            ValidationResult = new ValidationResult();
        }
        #endregion

        #region Métodos

        public abstract bool IsValid();
        public abstract string KeyValueLog();

        #endregion
    }

    public abstract class EntityLog<T> : Entity<T> where T : Entity<T>
    {

        #region Properties
        public DateTime CreatedAt { get; set; }
        public DateTime? ChangedAt { get; set; }
        public int? UserIdChange { get; set; }
        #endregion

        #region Constructors
        protected EntityLog()
        {
            CreatedAt = DateTime.Now;
        }
        #endregion

        #region Métodos

        #endregion

    }

    public abstract class Entity<T, TKey> : Entity<T> where T : Entity<T, TKey>
    {
        #region Properties
        public TKey Id { get; set; }
        #endregion

        #region Constructors
        protected Entity()
        {
        }
        #endregion

        #region Métodos

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T, TKey>;
            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<T, TKey> a, Entity<T, TKey> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);

        }

        public static bool operator !=(Entity<T, TKey> a, Entity<T, TKey> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id.ToString() + "]";
        }

        public override string KeyValueLog()
        {
            return "{\"Id\":" + Id.ToString() + "}";
        }

        #endregion

    }

    public abstract class EntityLog<T, TKey> : Entity<T, TKey> where T : Entity<T, TKey>
    {

        #region Properties
        public DateTime CreatedAt { get; set; }
        public DateTime? ChangedAt { get; set; }
        public int? UserIdChange { get; set; }
        #endregion

        #region Constructors
        protected EntityLog()
        {
            CreatedAt = DateTime.Now;
        }
        #endregion

        #region Métodos


        #endregion

    }
}
