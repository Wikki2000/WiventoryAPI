using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WiventoryAPI.Models
{
    /// <summary>
    /// Represents the base model that other entity classes inherit from.
    /// Provides common properties such as Id, CreatedAt, and UpdatedAt.
    /// </summary>
    public abstract class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Returns a JSON string representation of the current object.
        /// </summary>
        /// <returns>A JSON string representing the object, formatted with indentation.</returns>
        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.Never
            };

            return JsonSerializer.Serialize(this, this.GetType(), options);
        }
    }
}
