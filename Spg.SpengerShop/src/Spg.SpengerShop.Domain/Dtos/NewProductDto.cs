using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public enum ProductionStatesDto
    {

    }

    public class NewProductDto
    {
        public ProductionStatesDto ProductionState { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Tax { get; set; } // Steuerklasse
        public string Ean { get; set; } = string.Empty;
        public string? Material { get; set; } = string.Empty;
        public DateTime? ExpiryDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}
