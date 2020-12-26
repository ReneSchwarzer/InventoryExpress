using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Hersteller
    /// </summary>
    [Table("MANUFACTURER")]
    public class Manufacturer : ItemAaddress
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Manufacturer()
            : base()
        {
        }
    }
}
