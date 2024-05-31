using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocumentClassificationZonalOcr.Api.Models
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private init; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public bool Cancelled { get; set; } = false;
        protected BaseEntity(int id)
        {
            Id = id;
        }
        protected BaseEntity()
        {
        }

        public static bool operator ==(BaseEntity? first, BaseEntity? second)
        {
            return first is not null && second is not null && first.Equals(second);
        }
        public static bool operator !=(BaseEntity? first, BaseEntity? second)
        {
            return !(first == second);
        }
        public bool Equals(BaseEntity? other)
        {
            if (other is null)
            {
                return false;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return other.Id == Id;
        }
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            if (obj is not BaseEntity entity)
            {
                return false;
            }

            return entity.Id == Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() * 50;
        }
    }
}
