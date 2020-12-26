using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Lieferant
    /// </summary>
    [Table("SUPPLIER")]
    public class Supplier : ItemAaddress
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Supplier()
            : base()
        {
        }
    }
}
