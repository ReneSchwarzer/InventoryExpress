using System.Collections.Generic;
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
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Manufacturer()
            : base()
        {
            Inventories = new HashSet<Inventory>();
        }
    }
}
