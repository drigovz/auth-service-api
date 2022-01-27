using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuthService.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        private DateTime? _createdAt;

        protected BaseEntity()
        {
            Id = Guid.NewGuid(); 
        }

        public DateTime? CreatedAt
        {
            get => _createdAt;
            set => _createdAt = (value == null ? DateTime.UtcNow : value);
        }

        public DateTime? UpdatedAt { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool Valid { get; protected set; }

        [NotMapped]
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; protected set; }

        protected bool EntityValidation<T>(T model, AbstractValidator<T> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }
    }
}
