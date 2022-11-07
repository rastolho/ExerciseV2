using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.Serialization;

namespace ExerciseGB.DTO.Models
{
    public class Purchase 
    {
        /// <summary>
        /// Net Value
        /// </summary>
        public double Net { get; set; }

        /// <summary>
        /// Gross Value
        /// </summary>
        public double Gross { get; set; }

        /// <summary>
        /// Vat Value
        /// </summary>
        public double Vat { get; set; }
    }
}
